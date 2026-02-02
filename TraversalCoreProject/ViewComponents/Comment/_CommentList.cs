using BusinessLayer.Abstract;

using DataAccessLayer.Concrete;

using Microsoft.AspNetCore.Mvc;


namespace TraversalCoreProje.ViewComponents.Comment
{
    public class _CommentList : ViewComponent
    {
        private readonly ICommentService _commentService;
       

        public _CommentList(ICommentService commentService, Context context)
        {
            _commentService = commentService;
        
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.commentCount = await _commentService.GetCountByDestinationIdAsync(id);
            var values = await _commentService.GetCommentsWithDestinationAndUserAsync(id);
            return View(values);
        }
    }
}
