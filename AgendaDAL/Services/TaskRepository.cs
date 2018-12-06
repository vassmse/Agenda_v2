using AgendaCON.Models;
using AgendaDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Services
{
    public class TaskRepository : IRepository<TaskDto>
    {
        private AgendaDbContext DbContext { get; set; }

        private IMapper DbMapper { get; set; }

        public TaskRepository(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            // Configurate the mapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.Task, TaskDto>());
            DbMapper = new Mapper(config);
        }

        // Adding new task to DB
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

        // Delete task from DB
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

        // Get all tasks from DB
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

        // Returns a task from DB
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

        // Updating the task
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