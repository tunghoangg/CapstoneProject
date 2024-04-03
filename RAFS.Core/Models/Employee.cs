using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Status { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
    }
}
