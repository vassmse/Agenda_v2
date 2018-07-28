using AgendaCON.Models;
using AgendaDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddItem(UserDto item)
        {
            DbContext.Users.Add(mapper.Map<User>(item));
            DbContext.SaveChanges();
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
            if (DbContext.Tasks.Count() == 0)
            {
                AddItem(new UserDto { Name = "testUser", RegisterDate = DateTime.Now, Email = "test@test.com", PasswordHash = "asdasd" });
                AddItem(new UserDto { Name = "testUser2", RegisterDate = DateTime.Now, Email = "test2@test.com", PasswordHash = "asda" });

            }
        }
    }
}