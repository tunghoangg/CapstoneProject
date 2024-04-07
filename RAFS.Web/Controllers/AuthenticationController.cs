using AutoMapper;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;
using RAFS.Web.Models;
using System.Security.Claims;

namespace RAFS.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[action]")]
    public class AuthenticationController : Controller
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<AspUser> _signInManager;
        private readonly UserManager<AspUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendMailService;
        private readonly IFarmAdminService _farmAdminService;

        public AuthenticationController(
            IMapper mapper,
            ILogger<AuthenticationController> logger,
            SignInManager<AspUser> signInManager,
            UserManager<AspUser> userManager,
            ISendMailService sendMailService,
            IFarmAdminService farmAdminService)
        {
            _logger = logger;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _sendMailService = sendMailService;
            _farmAdminService = farmAdminService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (TempData.ContainsKey("checkEmail"))
            {
                ViewBag.checkEmail = TempData["checkEmail"];
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {

            if (ModelState.IsValid)
            {
                //Check account status
                AspUser currentUser = await _userManager.FindByNameAsync(user.Email);

                if (currentUser != null)
                {
                    if (!currentUser.Status)
                    {
                        ModelState.AddModelError("error", "Tài khoản đã bị vô hiệu hoá.");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError("error", " Sai tài khoản hoặc mật khẩu.");
                    return View(user);
                }

                //Logging in
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    //Get User Roles after succesful login
                    var userRoles = await _userManager.GetRolesAsync(currentUser);

                    string redirectAction = "";
                    string redirectController = "";
                    //Add FarmId to Cookie if User is a technician or employee
                    foreach (var role in userRoles)
                    {
                        if (role.Equals("Technician"))
                        {
                            Farm farm = await _farmAdminService.GetFarmByUserId(currentUser.Id);
                            if (farm != null)
                            {
                                if (!farm.Status)
                                {
                                    ModelState.AddModelError("error", "Trang trại chủ quản không hoạt động.");
                                    await _signInManager.SignOutAsync();
                                    return View(user);
                                }

                                
                            }
                            if (farm == null)
                            {
                                ModelState.AddModelError("error", "Tài khoản nhân viên không thuộc vào trang trại nào.");
                                await _signInManager.SignOutAsync();
                                return View(user);
                            }

                            redirectAction = "ViewProfile";
                            redirectController = "Profile";
                        }

                        if (role.Equals("Admin"))
                        {
                            redirectAction = "UserManagement";
                            redirectController = "Admin";
                        }

                        if (role.Equals("Owner"))
                        {
                            redirectAction = "Index";
                            redirectController = "Statistics";
                        }

                        if (role.Equals("Staff"))
                        {
                            redirectAction = "BlogList";
                            redirectController = "Blog";
                        }
                    }

                    return RedirectToAction(redirectAction, redirectController);
                }

                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("error", "Vui lòng kiểm tra Email để xác minh tài khoản.");
                    TwoFactor(currentUser);
                    return View(user);
                }

                else
                {
                    ModelState.AddModelError("error", " Sai tài khoản hoặc mật khẩu.");
                    return View(user);
                }

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactor(AspUser account)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(account);
            var link = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = account.Email }, Request.Scheme);
            await _sendMailService.sendMailAsync(account.Email, "Xác nhận tài khoản", link);
            return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Chưa map RegistrationViewModel to AspUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationDTO registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var user = _mapper.Map<AspUser>(registration);

            user.Status = true;

            var result = await _userManager.CreateAsync(user, registration.Password);

            //if (!TermsAccepted) {
            //    ModelState.AddModelError(string.Empty, "Bạn phải chấp nhận các điều khoản.");
            //}

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registration);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            await _sendMailService.sendMailAsync(user.Email, "Xác nhận tài khoản", link);

            await _userManager.AddToRoleAsync(user, "Owner");
            TempData["checkEmail"] = "Vui lòng kiểm tra Email để xác minh tài khoản.";
            return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel rpvm)
        {
            if (!ModelState.IsValid)
            {
                return View(rpvm);
            }

            //Get user
            var user = await _userManager.FindByEmailAsync(rpvm.Email);
            ModelState.AddModelError("confirm", "Vui lòng kiểm tra tài khoản Email của bạn để tiếp tục.");
            if (user == null)
            {
                
                return View(rpvm);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);


            await _sendMailService.sendMailAsync(user.Email, "Quên mật khẩu", link);
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

            if (user == null)
            {

                return View();
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation", "Authentication");
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GoogleLogin(string provider, string returnUrl = null)
        {
            try
            {
                // Kiểm tra yêu cầu dịch vụ provider tồn tại
                var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                var provider_process = listprovider.Find((m) => m.Name == provider);
                if (provider_process == null)
                {
                    return NotFound("Dịch vụ không chính xác: " + provider);
                }

                // redirectUrl - là Url sẽ chuyển hướng đến - sau khi CallbackPath (/dang-nhap-tu-google) thi hành xong
                // nó bằng identity/account/externallogin?handler=Callback
                // tức là gọi OnGetCallbackAsync
                var redirectUrl = Url.Action("GoogleLoginCallback", "Authentication", new { ReturnUrl = returnUrl });

                // Cấu hình
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return new ChallengeResult(provider, properties);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Authentication");
            }
            // Chuyển hướng đến dịch vụ ngoài (Googe, Facebook)
        }

        [HttpGet]
        public async Task<IActionResult> GoogleLoginCallback(string returnUrl = null, string remoteError = null)
        {

            if (remoteError != null)
            {
                // Handle external authentication errors
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                // Handle external login info not available
                return RedirectToAction(nameof(Login));
            }

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user != null)
            {
                // User already exists, sign them in
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Dashboard");
            }

            // Sign in the user with the external login info
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // If the user doesn't have an account, create a new one
            var email = info.Principal.FindFirstValue(ClaimTypes.Email).ToString();
            var username = email.Substring(0, email.IndexOf('@'));
            var extUser = new AspUser {UserName = username, Email = email};
            var createUserResult = await _userManager.CreateAsync(extUser);

            //Check lỗi trong quá trong quá trình tạo tài khoản
            if (createUserResult.Errors.Any())
            {
                foreach (IdentityError error in createUserResult.Errors)
                {
                    //Nếu email đã tồn tại ( đăng ký bằng Register trước đó)
                    if (error.Code.Equals("DuplicateEmail"))
                    {
                        //Tìm tài khoản đã đăng kí và add thông tin đăng nhập external
                        AspUser asp = await _userManager.FindByEmailAsync(extUser.Email);
                        var addLoginResult = await _userManager.AddLoginAsync(asp, info);
                        if (addLoginResult.Succeeded)
                        {
                            // Sign in the newly created user
                            await _signInManager.SignInAsync(asp, isPersistent: false);
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                }
            }

            if (createUserResult.Succeeded)
            {
                // Add the external login to the new user
                var addLoginResult = await _userManager.AddLoginAsync(extUser, info);
                extUser.Status = true;
                extUser.EmailConfirmed  = true;
                await _userManager.UpdateAsync(extUser);
                await _userManager.AddToRoleAsync(extUser, "Owner");
                if (addLoginResult.Succeeded)
                {
                    // Sign in the newly created user
                    return RedirectToAction("ExternalRegister", "Authentication",new {UserName = extUser.UserName, Email = extUser.Email });
                }
                else
                {
                    // Failed to add external login to the new user
                    // Handle error appropriately
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["FailedExternalLogin"] = "Có lỗi xảy ra trong quá trình xác thực. Vui lòng đăng ký tài khoản.";
                return RedirectToAction("Register", "Authentication");
            }


        }

        [HttpGet]
        public IActionResult ExternalRegister(string UserName, string Email)
        {
            ViewBag.UserName = UserName;
            ViewBag.Email = Email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExternalRegister(ExternalRegistrationDTO registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            AspUser user = await _userManager.FindByEmailAsync(registration.Email);
            user.UserName = registration.UserName;
            user.PhoneNumber = registration.PhoneNumber;
            user.EmailConfirmed = true;
            user.Status = true;
            user.Avatar = registration.Avatar;
            var result = await _userManager.UpdateAsync(user);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registration);
            }


            
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}


