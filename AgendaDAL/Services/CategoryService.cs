using AgendaBLL.Models;
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
        private IMapper mapper { get; set; }

        public CategoryService(AgendaDbContext dbContext)
        {
            DbContext = dbContext;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>());
            mapper = new Mapper(config);

            AddInitialItems();
        }

        public void AddItem(CategoryDto item)
        {
            DbContext.Categories.Add(mapper.Map<Category>(item));
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
            return mapper.Map<List<Category>, List<CategoryDto>>(DbContext.Categories.ToList());
        }

        public CategoryDto GetItem(int id)
        {
            return mapper.Map<CategoryDto>(DbContext.Categories.FirstOrDefault(t => t.CategoryId == id));
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
                AddItem(new CategoryDto { Name = "Iskola", CategoryType = StateTypes.Checklist, ParentUserId = 1 });
                AddItem(new CategoryDto { Name = "Munka", CategoryType = StateTypes.Checklist, ParentUserId = 1 });
                AddItem(new CategoryDto { Name = "Bevásárlás", CategoryType = StateTypes.Checklist, ParentUserId = 1 });

            }
        }
    }
}
