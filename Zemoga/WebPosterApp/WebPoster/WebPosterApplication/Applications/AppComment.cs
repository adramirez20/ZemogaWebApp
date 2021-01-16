using System.Collections.Generic;
using WebPosterApplication.Interface;
using WebPosterDomain.Entities;
using WebPosterDomain.Interfaces.Common;

namespace WebPosterApplication.Applications
{
    public class AppComment : IAppComment
    {
        IComment _IComment;

        public AppComment(IComment IComment)
        {
            _IComment = IComment;
        }
        public void Add(Comment Object)
        {
            _IComment.Add(Object);
        }

        public void Delete(Comment Object)
        {
            _IComment.Delete(Object);
        }

        public IEnumerable<Comment> GetAll()
        {
            return (IEnumerable<Comment>)_IComment.GetAll();
        }

        public Comment GetByID(int id)
        {
           return _IComment.GetByID(id);
        }

        public void Update(Comment Object)
        {
            _IComment.Update(Object);
        }
    }
}
