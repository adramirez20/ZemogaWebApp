using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebPosterApp.Entities;
using WebPosterApplication.Interface;
using WebPosterDomain.Entities;
using WebPosterDomain.Enums;

namespace WebPosterApp.Controllers
{

    public class PosterWebController : Controller
    {
        private readonly IAppPoster _AppPoster;
        public PosterWebController(IAppPoster AppPoster)
        {
            _AppPoster = AppPoster;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_AppPoster.GetAll());
        }

        // GET: PostWebController/Details/5
        [HttpGet]
        [AllowAnonymous]
        [Route("PosterWeb/Details")]
        public ActionResult Details(int id)
        {
            return View(_AppPoster.GetByID(id));
        }

        // GET: PosterWebController/Create
        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Writer)]
        public ActionResult Create()
        {
            return View(new Poster());
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("PosterWeb/Create")]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Writer)]
        public ActionResult Create(Poster collection)
        {
            try
            {
                _AppPoster.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
         

        // GET: PostWebController/Delete/5
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult Delete(int id)
        {

            return View(_AppPoster.GetByID(id));
        }

        // POST: PostWebController/Delete/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult Delete(int id, Poster collection)
        {
            try
            {
                _AppPoster.Delete(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostWebController/RejectPost/5
        /// <summary>
        /// Reject Poster a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>        
        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult RejectPoster(int id)
        {
            //return View(AppProduto.ObterPorId(id));
            return View(_AppPoster.GetByID(id));
        }

        // POST: PostWebController/RejectPoster/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult RejectPoster(int id, Poster collection)
        {
            try
            {
                _AppPoster.UpdateState(id, Convert.ToInt32(Status.Rejected));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostWebController/ApprovePoster/5
        /// <summary>
        /// Approve Poster a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult ApprovePoster(int id)
        {
            //return View(AppProduto.ObterPorId(id));
            return View(_AppPoster.GetByID(id));
        }

        // POST: PostWebController/ApprovePoster/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Admin)]
        [Authorize(Roles = Role.Editor)]
        public ActionResult ApprovePoster(int id, Poster collection)
        {
            try
            {
                _AppPoster.UpdateState(id, Convert.ToInt32(Status.Approved));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
