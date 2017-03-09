using Microsoft.Owin.Security;
using MvcCms.Areas.Admin.ViewModels;
using MvcCms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCms.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUserRepository _users;

        public AdminController() : this(new UserRepository()) { }

        public AdminController(IUserRepository userRepository)
        {
            _users = userRepository;
        }

        // GET: Admin/Admin
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var user = await _users.GetLoginUserAsync(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "The user with the supplied credentials does not exist");
            }

            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = await _users.CreateIdentityAsync(user);

            authManager.SignIn(
                new AuthenticationProperties() { IsPersistent = model.RememberMe }, userIdentity);


            return RedirectToAction("index");
        }

        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;

            authManager.SignOut();

            return RedirectToAction("index", "home", new { area = "" });
        }

        [AllowAnonymous]
        public async Task<PartialViewResult> AdminMenu()
        {
            var items = new List<AdminMenuItem>();

            if (User.Identity.IsAuthenticated)
            {
                items.Add(new AdminMenuItem
                {
                    Text = "Dashboard",
                    Icon = "fa fa-dashboard nav-icon",
                    Action = "index",
                    RouteInfo = new { controller = "admin", area = "admin" }
                });

                if (User.IsInRole("admin"))
                {
                    items.Add(new AdminMenuItem
                    {
                        Text = "Users",
                        Icon = "fa fa-check-square-o nav_icon",
                        Action = "index",
                        RouteInfo = new { controller = "user", area = "admin" }
                    });
                }
                else
                {
                    items.Add(new AdminMenuItem
                    {
                        Text = "Profile",
                        Icon = "fa fa-check-square-o nav_icon",
                        Action = "edit",
                        RouteInfo = new { controller = "user", area = "admin", username = User.Identity.Name }
                    });
                }

                if (!User.IsInRole("author"))
                {
                    items.Add(new AdminMenuItem
                    {
                        Text = "Tags",
                        Icon = "fa fa-list-ul nav-icon",
                        Action = "index",
                        RouteInfo = new { controller = "tag", area = "admin" }
                    });
                }

                items.Add(new AdminMenuItem
                {
                    Text = "Posts",
                    Icon = "fa fa-file-text-o nav-icon",
                    Action = "index",
                    RouteInfo = new { controller = "post", area = "admin" }
                });
            }

            return PartialView(items);
        }

        [AllowAnonymous]
        public async Task<PartialViewResult> AuthenticationLink()
        {
            var item = new AdminMenuItem
            {
                RouteInfo = new { controller = "admin", area = "admin" }
            };

            if (User.Identity.IsAuthenticated)
            {
                item.Text = "Logout";
                item.Icon = "icon-off nav-icon";
                item.Action = "logout";
            }
            else
            {
                item.Text = "Login";
                item.Action = "login";
            }

            return PartialView("_menuLink", item);
        }

        private bool _isDisposed;
        protected override void Dispose(bool disposing)
        {

            if (!_isDisposed)
            {
                _users.Dispose();
            }
            _isDisposed = true;

            base.Dispose(disposing);
        }
    }
}