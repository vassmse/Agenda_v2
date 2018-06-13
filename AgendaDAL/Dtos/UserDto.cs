using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public DateTime RegisterDate { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
