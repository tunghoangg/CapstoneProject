using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.Service
{
    public class FarmAdminService : IFarmAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<AspUser> _userManager;

        public FarmAdminService(IUnitOfWork uow, IMapper mapper, UserManager<AspUser> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateFarmAdminDTOAsync(AddFarmAdminDTO model)
        {
            Farm farm = new Farm();
            farm.Code = GenerateUniqueCode();
            farm.Name = model.Name;
            farm.Address = model.Address;
            farm.Phone = model.Phone;
            farm.PageLink = model.PageLink;
            farm.Area = (double)model.Area;
            farm.EstablishedDate = model.EstablishedDate;
            farm.Description = model.Description;
            farm.Status = true;
            farm.Logo = "https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik";

            await _uow.farmAdminRepo.AddAsync(farm);
            _uow.Save();

            var listFarm = await _uow.farmAdminRepo.GetAllAsync();
            //int farmId = _uow.farmAdminRepo.GetAll().FirstOrDefault(x => x.Code.Equals(farm.Code))?.Id ?? 0;
            int farmId = listFarm.FirstOrDefault(x => x.Code.Equals(farm.Code))?.Id ?? 0;

            UserFunctionFarm uff = new UserFunctionFarm();
            uff.AspUserId = model.UserId;
            uff.FarmId = farmId;
            uff.FunctionId = 1;
            await _uow.uffRepo.AddAsync(uff);
            await _uow.SaveAsync();

            List<Image> images = new List<Image>()
            {
                new Image(){URL = "https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik", FarmId = farmId},
                new Image(){URL = "https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik", FarmId = farmId},
                new Image(){URL = "https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik", FarmId = farmId},
                new Image(){URL = "https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik", FarmId = farmId},
                new Image(){URL = "https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik", FarmId = farmId},
            };

            foreach (Image image in images)
            {
                await _uow.imageRepo.AddAsync(image);
                await _uow.SaveAsync();
            }

            for (int i = 0; i < 2; i++)
            {
                AspUser aspUser = GenerateUserFarm(farm.Code);
                if (aspUser != null)
                {
                    await _uow.aspUserRepo.AddAsync(aspUser);
                    await _uow.SaveAsync();

                    uff.AspUserId = aspUser.Id;
                    uff.FarmId = farmId;
                    uff.FunctionId = 1;
                    
                    await _uow.uffRepo.AddAsync(uff);
                    await _uow.SaveAsync();

                    await _userManager.AddToRoleAsync(aspUser, "Technician");
                    await _uow.SaveAsync();
                }
                

            }
            
        }

        public async Task<FarmAdminDTO> GetFarmAdminDTOByFarmId(int farmId)
        {
            Farm farm = await _uow.farmAdminRepo.GetByIdAsync(farmId);
            var farmAdminDTO = _mapper.Map<FarmAdminDTO>(farm);
            return farmAdminDTO;
        }

        public async Task<List<FarmAdminDTO>> GetFarmAdminDTOsAsync(string userId)
        {
            List<Farm> listFarm = await _uow.farmAdminRepo.GetFarmAdminDTOsAsync(userId);
            var listFarmDTO = _mapper.Map<List<FarmAdminDTO>>(listFarm);
            return listFarmDTO;
        }

        public async Task UpdateFarmAdminDTOAsync(UpdateFarmAdminDTO model)
        {
            //var listFarm = await _uow.farmAdminRepo.GetAllAsync();
            var farm = await _uow.farmAdminRepo.GetByIdAsync(model.FarmId);

            //var farm = listFarm.FirstOrDefault(x => x.Id.Equals(model.FarmId));
            if (farm != null)
            {
                farm.Name = model.Name;
                farm.Address = model.Address;
                farm.Phone = model.Phone;
                farm.PageLink = model.PageLink;
                farm.Area = (double)model.Area;
                farm.EstablishedDate = model.EstablishedDate;
                farm.Description = model.Description;
                farm.LastUpdate = DateTime.UtcNow;
                await _uow.farmAdminRepo.UpdateAsync(farm);
                await _uow.SaveAsync();
            }
        }

        public string GenerateUniqueCode()
        {
            string code;
            do
            {
                code = GenerateRandomString(6);
            }
            while (IsCodeExists(code));

            return code;
        }

        private bool IsCodeExists(string code)
        {
            return _uow.farmAdminRepo.GetAll().Any(f => f.Code == code);
        }

        public async Task<string> GetLogoLinkByFarmId(int farmId)
        {
            Farm farm = await _uow.farmAdminRepo.GetByIdAsync(farmId);
            string logoLink = farm.Logo;
            return logoLink;
        }

        public async Task UpdateLogoLink(int farmId, string logoLink)
        {
            Farm farm = await _uow.farmAdminRepo.GetByIdAsync(farmId);
            farm.Logo = logoLink;
            await _uow.farmAdminRepo.UpdateAsync(farm);
            await _uow.SaveAsync();
        }

        public async Task<List<UserFarmAdminDTO>> GetUFFsByFarmId(string userId, int farmId)
        {
            //Lấy danh sách của technician trong farm và nhóm chúng thẹo theo userId để lấy được danh sách các chức năng của chúng
            var listUFF = await _uow.uffRepo.GetUFFsByFarmId(userId, farmId);
            List<UserFarmAdminDTO> result = new List<UserFarmAdminDTO>();
            var temp = listUFF.GroupBy(x => x.AspUserId)
                .Select(x => new
                {
                    tUserId = x.Key,
                    tUserName = x.First().AspUser.UserName,
                    tFullName = x.First().AspUser.FullName,
                    tPassword = x.First().AspUser.PasswordHash,
                    tUserAddress = x.First().AspUser.Address,
                    tUserEmail = x.First().AspUser.Email,
                    tUserPhone = x.First().AspUser.PhoneNumber,
                    tUserAvatar = x.First().AspUser.Avatar,
                    tUserStatus = x.First().AspUser.Status,
                    tDescription = x.First().AspUser?.Description,
                    tFunctionIds = x.Select(x => x.FunctionId).ToList(),
                }).ToList();
            if (temp.Count > 0)
            {
                foreach (var item in temp)
                {
                    UserFarmAdminDTO flag = new UserFarmAdminDTO();
                    flag.UserId = item.tUserId;
                    flag.UserName = item.tUserName;
                    flag.FullName = item.tFullName;
                    flag.Password = item.tPassword;
                    flag.UserAddress = item.tUserAddress;
                    flag.UserEmail = item.tUserEmail;
                    flag.UserPhone = item.tUserPhone;
                    flag.UserAvatar = item.tUserAvatar;
                    flag.UserStatus = item.tUserStatus;
                    flag.Description = item.tDescription;
                    flag.functionDTOs = item.tFunctionIds;

                    result.Add(flag);
                }
            }

            return result;
        }

        public async Task UpdateUserFunction(UpdateUFFDTO model)
        {
            var listFunctionId = model.FunctionIds.ToList();
            //Loại bỏ functionId = 1 vì nó là mặc định
            listFunctionId = listFunctionId.Where(x => x != 1).ToList();

            var listOldUFF = await _uow.uffRepo.GetAllAsync();
            //Lấy uff có functionId = 1 là mặc định
            UserFunctionFarm uffFirst = listOldUFF.FirstOrDefault(x => x.AspUserId.Equals(model.UserId));
            //Lấy list uff cũ của user truyền về và loại bỏ functionId = 1 đi vì nó là mặc định
            listOldUFF = listOldUFF.Where(x => x.AspUserId.Equals(model.UserId) && x.FunctionId != 1).ToList();
            if (listOldUFF.Count > 0 && uffFirst != null)
            {
                foreach (var item in listOldUFF)
                {
                    await _uow.uffRepo.DeleteAsync(item);
                    await _uow.SaveAsync();
                }
                //Ko add range đc
                //await _uow.uffRepo.DeleteRangeAsync(listOldUFF);
                //await _uow.SaveAsync();
            }

            //Thêm mới những thằng uff thay đổi được truyền về
            var listNewUFF = new List<UserFunctionFarm>();
            if (uffFirst != null)
            {
                foreach (var functionId in listFunctionId)
                {
                    UserFunctionFarm uff = new UserFunctionFarm();
                    uff.AspUserId = uffFirst.AspUserId;
                    uff.FarmId = uffFirst.FarmId;
                    uff.FunctionId = functionId;
                    listNewUFF.Add(uff);
                }
            }

            if (listNewUFF.Count > 0)
            {
                foreach (var item in listNewUFF)
                {
                    await _uow.uffRepo.AddAsync(item);
                    await _uow.SaveAsync();
                }
                //Ko add range đc
                //await _uow.uffRepo.AddRangeAsync(listNewUFF);
                //await _uow.SaveAsync();
            }
        }

        public string HashPassword(AspUser user, string password)
        {
            var hasher = new PasswordHasher<AspUser>();
            string hashedPassword = hasher.HashPassword(user, password);
            return hashedPassword;
        }

        public AspUser GenerateUserFarm(string farmCode)
        {
            AspUser aspUser = new AspUser();
            aspUser.Id = Guid.NewGuid().ToString();
            aspUser.Status = false;
            aspUser.UserName = GenerateUniqueUserName(farmCode).ToLower();                                    
            aspUser.NormalizedUserName = aspUser.UserName.ToLower();
            aspUser.Email = "";
            aspUser.EmailConfirmed = true;
            aspUser.NormalizedEmail = aspUser.Email.ToLower();
            aspUser.PhoneNumber = "";
            aspUser.Address = "";
            aspUser.FullName = "";
            aspUser.Description = "";
            aspUser.Avatar = "https://lh3.googleusercontent.com/d/1LtjBZGYa-Mn6n1D7n2WwXwLrRpeUIUkY";

            aspUser.PasswordHash = HashPassword(aspUser, GenerateRandomString(6).ToLower());

            return aspUser;
        }

        public string GenerateUniqueUserName(string farmCode)
        {
            string userName;
            do
            {
                userName = farmCode + "_" + GenerateRandomString(2);
            }
            while (IsUserNameExists(userName));

            return userName;
        }

        private bool IsUserNameExists(string userName)
        {
            return _uow.aspUserRepo.GetAll().Any(x => x.UserName.ToLower().Equals(userName.ToLower()));
        }

        private string GenerateRandomString(int num)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = new string(Enumerable.Repeat(chars, num)
                                            .Select(s => s[random.Next(s.Length)]).ToArray());

            // Kiểm tra nếu ký tự đầu tiên là số, thì thay thế nó bằng một ký tự chữ cái ngẫu nhiên
            if (char.IsDigit(result[0]))
            {
                // Thay thế ký tự đầu tiên bằng một ký tự chữ cái ngẫu nhiên
                char randomChar = (char)('A' + random.Next(0, 26));
                result = randomChar + result.Substring(1);
            }

            return result;
        }

        public async Task<string> ChangeStatusTechnician(string userId)
        {
            var users = await _uow.aspUserRepo.GetAllAsync();
            AspUser user = users.FirstOrDefault(x => x.Id.Equals(userId));
            string newPassWord = "";
            if (user != null)
            {
                //đang active thành inactive
                if (user.Status == true)
                {
                    user.Status = false;
                    await _uow.aspUserRepo.UpdateAsync(user);
                    await _uow.SaveAsync();
                }
                else
                {
                    string genPass = GenerateRandomString(6);   
                    user.Status = true;
                    user.PasswordHash = HashPassword(user, genPass.ToLower());
                    newPassWord = genPass.ToLower();
                    await _uow.aspUserRepo.UpdateAsync(user);
                    await _uow.SaveAsync();
                }
            }

            return newPassWord;
        }

        public async Task SoftDeleteFarm(int farmId)
        {

            var farm = await _uow.farmAdminRepo.GetByIdAsync(farmId);
            if (farm != null)
            {
                if (farm.Status == true)
                {
                    farm.Status = false;
                    await _uow.farmAdminRepo.UpdateAsync(farm);
                    await _uow.SaveAsync();
                }
                else
                {

                    farm.Status = true;
                    await _uow.farmAdminRepo.UpdateAsync(farm);
                    await _uow.SaveAsync();
                    
                }
            }
        }

        public async Task<Farm> GetFarmByUserId(string userId)
        {
            Farm farm = _uow.farmAdminRepo.GetFarmByUserIdAsync(userId);
            return farm;
        }

        public async Task<List<ImagesFarmDTO>> GetImagesFarmDTO(int farmId)
        {
            var list = await _uow.imageRepo.GetImagesFarm(farmId);
            List<ImagesFarmDTO> imagesFarmDTOs = new List<ImagesFarmDTO>();
            if (list.Count >= 5)
            {
                foreach (var item in list)
                {
                    ImagesFarmDTO model = new ImagesFarmDTO();
                    model.ImageId = item.Id;
                    model.ImageURL = item.URL;
                    model.FarmId = farmId;
                    imagesFarmDTOs.Add(model);
                }
            }

            return imagesFarmDTOs;
        }

        public async Task<string> GetImageURLByImageId(int imageId)
        {
            Image image = await _uow.imageRepo.GetByIdAsync(imageId);
            return image.URL;
        }

        public async Task UpdateImagesFarm(int imageId, string imageURL)
        {
            var image = await _uow.imageRepo.GetByIdAsync(imageId);    
            image.URL = imageURL;
            await _uow.imageRepo.UpdateAsync(image);
            await _uow.SaveAsync();
        }
    }
}