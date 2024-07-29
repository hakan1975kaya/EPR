using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Entities
{
    public class Menu: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LinkedMenuId { get; set; }
        public string Path { get; set; }
        public int MenuOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
