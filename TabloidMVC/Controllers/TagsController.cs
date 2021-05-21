using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class TagsController : Controller
    {

        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository postRepository, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: TagsController
        public ActionResult Index()
        {
            var tags = _tagRepository.GetAll();
            return View(tags);
        }

        // GET: TagsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TagsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagsController/Create
        [HttpPost]
        
        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagsController/Edit/5
        public ActionResult Edit(int id)
        {
            // GetTagById method is invoked and the id of tag in the url path is passed as a param
            Tag tag = _tagRepository.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: TagsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            // Once the id of the tag is passed with the GET request the POST form returns with the same Id
            // EditTag method is called and passed the method as a parameter
            // successul updates to DB will then return to the list or "Index"
            try
            {
                _tagRepository.EditTag(tag);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }

        // GET: TagsController/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);

            return View(tag);
        }

        // POST: TagsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepository.DeleteTag(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }
    }
}
