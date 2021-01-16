using System.Collections.Generic;
using WebPosterDomain.Entities;

namespace WebPosterApplication.Interface
{
    public interface IAppPoster : IGenericApp<Poster>
    {
        IEnumerable<Comment> GetByPosterID(int id);
        void UpdateState(int posterID, int statusid);
    }
}
