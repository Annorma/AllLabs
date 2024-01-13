using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10.DAL
{
    public interface IRepository<T> where T : new()
    {
        List<T> Select(params Tuple<string, string, object, string>[] filters);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool IsEntityExitsInDb(T entity);
    }

}
