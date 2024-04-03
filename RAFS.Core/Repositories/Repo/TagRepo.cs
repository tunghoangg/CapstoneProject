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
    public class TagRepo: GenericRepo<Tag>, ITagRepo
    {
        public TagRepo(RAFSContext context) : base(context)
        {

        }
        public bool tagCheck(string tagName)
        {
           if(_context.Tags.FirstOrDefault(t => t.Name.ToLower() == tagName.ToLower())!=null)
                return true;
           else
            return false;
        }
        public Tag getTagByName(string tagName) { 
            return _context.Tags.FirstOrDefault(t => t.Name == tagName);
        }

        public Tag getLastTag()
        {
            if (_context.Tags.Any())
            {
                return _context.Tags.OrderBy(tag => tag.Id).Last();
            }
            else
            {
                // Xử lý khi danh sách rỗng, ví dụ:
                return null; // hoặc giá trị mặc định phù hợp với kiểu dữ liệu của bạn
            }
        }
    }
}
