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

namespace MyPersonalPage.Controllers
{  
    [Authorize(Roles="Admin")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Posts.OrderByDescending(p => p.Created).Take(3).ToList());
        }


        // GET: Posts
        public async Task<ActionResult> AdminIndex()
        {
            return View(await db.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(string Slug)
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
            return View(post);
        }

        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = await db.Posts.FindAsync(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

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
        public async Task<ActionResult> Create([Bind(Include = "Created,Title,Body")] Post post, HttpPostedFileBase image)
        {
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
                    post.Created = System.DateTimeOffset.Now;
                    post.Slug = Slug;

                    db.Posts.Add(post);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }


        //public async Task<ActionResult> Create([Bind(Include = "Id,Created,Updated,Title,Body,MediaUrl,Slug")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Posts.Add(post);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(post);
        //}

        // GET: Posts/Edit/5
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

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaUrl,Slug")] Post post)
        public async Task<ActionResult> Edit([Bind(Include = "Id,Updated,Title,Body")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(post);                
                post.Updated = System.DateTimeOffset.Now;
                    
                db.Entry(post).Property(p => p.Body).IsModified = true;
                db.Entry(post).Property(p => p.Title).IsModified = true;
                db.Entry(post).Property(p => p.Updated).IsModified = true;
                    
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //public async Task<ActionResult> Edit([Bind(Include = "Id,Updated,Title,Body,Published")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(post).State = EntityState.Modified;

        //        post.Updated = System.DateTimeOffset.Now;

        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(post);
        //}


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
    }
}
