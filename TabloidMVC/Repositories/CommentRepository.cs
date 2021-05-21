using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public void Add(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public Post GetCommentById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
