using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebPosterDomain.Entities;
using WebPosterDomain.Interfaces.Poster;
using WebPosterInfrastructure.Configuration;
using WebPosterInfrastructure.Repository.Generic;

namespace WebPosterInfrastructure.Repository.Common
{
    public class PosterRepo : GenericRepo<Poster>, IPoster
    {
        private readonly DbContextOptions<Context> _Options;
        private readonly DbContextOptionsBuilder<Context> _OptionsBuilder;

        public PosterRepo()
        {
            _Options = new DbContextOptions<Context>();
            _OptionsBuilder = new DbContextOptionsBuilder<Context>();
        }

        public List<Comment> GetByPosterID(int id)
        {
            using (var obj = new Context(_Options))
            {
                return obj.Set<Comment>().AsNoTracking().Where(x => x.PosterID == id).ToList();
            }
        }

        public void UpdateState(int posterID, int statusid) {
            using (var obj = new Context(_OptionsBuilder.Options)) {
                var param = new SqlParameter("@PosterID", posterID);
                var param2 = new SqlParameter("@StatusID", statusid);
                obj.Database.ExecuteSqlRaw("[Core].[UpdateState] @PosterID, @StatusID", param, param2);
                obj.SaveChanges();
            }
        }
    }
}
