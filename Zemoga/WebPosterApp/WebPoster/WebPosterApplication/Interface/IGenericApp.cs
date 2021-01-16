using System.Collections.Generic;

namespace WebPosterApplication.Interface
{
    public interface IGenericApp<T> where T : class
    {
        void Add(T Object);
        void Update(T Object);
        void Delete(T Object);
        T GetByID(int Id);
        IEnumerable<T> GetAll();

    }
}
