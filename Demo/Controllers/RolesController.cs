using Demo.BL.Models;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;


        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Roles = roleManager.Roles;
            return View(Roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {

            var Role = new IdentityRole()
            {
                Name = model.Name,
                NormalizedName = model.Name.ToUpper()
            };

            var result =await roleManager.CreateAsync(Role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var Role = await roleManager.FindByIdAsync(id);
            return View(Role);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {

            var Role = await roleManager.FindByIdAsync(model.Id);

            Role.Name = model.Name;
            Role.NormalizedName = model.Name.ToUpper();

            var result = await roleManager.UpdateAsync(Role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }


            return View(model);
        }

         
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {

            ViewBag.RoleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleVM>();

            var users = await userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleVM()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {

            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (i < model.Count)
                    continue;
            }

            return RedirectToAction("Edit", new { id = RoleId });
        }

    }
}
