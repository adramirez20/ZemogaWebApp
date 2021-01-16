using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using WebPosterApp.Entities;
using WebPosterApp.Services;

namespace WebPosterApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserService _userService;

        public LoginController(IUserService UserService)
        {
            _userService = UserService;
        }

        // GET: PosterWebController/Create
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            
            
            return View(new User());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User obj)
        {
            if (!ModelState.IsValid)
            {
                return View(User);
            }
            var user = _userService.Authenticate(obj.Username, obj.Password);
            
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "PosterWeb", action = "Index" }));

        }

    }
}
