using AgendaCON.Models;
using AgendaDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Services
{
    public class TaskService : IService<TaskDto>
    {
        private AgendaDbContext DbContext { get; set; }

        private IMapper DbMapper { get; set; }

        public TaskService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.Task, TaskDto>());
            DbMapper = new Mapper(config);
        }

        public bool AddItem(TaskDto item)
        {
            try
            {
                DbContext.Tasks.Add(DbMapper.Map<Models.Task>(item));
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteItem(TaskDto item)
        {
            try
            {
                var result = DbContext.Tasks.SingleOrDefault(t => t.TaskId == item.TaskId);
                DbContext.Remove(result);
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TaskDto> GetAllItem()
        {
            try
            {
                return DbMapper.Map<List<Models.Task>, List<TaskDto>>(DbContext.Tasks.ToList());
            }
            catch
            {
                return null;
            }
        }

        public TaskDto GetItem(int id)
        {
            try
            {
                return DbMapper.Map<TaskDto>(DbContext.Tasks.FirstOrDefault(t => t.TaskId == id));
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateItem(TaskDto item)
        {
            try
            {
                var result = DbContext.Tasks.SingleOrDefault(t => t.TaskId == item.TaskId);
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
    }
}