using AgendaCON.Models;
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

        bool AddItem(T item);

        bool DeleteItem(T item);

        bool UpdateItem(T item);

    }
}
