using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Farm
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

        public virtual IList<Employee>? Employees { get; set; }
        public virtual IList<Milestone>? Milestones { get; set; }
        public virtual IList<CashFlow>? CashFlows { get; set; }
        public virtual IList<UserFunctionFarm>? UserFunctionFarms { get; set; }
        public virtual IList<Image>? Images { get; set; }
        //public virtual Inventory? Inventory { get; set; }
        public virtual IList<Item>? Inventories { get; set; }


    }
}
