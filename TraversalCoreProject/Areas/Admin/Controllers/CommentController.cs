using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _commentService.GetCommentsWithDestinationAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
