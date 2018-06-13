using AgendaDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDAL.Services
{
    interface IService<T>
    {
        List<T> GetAllItem();

        T GetItem(int id);

        void AddItem(T item);

        void DeleteItem(T item);

        void UpdateItem(T item);

    }
}
