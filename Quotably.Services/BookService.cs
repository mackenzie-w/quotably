using Quotably.Data;
using Quotably.Models;
using Quotably.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Services
{
    public class BookService
    {
        private readonly Guid _userId;

        public BookService(Guid userId)
        {
            userId = _userId;
        }

        public bool CreateBook(BookCreate model)
        {
            var entity = new Book()
            {
                AuthorID = model.AuthorID,
                Title = model.Title,
                Description = model.Description,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookListItem> GetBooks()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Books
                    .Select(
                        e =>
                            new BookListItem
                            {
                                BookID = e.BookID,
                                Title = e.Title,
                                AuthorID = e.AuthorID,
                                CreatedUtc = e.CreatedUtc
                            }
                    );
                return query.ToArray();
            }
        }

        public BookDetail GetBookById(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Books
                    .Single(e => e.BookID == bookId && e.OwnerID == _userId);
                return new BookDetail
                {
                    BookID = entity.BookID,
                    Title = entity.Title,
                    AuthorID = entity.AuthorID,
                    Description = entity.Description,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }

        public bool UpdateBook(BookEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Books
                    .Single(e => e.BookID == model.BookID && e.OwnerID == _userId);
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.AuthorID = model.AuthorID;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int bookId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Books
                    .Single(e => e.BookID == bookId && e.OwnerID == _userId);
                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
