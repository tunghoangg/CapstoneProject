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
    public class AspUserRepo : GenericRepo<AspUser>, IAspUserRepo
    {
        public AspUserRepo(RAFSContext context) : base(context)
        {

        }
    }
}
