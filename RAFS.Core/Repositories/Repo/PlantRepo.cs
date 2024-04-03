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
    public class PlantRepo : GenericRepo<Plant>, IPlantRepo
    {

        public PlantRepo(RAFSContext context) : base(context)
        {
        }

        public int CreatePlant(Plant milestoneinfor)
        {
            try
            {

                milestoneinfor.QRCode = "https://localhost:7043/QR?plantid=";
                milestoneinfor.Status = true;
                milestoneinfor.LastUpdate = DateTime.Now;
                _context.Plants.Add(milestoneinfor);
                _context.SaveChanges();

                // Now, milestoneinfor.Id will contain the ID assigned by the database
                int createdObjectId = milestoneinfor.Id;
                return createdObjectId;
            }
            catch
            {
                return 0;
            }
        }

        public bool DeletePlant(int milestoneid)
        {
            try {

                Plant milestones = GetPlantById(milestoneid);
                milestones.LastUpdate = DateTime.Now;
                milestones.Status = false;
               _context.Plants.Update(milestones);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public List<Plant> GetPlants()
        {
            List<Plant> milestones = new List<Plant>();
            milestones = _context.Plants.Where(x=>x.Status==true).ToList();


            return milestones;
        }

        public Plant GetPlantById(int milestoneid)
        {
            Plant milestones = new Plant();
            milestones = _context.Plants.FirstOrDefault(x=> x.Id == milestoneid);


            return milestones;
        }

        public bool UpdatePlant(Plant milestoneinfor)
        {
            try
            {
     
                milestoneinfor.Status = true;
                milestoneinfor.LastUpdate = DateTime.Now;
                _context.Plants.Update(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PlantRecordDTO> GetPlantDiaries(int plantid)
        {
            List<PlantRecordDTO> finalresult = new List<PlantRecordDTO>();

            List<Item> items = _context.Inventories.ToList();
            List<Plant> plants = _context.Plants.ToList();
            List<Unit> units = _context.Units.ToList();

            List<Diary> diaries = _context.Diaries.Where(x => x.PlantId == plantid  && x.Status == true ).ToList();
            List<TakeAndSendMaterial> diaries2 = _context.TakeAndSendMaterials.Where(x => x.PlantId == plantid && x.Status == true).ToList();

            if (diaries.Count > 0)
            {
                foreach (var diary in diaries)
                {
                    var typereport = "";
                    PlantRecordDTO plantRecordDTO = new PlantRecordDTO();

                    if (diary.Type == 4)
                    {
                        typereport = "Ngày tạo";
                        plantRecordDTO.type = 1;
                        var descrip = typereport + ": " + diary.LastUpdate + " Cây: " + plants.FirstOrDefault(x=>x.Id == diary.PlantId).Name +" đã được tạo";

                        plantRecordDTO.Description = descrip;
                        plantRecordDTO.createdate = diary.LastUpdate;

                        finalresult.Add(plantRecordDTO);
                    }
                    if (diary.Type == 5)
                    {
                        typereport = "Ngày hủy";
                        plantRecordDTO.type = 2;
                        var descrip = typereport + ": " + diary.CreatedDay + " Cây: " + plants.FirstOrDefault(x => x.Id == diary.PlantId).Name + " đã bị hủy"; ;

                        plantRecordDTO.Description = descrip;
                        plantRecordDTO.createdate = diary.CreatedDay;

                        finalresult.Add(plantRecordDTO);
                    }

                   

                }
            }

            if (diaries2.Count > 0)
            {
                foreach (var diary in diaries2)
                {
                   


                    var typereport = "";
                    var action = "";
                    var typer = 0;
                    var unit = units.FirstOrDefault(x => x.Id == (items.FirstOrDefault(x => x.Id == diary.InventoryId)).UnitId).Name;
                    if (items.FirstOrDefault(x => x.Id == diary.InventoryId).TypeId == 10)
                    {
                        typereport = "Báo cáo thuốc";
                        action = " đã dùng";
                        typer =3;
                    }
                    if (items.FirstOrDefault(x => x.Id == diary.InventoryId).TypeId == 12)
                    {
                        action = " đã dùng";
                        typereport = "Báo cáo phân";
                        typer = 4;
                    }
                    if (items.FirstOrDefault(x => x.Id == diary.InventoryId).TypeId == 18)
                    {
                        action = " thu hoạch được";
                        typereport = "Báo cáo sản lượng";
                        typer = 5;
                    }

                    var descrip = typereport + " Thời gian: " + diary.LastUpdate + action+" Số lượng: " + diary.Quality+" đơn vị " + unit;
                    PlantRecordDTO plantRecordDTO = new PlantRecordDTO();
                    plantRecordDTO.Description = descrip;
                    plantRecordDTO.type = typer;
                    plantRecordDTO.createdate = diary.LastUpdate;
                    finalresult.Add(plantRecordDTO);
                }
            }
            return finalresult.OrderByDescending(x=>x.createdate).ToList();
        }
    }
}
