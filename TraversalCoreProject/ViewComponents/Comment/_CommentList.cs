using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.ViewComponents.Comment
{
    public class _CommentList : ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly Context _context;

        public _CommentList(ICommentService commentService, Context context)
        {
            _commentService = commentService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.commentCount = _context.Comments.Where(x => x.Id == id).Count();
            var values =await _commentService.GetCommentsWithDestinationAndUserAsync(id);
            return View(values);
        }
    }
}
