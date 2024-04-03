using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface ISendMailService
    {
        Task sendMailAsync(string toAddress, string subject, string body);
    }
}
