using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebPosterApplication.Interface;
using WebPosterDomain.Entities;

namespace WebPosterApp.Controllers
{
    public class CommentWebController : Controller
    {
        private readonly IAppComment _AppComment;
        public CommentWebController(IAppComment AppComment)
        {
            _AppComment = AppComment;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        [AllowAnonymous]
        public ActionResult PostComment(int id = 0)
        {
            var post = _AppComment.GetByID(id);
            return View(new Comment { PosterID = id });
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostComment(int id, Comment comment)
        {
            try
            {
                Comment addComment = new Comment();
                addComment.PosterID = id;
                addComment.Description = comment.Description;
                addComment.AddedDate = DateTime.Now;
                _AppComment.Add(addComment);
                return RedirectToAction("Details", "PosterWeb", new { id = id });
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}
