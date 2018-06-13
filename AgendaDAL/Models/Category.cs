using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        
        public string Name { get; set; }

        public int CategoryType { get; set; }

        public List<Task> Tasks { get; set; }

        public int ParentUserId { get; set; }
    }
}
