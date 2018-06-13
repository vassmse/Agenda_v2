using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int CategoryType { get; set; }

        public List<TaskDto> Tasks { get; set; }

        public int ParentUserId { get; set; }
    }
}
