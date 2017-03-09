using MvcCms.Data;
using MvcCms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCms.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IPostRepository _posts;
        private readonly int _pageSize = 2;

        public HomeController() : this(new PostRepository()) { }

        public HomeController(IPostRepository postRepository)
        {
            _posts = postRepository;
        }

        // GET: Default
        // root/
        [Route("")]
        public async Task<ActionResult> Index()
        {
            //var posts = await _posts.GetPageAsync(1, _pageSize);

            //ViewBag.PreviousPage = 0;
            //ViewBag.NextPage = (Decimal.Divide(_posts.CountPublished, _pageSize) > 1) ? 2 : -1;

            //return View(posts);
            var posts = await _posts.GetAllAsync();
            return View(posts);
        }

        [Route("page/{page:int}")]
        public async Task<ActionResult> Page(int page = 1)
        {
            if (page < 2)
            {
                return RedirectToAction("index");
            }

            var posts = await _posts.GetPageAsync(page, _pageSize);

            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = (Decimal.Divide(_posts.CountPublished, _pageSize) > page) ? page + 1 : -1;

            return View("index", posts);
        }

        // root/posts/post-id
        [Route("posts/{postId}")]
        public async Task<ActionResult> Post(string postId)
        {
            var post = _posts.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // root/tags/tag-id
        [Route("tags/{tagId}")]
        public async Task<ActionResult> Tag(string tagId)
        {
            var posts = await _posts.GetPostsByTagAsync(tagId);

            if (!posts.Any())
            {
                return HttpNotFound();
            }

            ViewBag.Tag = tagId;

            return View(posts);
        }
        public void uploadnow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string ImageName = upload.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/uploads"), ImageName);
                upload.SaveAs(path);
            }
        }
        public ActionResult uploadPartial()
        {
            var appData = Server.MapPath("~/Images/uploads");
            var images = Directory.GetFiles(appData).Select(x => new imagesviewmodel
            {
                Url = Url.Content("/Images/uploads/" + Path.GetFileName(x))
            });
            return View(images);
        }
    }
}