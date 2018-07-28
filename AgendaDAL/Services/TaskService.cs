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

        private IMapper mapper { get; set; }

        public TaskService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.Task, TaskDto>());
            mapper = new Mapper(config);
            AddInitialItems();
        }

        public void AddItem(TaskDto item)
        {
            DbContext.Tasks.Add(mapper.Map<Models.Task>(item));
            DbContext.SaveChanges();

        }

        public void DeleteItem(TaskDto item)
        {
            var result = DbContext.Tasks.SingleOrDefault(t => t.TaskId == item.TaskId);
            DbContext.Remove(result);
            DbContext.SaveChanges();
        }

        public List<TaskDto> GetAllItem()
        {
            return mapper.Map<List<Models.Task>, List<TaskDto>>(DbContext.Tasks.ToList());
        }

        public TaskDto GetItem(int id)
        {
            return mapper.Map<TaskDto>(DbContext.Tasks.FirstOrDefault(t => t.TaskId == id));
        }

        public void UpdateItem(TaskDto item)
        {
            var result = DbContext.Tasks.SingleOrDefault(t => t.TaskId == item.TaskId);
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
                AddItem(new TaskDto { Name = "Alma", Description = "1 kg", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });
                AddItem(new TaskDto { Name = "Banán", Description = "2 kg", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });
                AddItem(new TaskDto { Name = "Mangó", Description = "3 db", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });

            }
        }
    }
}