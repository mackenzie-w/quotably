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
    public class BookQuoteService
    {
        private readonly Guid _userID;

        public BookQuoteService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateBookQuote(BookQuoteCreate model)
        {
            var entity = new BookQuote()
            {
                OwnerID = _userID,
                Content = model.Content,
                BookID = model.BookID,
                AuthorID = model.AuthorID,
                CreatedUtc = DateTimeOffset.Now
            };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.BookQuotes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookQuoteListItem> GetBookQuotes()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .BookQuotes
                    .Where(e => e.OwnerID == _userID)
                    .Select(
                        e => new BookQuoteListItem
                        {
                            BookQuoteID = e.BookQuoteID,
                            Content = e.Content,
                            BookID = e.BookID,
                            AuthorID = e.AuthorID,
                            CreatedUtc = e.CreatedUtc
                            
                        }
                    );
                return query.ToArray();
            }
        }

        public BookQuoteDetail GetBookQuoteById(int bookquoteId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .BookQuotes
                    .Single(e => e.BookQuoteID == bookquoteId && e.OwnerID == _userID);
                return new BookQuoteDetail
                {
                    BookQuoteID = entity.BookQuoteID,
                    Content = entity.Content,
                    BookID = entity.BookID,
                    AuthorID = entity.AuthorID,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }

        public bool UpdateBookQuote(BookQuoteEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .BookQuotes
                    .Single(e => e.BookQuoteID == model.BookQuoteID && e.OwnerID == _userID);
                entity.Content = model.Content;
                entity.AuthorID = model.AuthorID;
                entity.BookID = model.BookID;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBookQuote(int bookquoteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .BookQuotes
                    .Single(e => e.BookQuoteID == bookquoteId && e.OwnerID == _userID);
                ctx.BookQuotes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
