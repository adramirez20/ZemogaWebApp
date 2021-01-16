using System.Collections.Generic;
using WebPosterDomain.Entities;
using WebPosterDomain.Interfaces.Generic;

namespace WebPosterDomain.Interfaces.Poster
{
    public interface IPoster : IGeneric<WebPosterDomain.Entities.Poster>
    {
        public List<Comment> GetByPosterID(int id);

        void UpdateState(int posterID, int statusid);
    }
}
