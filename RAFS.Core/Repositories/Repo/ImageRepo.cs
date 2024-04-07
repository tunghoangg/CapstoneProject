using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class ImageRepo : GenericRepo<Image>, IImageRepo
    {
        public ImageRepo(RAFSContext context) : base(context)
        {
        }

        public async Task<List<Image>> GetImagesFarm(int farmId)
        {
            var list = await _context.Images.Where(x => x.FarmId == farmId).Take(5).ToListAsync();

            return list;
        }
    }
}
