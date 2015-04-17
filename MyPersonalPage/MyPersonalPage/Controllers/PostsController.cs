using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPersonalPage.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace MyPersonalPage.Controllers
{  
    [Authorize(Roles="Admin")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[AllowAnonymous]
        //public ActionResult Index(int? page)
        //{
        //    int pageSize = 3;    // display three blog posts at a time on this page
        //    int pageNumber = (page ?? 1);
        //    return View(db.Posts.OrderByDescending(p => p.Created).ToPagedList(page ?? 1, pageSize));
        //}

        [AllowAnonymous]
        public ActionResult Index(int? page, string searchString)
        {
            var lastSearch = TempData["lastSearch"] as string;
            if (lastSearch != searchString)
            {
                page = 1;            
                TempData["lastSearch"] = searchString;
            }

            int pageSize = 3;    // display three blog posts at a time on this page
            int pageNumber = (page ?? 1);

            var searched = from p in db.Posts
                           where searchString == "" || searchString == null || 
                           p.Body.Contains(searchString) ||
                           p.Title.Contains(searchString) ||
                           p.Comments.Any(c => c.Body.Contains(searchString) || 
                               c.Author.DisplayName.Contains(searchString))
                           orderby p.Created descending
                           select p;

            ViewBag.found = searched.Count();

            //var searched = db.Posts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize);
            
            ViewBag.searchString = searchString;

            return View(searched.ToPagedList(pageNumber, pageSize));
        }

        // GET: Posts
        public async Task<ActionResult> AdminIndex()
        {
            return View(await db.Posts.ToListAsync());
        }

        // GET: Posts/Details
        [AllowAnonymous]
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            // sort comments in descending order
            post.Comments = post.Comments.OrderByDescending(p => p.Created).ToList();

            return View(post);
        }

        //// GET: Posts/CreateComment
        //public ActionResult CreateComment()
        //{
        //    return View();
        //}

        // POST: Posts/CreateComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateComment([Bind(Include = "PostId,AuthorId,Created,Body")] Comment comment, string slug)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrWhiteSpace(comment.Body))
                {
                    ModelState.AddModelError("Body", "Missing Comment Text");
                    return RedirectToAction("Details", new { Slug = slug });
                }
                    comment.Created = System.DateTimeOffset.Now;
                    comment.AuthorId = User.Identity.GetUserId();

                    db.Comments.Add(comment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details", new { Slug = slug });
              
            }
            return RedirectToAction("Details", new { Slug = slug});
        }



        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Created,Title,Body,MediaUrl")] Post post, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                // check the file name to make sure it's an image type
                var ext = Path.GetExtension(image.FileName);
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
                    ModelState.AddModelError("image", "Invalid format.");
            }


            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(post.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(post);
                }
                if (db.Posts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(post);
                }
                else
                {
                 ////relative server path
                 //var filePath = "/Uploads/blog/images/";
                 ////path on physical drive on the server
                 //var absPath = Server.MapPath("~" + filePath);
                 ////media Url for relative path
                 //post.MediaUrl = filePath + image.FileName;
                 ////save image
                 //image.SaveAs(Path.Combine(absPath, image.FileName));


                    post.Created = System.DateTimeOffset.Now;
                    post.Slug = Slug;

                    db.Posts.Add(post);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }

        // GET: Posts/EditComment
        public async Task<ActionResult> EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Posts/EditComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaUrl,Slug")] Post post)
        public async Task<ActionResult> EditComment([Bind(Include = "Created,Id,Updated,AuthorId,Body,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Attach(comment);
                comment.Updated = System.DateTimeOffset.Now;

                db.Entry(comment).State = EntityState.Modified;

                Post post = db.Posts.Find(comment.PostId);

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { slug = post.Slug });
                //return View();
            }
            return View(comment);
        }


        // GET: Posts/Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaUrl,Slug")] Post post)
        public async Task<ActionResult> Edit([Bind(Include = "Id,Updated,Title,Body,MediaUrl")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(post);                
                post.Updated = System.DateTimeOffset.Now;
                    
                db.Entry(post).Property(p => p.Body).IsModified = true;
                //db.Entry(post).Property(p => p.Title).IsModified = true;
                db.Entry(post).Property(p => p.Updated).IsModified = true;
                db.Entry(post).Property(p => p.MediaUrl).IsModified = true;
                    
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Posts/DeleteComment
        public async Task<ActionResult> DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Posts/DeleteComment
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCommentConfirmed(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            Post post = db.Posts.Find(comment.PostId);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { slug = post.Slug });
        }

   



    }
}
