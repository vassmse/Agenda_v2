using AgendaCON.Models;
using AgendaDAL.Models;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDAL.Services
{
    public class UserService : IService<UserDto>
    {
        private AgendaDbContext DbContext { get; set; }

        private IMapper mapper { get; set; }

        public UserService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            mapper = new Mapper(config);
            AddInitialItems();
        }

        public UserDto Authenticate(string email, string password)
        {
            try
            {
                var user = mapper.Map<UserDto>(DbContext.Users.FirstOrDefault(t => t.Email == email && t.PasswordHash == password));

                if (user == null)
                    return null;


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("Security key for Users' password");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                user.PasswordHash = null;

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool AddItem(UserDto item)
        {
            var existingUser = mapper.Map<UserDto>(DbContext.Users.FirstOrDefault(u => u.Email == item.Email));
            if (existingUser == null)
            {
                DbContext.Users.Add(mapper.Map<User>(item));
                DbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void DeleteItem(UserDto item)
        {
            var result = DbContext.Users.SingleOrDefault(u => u.UserId == item.UserId);
            DbContext.Remove(result);
            DbContext.SaveChanges();
        }

        public List<UserDto> GetAllItem()
        {
            return mapper.Map<List<User>, List<UserDto>>(DbContext.Users.ToList());
        }

        public UserDto GetItem(int id)
        {
            return mapper.Map<UserDto>(DbContext.Users.FirstOrDefault(u => u.UserId == id));
        }

        public void UpdateItem(UserDto item)
        {
            var result = DbContext.Users.SingleOrDefault(u => u.UserId == item.UserId);
            if (result != null)
            {
                DbContext.Entry(result).CurrentValues.SetValues(item);
                DbContext.SaveChanges();
            }
        }

        private void AddInitialItems()
        {
            if (DbContext.Users.Count() == 0)
            {
                AddItem(new UserDto { Email = "a", PasswordHash = "a" });
                AddItem(new UserDto { Email = "b", PasswordHash = "b" });
                AddItem(new UserDto { Email = "c", PasswordHash = "c" });

            }
        }
    }
}