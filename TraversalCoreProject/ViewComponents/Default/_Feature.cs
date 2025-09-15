using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.ViewComponents.Default
{
    public class _Feature:ViewComponent
    {
        private readonly IFeatureService _featureService;

        public _Feature(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public IViewComponentResult Invoke()
        {
           // var values = featureManager.TGetList();
           // ViewBag.image1 = featureManager.get
            return View();
        }
    }
}
