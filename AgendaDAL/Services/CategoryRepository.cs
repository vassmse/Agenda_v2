using AgendaCON.Models;
using AgendaDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Services
{

    public class CategoryRepository : IRepository<CategoryDto>
    {
        private AgendaDbContext DbContext { get; set; }
        private IMapper DbMapper { get; set; }

        public CategoryRepository(AgendaDbContext dbContext)
        {
            DbContext = dbContext;

            // Configure the mapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>());
            DbMapper = new Mapper(config);
        }

        // Add new category to the database
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

        // Delete a category
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

        // Returns all the categories
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

        // Returns the category
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

        // Update a category in the database
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
    }
}
