using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public DateTime RegisterDate { get; set; }

        public List<Category> Categories { get; set; }
    }
}