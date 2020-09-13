using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitalLibrary.Models;

namespace DigitalLibrary.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Administrator , Editor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Url,numOfCopies")]
            Book book)
        {
            if (ModelState.IsValid)
            {
                book.numOfCopiesLeft = book.numOfCopies;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Url,numOfCopies")]
            Book book)
        {
            if (ModelState.IsValid)
            {
                book.numOfCopiesLeft = book.numOfCopies;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [Authorize(Roles = "Editor , Administrator")]
        public ActionResult InsertNewClient(int id)
        {
            ClientBook model = new ClientBook();
            model.BookId = id;
            model.Book = db.Books.Find(id);
            if (model.Book.numOfCopiesLeft == 0)
            {
                return RedirectToAction("Index");
            }
            model.Clients = db.Clients.ToList();
            ViewBag.Clients = db.Clients.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertNewClient(ClientBook model)
        {
            var client = db.Clients.FirstOrDefault(m => m.Id == model.ClientId);
            var book = db.Books.FirstOrDefault(m => m.Id == model.BookId);
            book.Clients.Add(client);
            client.Books.Add(book);
            client.NumBooks = client.Books.Count;
            book.numOfCopiesLeft--;
            db.SaveChanges();
            return View("Index", db.Books.ToList());
        }

        [Authorize(Roles = "Editor , Administrator")]
        public ActionResult RemoveFromBook(int bookId, int clientId)
        {
            ClientBook clientBook = new ClientBook();
            clientBook.BookId = bookId;
            clientBook.ClientId = clientId;
            clientBook.Book = db.Books.Find(bookId);
            clientBook.Client = db.Clients.Find(clientId);
            return View(clientBook);
        }


        [HttpPost]
        public ActionResult RemoveFromBook(ClientBook model)
        {
            var client = db.Clients.FirstOrDefault(m => m.Id == model.ClientId);
            var book = db.Books.FirstOrDefault(m => m.Id == model.BookId);
            book.Clients.Remove(client);
            client.Books.Remove(book);
            client.NumBooks = client.Books.Count;
            book.numOfCopiesLeft++;
            db.SaveChanges();
            return View("Index", db.Books.ToList());
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
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
