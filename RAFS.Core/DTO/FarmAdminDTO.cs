using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class FarmAdminDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public string PageLink { get; set; }
        public double Area { get; set; }
        public DateTime EstablishedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class AddFarmAdminDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PageLink { get; set; }
        public double Area { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string Description { get; set; }
    }

    public class UpdateFarmAdminDTO
    {
        public int FarmId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PageLink { get; set; }
        public double Area { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string Description { get; set; }
    }

    public class UpdateImageFarmAdminDTO
    {
        public int FarmId { get; set; }
        public IFormFile ImageFile { get; set; }
    }


    public class UserFarmAdminDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserAvatar { get; set; }
        public string Description { get; set; }
        public bool UserStatus { get; set; }
        public List<int>? functionDTOs { get; set; }

    }

    public class UpdateUFFDTO
    {
        public string UserId { get; set; }
        public List<int>? FunctionIds { get; set; }
    }

    public class ImagesFarmDTO { 
        public int ImageId { get; set; }
        public string? ImageURL { get; set; }
        public int FarmId { get; set; }
    }

    public class UpdateImagesFarmnDTO
    {
        public int ImageId { get; set; }
        public IFormFile ImageFile { get; set; }
    }

}