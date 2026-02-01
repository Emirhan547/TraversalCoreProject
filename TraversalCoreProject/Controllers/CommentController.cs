using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.CommentsDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
 
        public CommentController(ICommentService commentService)
        {
           
            _commentService = commentService;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto comment)
        {
            comment.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.CommentState = true;
            await _commentService.AddAsync(comment);
            return RedirectToAction("Index", "Destination");
        }
    }
}
