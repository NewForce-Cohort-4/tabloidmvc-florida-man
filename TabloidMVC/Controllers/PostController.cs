using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private object _postRepo;

        
        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            ViewBag.currentUserId = GetCurrentUserProfileId();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            ViewBag.currentUserId = GetCurrentUserProfileId();
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        public IActionResult AllPostsByUser()
        {
            int currentUserId = GetCurrentUserProfileId();
            var posts = _postRepository.GetAllPostsByUser(currentUserId);
            return View(posts);
        }


        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        //*****************************************DELETE****************//
        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            Post post = _postRepository.GetPostById(id);
            return post.UserProfileId == GetCurrentUserProfileId() ? View(post) : NotFound();

        }

        // POST: Owners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Post post)
        {
            try
            {
                _postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }


        public ActionResult Edit(int id)
        {
            List<Category> categories = _categoryRepository.GetAll();
            Post post = _postRepository.GetPostById(id);

            PostEditViewModel vm = new PostEditViewModel()
            {
                CategoryOptions = categories,
                Post = post
            };  

            if (post == null)
            {
                return NotFound();
            }

            else if (post.UserProfileId == GetCurrentUserProfileId())
            {
                return post.UserProfileId == GetCurrentUserProfileId() ? View(vm) : NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditViewModel posteditviewmodel)
        {

            //try
            {
                _postRepository.UpdatePost(posteditviewmodel.Post);

                return RedirectToAction("Index");
            }
            //catch (Exception ex)
            {
                //return View(post);
            }

        }


    }
}
