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
    public class AuthorService
    {
        private readonly Guid _userId;

        public AuthorService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAuthor(AuthorCreate model)
        {
            var entity = new Author()
            {
                OwnerId = _userId,
                AuthorFirstName = model.AuthorFirstName,
                AuthorLastName = model.AuthorLastName,
            };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.Authors.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AuthorListItem> GetAuthors()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Authors
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new AuthorListItem
                            {
                                AuthorID = e.AuthorID,
                                AuthorFirstName = e.AuthorFirstName,
                                AuthorLastName = e.AuthorLastName,
                                CreatedUtc = e.CreatedUtc,
                            }
                    );
                return query.ToArray();
            }
        }

        public AuthorDetail GetAuthorById(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Authors
                    .Single(e => e.AuthorID == authorId && e.OwnerId == _userId);
                return new AuthorDetail
                {
                    AuthorID = entity.AuthorID,
                    AuthorFirstName = entity.AuthorFirstName,
                    AuthorLastName = entity.AuthorLastName,
                    CreatedUtc = entity.CreatedUtc
                };
            }
        }

        public bool UpdateAuthor(AuthorEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Authors
                    .Single(e => e.AuthorID == model.AuthorID && e.OwnerId == _userId);
                entity.AuthorFirstName = model.AuthorFirstName;
                entity.AuthorLastName = model.AuthorLastName;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAuthor(int authorId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Authors
                    .Single(e => e.AuthorID == authorId && e.OwnerId == _userId);

                ctx.Authors.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
