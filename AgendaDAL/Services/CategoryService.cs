using AgendaCON.Models;
using AgendaDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Services
{
    public class CategoryService : IService<CategoryDto>
    {
        private AgendaDbContext DbContext { get; set; }
        private IMapper DbMapper { get; set; }

        public CategoryService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>());
            DbMapper = new Mapper(config);
            AddInitialItems();
        }

        public bool AddItem(CategoryDto item)
        {
            try
            {
                DbContext.Categories.Add(DbMapper.Map<Category>(item));
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteItem(CategoryDto item)
        {
            try
            {
                var result = DbContext.Categories.SingleOrDefault(c => c.CategoryId == item.CategoryId);
                DbContext.Remove(result);
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<CategoryDto> GetAllItem()
        {
            try
            {
                return DbMapper.Map<List<Category>, List<CategoryDto>>(DbContext.Categories.ToList());
            }
            catch
            {
                return null;
            }
        }

        public CategoryDto GetItem(int id)
        {
            try
            {
                return DbMapper.Map<CategoryDto>(DbContext.Categories.FirstOrDefault(t => t.CategoryId == id));
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateItem(CategoryDto item)
        {
            try
            {
                var result = DbContext.Categories.SingleOrDefault(c => c.CategoryId == item.CategoryId);
                if (result != null)
                {
                    DbContext.Entry(result).CurrentValues.SetValues(item);
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

        private void AddInitialItems()
        {
            if (DbContext.Categories.Count() == 0)
            {
                // Initial categories for Test user 1
                AddItem(new CategoryDto { Name = "School", CategoryType = CategoryTypes.MultiChecklist, ParentUserId = 1 });
                AddItem(new CategoryDto { Name = "Work", CategoryType = CategoryTypes.Kanban3, ParentUserId = 1 });
                AddItem(new CategoryDto { Name = "Shopping list", CategoryType = CategoryTypes.Checklist, ParentUserId = 1 });

                // Initial categories for Test user 2
                AddItem(new CategoryDto { Name = "Shopping", CategoryType = CategoryTypes.Checklist, ParentUserId = 2 });
                AddItem(new CategoryDto { Name = "Exercising", CategoryType = CategoryTypes.MultiChecklist, ParentUserId = 2 });
                AddItem(new CategoryDto { Name = "Project", CategoryType = CategoryTypes.Kanban5, ParentUserId = 2 });
            }
        }
    }
}
