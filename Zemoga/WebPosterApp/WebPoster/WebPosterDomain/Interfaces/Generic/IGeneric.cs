using System;
using System.Collections.Generic;
using System.Text;

namespace WebPosterDomain.Interfaces.Generic
{
    public interface IGeneric<T> where T : class
    {
        void Add(T Object);
        void Update(T Object);
        void Delete(T Object);
        T GetByID(int Id);
        List<T> GetAll();

    }
}
