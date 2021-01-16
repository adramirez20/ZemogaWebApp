using System;
using System.Collections.Generic;
using WebPosterApplication.Interface;
using WebPosterDomain.Entities;
using WebPosterDomain.Enums;
using WebPosterDomain.Interfaces.Common;
using WebPosterDomain.Interfaces.Poster;

namespace WebPosterApplication.Applications
{
    public class AppPoster : IAppPoster
    {
        IPoster _IPoster;

        public AppPoster(IPoster IPoster)
        {
            _IPoster = IPoster;
        }

        public void Add(Poster Object)
        {
            Object.StatusID = Convert.ToInt32(Status.Pending);
            Object.Active = true;
            Object.CreatedDate = DateTime.Now;
            //TODO: Get User ID
            Object.CreatedByUserID = 1;
            _IPoster.Add(Object);
        }

        public void Delete(Poster Object)
        {
            //_IPoster.Delete(Object);
            Object.Active = false;
            _IPoster.Update(Object);
        }

        public IEnumerable<Poster> GetAll()
        {
            return (IEnumerable<Poster>)_IPoster.GetAll().FindAll(x => x.StatusID == Convert.ToInt32(Status.Approved));

        }

        public Poster GetByID(int id)
        {
            var post = _IPoster.GetByID(id);
            if (post != null)
            {
                post.Writer = "User writer 1";
                post.Comments = _IPoster.GetByPosterID(id);
            }
            return post;
        }

        public IEnumerable<Comment> GetByPosterID(int id)
        {
            return (IEnumerable<Comment>)_IPoster.GetByPosterID(id);
        }

        public void Update(Poster Object)
        {
            _IPoster.Update(Object);
        }

        public void UpdateState(int posterID, int statusid)
        {
            _IPoster.UpdateState(posterID, statusid);
        }
    }
}
