using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppRoleDtos;


using Microsoft.AspNetCore.Mvc;



namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Role")]
    public class RoleController : Controller
    {
        private readonly IAppRoleService _appRoleService;


        public RoleController(IAppRoleService appRoleService)
        {
            _appRoleService = appRoleService;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _appRoleService.GetListAsync();
            return View(values);
        }

        [HttpGet]
        [Route("CreateRole")]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateAppRoleDto createRoleViewModel)
        {
            var role = new CreateAppRoleDto
            {
                Name = createRoleViewModel.Name
            };

            var result = await _appRoleService.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else 
            {
                return View();
            }
                
        }
        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _appRoleService.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        [HttpGet]
        [Route("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var value = await _appRoleService.GetByIdAsync(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }

            UpdateAppRoleDto updateRoleViewModel = new UpdateAppRoleDto
            {
                Id = value.Id,
                Name = value.Name
            };
            return View(updateRoleViewModel);
        }
        [HttpPost]
        [Route("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(UpdateAppRoleDto updateRoleViewModel)
        {
            var dto = new UpdateAppRoleDto
            {
                Id = updateRoleViewModel.Id,
                Name = updateRoleViewModel.Name
            };
            await _appRoleService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
        [Route("UserList")]
        public async Task< IActionResult> UserList()
        {
            var values = await _appRoleService.GetUsersAsync();
            return View(values);
        }
        [Route("AssignRole/{id}")]
        [HttpGet]
        public async Task <IActionResult>AssignRole(int id)
        {
           TempData["UserId"] = id;
            var roleAssignments = await _appRoleService.GetRoleAssignmentsAsync(id);
            return View(roleAssignments);
        }
        [HttpPost]
        [Route("AssignRole/{id}")]
        public async Task<IActionResult> AssignRole(List<RoleAssignDto> model)
        {
            var userid = (int)TempData["userId"];
            await _appRoleService.UpdateRoleAssignmentsAsync(userid, model);
            return RedirectToAction("UserList");
        }

    }
}
