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

        private IMapper DbMapper { get; set; }

        public UserService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            DbMapper = new Mapper(config);
            AddInitialItems();
        }

        public UserDto Authenticate(string email, string password)
        {
            try
            {
                var user = DbMapper.Map<UserDto>(DbContext.Users.FirstOrDefault(t => t.Email == email && t.PasswordHash == password));

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
            try
            {
                var existingUser = DbMapper.Map<UserDto>(DbContext.Users.FirstOrDefault(u => u.Email == item.Email));
                if (existingUser == null)
                {
                    DbContext.Users.Add(DbMapper.Map<User>(item));
                    DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteItem(UserDto item)
        {
            try
            {
                var result = DbContext.Users.SingleOrDefault(u => u.UserId == item.UserId);
                DbContext.Remove(result);
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<UserDto> GetAllItem()
        {
            try
            {
                return DbMapper.Map<List<User>, List<UserDto>>(DbContext.Users.ToList());
            }
            catch
            {
                return null;
            }
        }

        public UserDto GetItem(int id)
        {
            try
            {
                return DbMapper.Map<UserDto>(DbContext.Users.FirstOrDefault(u => u.UserId == id));
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateItem(UserDto item)
        {
            try
            {
                var result = DbContext.Users.SingleOrDefault(u => u.UserId == item.UserId);
                if (result != null)
                {
                    DbContext.Entry(result).CurrentValues.SetValues(item);
                    DbContext.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
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