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
            AddInitialItems();
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

        private void AddInitialItems()
        {
            if (DbContext.Tasks.Count() == 0)
            {
                AddItem(new TaskDto { Name = "History", Description = "", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });
                AddItem(new TaskDto { Name = "Essay", Description = "", State = 0, HasDeadlineDate = true, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1, ParentTaskId = 1, IsSubTask = true });
                AddItem(new TaskDto { Name = "Learning", Description = "", State = 0, HasDeadlineDate = true, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1, ParentTaskId = 1, IsSubTask = true });
                AddItem(new TaskDto { Name = "Art", Description = "", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });
                AddItem(new TaskDto { Name = "Biology", Description = "", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });
                AddItem(new TaskDto { Name = "Phisics", Description = "", State = 0, HasDeadlineDate = true, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });
                AddItem(new TaskDto { Name = "Math", Description = "3 db", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 1 });

                AddItem(new TaskDto { Name = "Apple", Description = "1 kg", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 2 });
                AddItem(new TaskDto { Name = "Banana", Description = "2 kg", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 2 });
                AddItem(new TaskDto { Name = "Mango", Description = "3 db", State = 0, DeadlineDate = DateTime.Now.AddDays(2), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 2 });
                AddItem(new TaskDto { Name = "Task1", Description = "", State = 0, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 4 });
                AddItem(new TaskDto { Name = "Task2", Description = "", State = 1, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 4 });
                AddItem(new TaskDto { Name = "Task3", Description = "", State = 2, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 4 });
                AddItem(new TaskDto { Name = "Task4", Description = "", State = 4, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 4 });
                AddItem(new TaskDto { Name = "Task5", Description = "", State = 4, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 4 });

                AddItem(new TaskDto { Name = "Task4", Description = "", State = 4, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 5 });
                AddItem(new TaskDto { Name = "Task5", Description = "", State = 4, DeadlineDate = DateTime.Now.AddDays(1), ScheduledDate = DateTime.Now.AddDays(1), ParentCategoryId = 6 });

            }
        }
    }
}