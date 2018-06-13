using AgendaDAL.Dtos;
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

        public CategoryService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDto>());

            AddInitialItems();            
        }
        
        public void AddItem(CategoryDto item)
        {
            DbContext.Categories.Add(Mapper.Map<Category>(item));
            DbContext.SaveChanges();
        }

        public void DeleteItem(CategoryDto item)
        {
            var result = DbContext.Categories.SingleOrDefault(c => c.CategoryId == item.CategoryId);
            DbContext.Remove(result);
            DbContext.SaveChanges();
        }

        public List<CategoryDto> GetAllItem()
        {            
            return Mapper.Map<List<Category>, List<CategoryDto>>(DbContext.Categories.ToList());
        }

        public CategoryDto GetItem(int id)
        {
            return Mapper.Map<CategoryDto>(DbContext.Categories.FirstOrDefault(t => t.CategoryId == id));
        }

        public void UpdateItem(CategoryDto item)
        {
            var result = DbContext.Categories.SingleOrDefault(c => c.CategoryId == item.CategoryId);
            if (result != null)
            {
                DbContext.Entry(result).CurrentValues.SetValues(item);
                DbContext.SaveChanges();
            }
        }

        private void AddInitialItems()
        {
            if (DbContext.Categories.Count() == 0)
            {
                AddItem(new CategoryDto { Name = "Iskola", CategoryType = 1, ParentUserId = 1 });
                AddItem(new CategoryDto { Name = "Munka", CategoryType = 1, ParentUserId = 1 });
                AddItem(new CategoryDto { Name = "Bevásárlás", CategoryType = 1, ParentUserId = 1 });

            }
        }
    }
}
