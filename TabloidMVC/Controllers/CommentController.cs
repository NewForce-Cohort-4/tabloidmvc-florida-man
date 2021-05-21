using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        // View comments for a specific post
        public IActionResult GetCommentsByPostId(int postId)
        {
            return View();
        }

    }
}
