using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPosterDomain.Interfaces.Generic;
using WebPosterInfrastructure.Configuration;

namespace WebPosterInfrastructure.Repository.Generic
{
    public class GenericRepo<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<Context> _OptionsBuilder;

        public GenericRepo()
        {
            _OptionsBuilder = new DbContextOptions<Context>();
        }

        public void Add(T Objeto)
        {
            using (var obj = new Context(_OptionsBuilder))
            {
                obj.Set<T>().Add(Objeto);
                obj.SaveChanges();
            }
        }

        public void Update(T Objeto)
        {
            using (var obj = new Context(_OptionsBuilder))
            {
                obj.Set<T>().Update(Objeto);
                obj.SaveChangesAsync();
            }
        }

        public void Delete(T Objeto)
        {
            using (var obj = new Context(_OptionsBuilder))
            {
                obj.Set<T>().Remove(Objeto);
                obj.SaveChangesAsync();
            }
        }

        public List<T> GetAll()
        {
            using (var obj = new Context(_OptionsBuilder))
            {
                return obj.Set<T>().AsNoTracking().ToList();
            }
        }

        public T GetByID(int Id)
        {
            using (var obj = new Context(_OptionsBuilder))
            {
                return obj.Set<T>().Find(Id);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
