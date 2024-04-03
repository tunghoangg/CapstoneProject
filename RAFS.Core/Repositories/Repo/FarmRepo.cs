using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class FarmRepo : GenericRepo<Farm>, IFarmRepo
    {
        public FarmRepo(RAFSContext context) : base(context)
        {

        }

        public Farm GetFarmDetailsInHomepage(int farmId)
        {
            Farm farm = new Farm();
            farm = _context.Farms.Include(i => i.Images)
                .Include(f => f.Milestones)
                .ThenInclude(m => m.Plants)
                .SingleOrDefault(x => x.Id == farmId);

            return farm;
        }

        public List<FarmImageDTO> GetFarmImg(int farmid)
        {
            List<FarmImageDTO> images = new List<FarmImageDTO>();
            images = _context.Images.Where(bt => bt.FarmId == farmid)
                             .Select(images => new FarmImageDTO
                             {
                                 Id = images.Id,
                                 URL = images.URL
                             })
                             .ToList();

            //List<String> url = _context.Images
            //                 .Where(bt => bt.FarmId == farmid)
            //                 .Select(bt => bt.URL)
            //                 .ToList();
            //return url;
            return images;
        }

        public List<FarmDetailPlantDTO> GetFarmPlants(int farmId)
        {
            List<FarmDetailPlantDTO> plants = new List<FarmDetailPlantDTO>();
            plants = _context.Plants
                .Where(plant => plant.Milestone.FarmId == farmId)
                .Select(plant => new FarmDetailPlantDTO
                {
                    Id = plant.Id,
                    Image = plant.Image,
                    Name = plant.Name,
                    Type = plant.Type
                })
                .ToList();
            return plants;
        }

        public List<Farm> GetFarmsInHomepage(string? search, string? province, string? district, string? ward, string? sort, string? area)
        {
            var listFarm = _context.Farms.AsQueryable();
            listFarm = listFarm.Where(f => f.Status == true);
            if (!string.IsNullOrEmpty(search))
            {
                listFarm = listFarm.Where(f => f.Name.ToLower().Contains(search.ToLower()) || f.Phone.Contains(search));
            }

            if (!string.IsNullOrEmpty(province))
            {
                listFarm = listFarm.Where(f => f.Address.ToLower().Contains(province.ToLower()));
            }

            if (!string.IsNullOrEmpty(province) && !string.IsNullOrEmpty(district) && !string.IsNullOrEmpty(ward))
            {
                listFarm = listFarm.Where(f => f.Address.ToLower().Contains(province.ToLower()) 
                && f.Address.ToLower().Contains(district.ToLower())
                && f.Address.ToLower().Contains(ward.ToLower()));
            }

            if (!string.IsNullOrEmpty(district))
            {
                listFarm = listFarm.Where(f => f.Address.ToLower().Contains(district.ToLower()));
            }

            if (!string.IsNullOrEmpty(ward))
            {
                listFarm = listFarm.Where(f => f.Address.ToLower().Contains(ward.ToLower()));
            }

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "asc": 
                        listFarm = listFarm.OrderBy(f => f.Name); 
                        break;
                    case "des":
                        listFarm = listFarm.OrderByDescending(f => f.Name);
                        break;
                    case "old":
                        listFarm = listFarm.OrderBy(f => f.EstablishedDate);
                        break;
                    case "new":
                        listFarm = listFarm.OrderByDescending(f => f.EstablishedDate);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(area))
            {
                switch (area)
                {
                    case "< 5ha":
                        listFarm = listFarm.Where(f => f.Area <= 5);
                        break;
                    case "> 5ha":
                        listFarm = listFarm.Where(f => f.Area > 5 && f.Area <= 15);
                        break;
                    case "> 15ha":
                        listFarm = listFarm.Where(f => f.Area > 15);
                        break;
                }
            }

            return listFarm.ToList();
        }

        public List<Farm> GetNewestFarms()
        {
            var listFarm = _context.Farms.AsQueryable();
            listFarm = listFarm.OrderByDescending(f => f.EstablishedDate).Where(f => f.Status).Take(4);
            return listFarm.ToList();
        }
    }
}
