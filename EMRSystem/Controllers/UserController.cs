using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EMRSystem.Models;
using EMRSystem.Services;
using EMRSystem.ViewModels;
using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EMRSystem.Controllers
{
    public class UserController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private AMRolesManager _rolesManager;
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }

        public UserController()
        {
        }
        public UserController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: User
        public ActionResult Index(string searchterm)
        {
            UsersListingViewModel model = new UsersListingViewModel();
            model.Users = SearchUsers(searchterm);
            model.Roles = RolesManager.Roles.ToList();
            return View(model);
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }


            return users;
        }

        public ActionResult Register(RegisterViewModel model)
        {
            model.Roles = RolesManager.Roles.ToList();
            return PartialView("_Register", model);
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID,string Type)
        {
            UserActionModel model = new UserActionModel();
            model.Roles = RolesManager.Roles.ToList();

            if (!string.IsNullOrEmpty(ID))
            {
                var user = await UserManager.FindByIdAsync(ID);
                model.ID = user.Id;
                model.Name = user.Name;
                model.Contact = user.PhoneNumber;
                model.Email = user.Email;
                model.City = user.City;
                model.CNIC = user.CNIC;
                model.Age = user.Age;   
                model.DOB = user.DOB;
                model.Gender = user.Gender;
                model.Image = user.Image;
                model.RegisteredNo = user.RegisteredNo;
                model.Image = user.Image;
                model.Role = user.Role;
                model.Password = user.Password;
                Session["Type"] = Type;
            }
            return PartialView("_Action", model);
        }



        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID)) //update record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.Id = model.ID;
                user.Name = model.Name;
                user.PhoneNumber = model.Contact;
                user.Email = model.Email;
                user.Role = model.Role;
                user.City = model.City;
                user.CNIC = model.CNIC;
                user.Age = model.Age;
                user.DOB = model.DOB;
                user.Gender = model.Gender;
                user.Image = model.Image;
                user.RegisteredNo = model.RegisteredNo;
                user.Image = model.Image;
                var token = await UserManager.GeneratePasswordResetTokenAsync(model.ID);
                var result2 = await UserManager.ResetPasswordAsync(model.ID, token, model.Password);
                result = await UserManager.UpdateAsync(user);

            }
            else
            {
                var User = new User();
                User.Name = model.Name;
                User.PhoneNumber = model.Contact;
                User.Email = model.Email;
                User.Password = model.Password;
                User.Role = model.Role;
                User.City = model.City;
                User.CNIC = model.CNIC;
                User.Age = model.Age;
                User.DOB = model.DOB;
                User.Gender = model.Gender;
                User.Image = model.Image;
                User.RegisteredNo = model.RegisteredNo;
                User.Image = model.Image;
                User.UserName = model.Email;
                result = await UserManager.CreateAsync(User);

                await UserManager.AddToRoleAsync(User.Id, model.Role);

            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UserActionModel model = new UserActionModel();

            var user = await UserManager.FindByIdAsync(ID);

            model.ID = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) //we are trying to delete a record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid user." };
            }

            return json;
        }



        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
            UserRoleModel model = new UserRoleModel();
            model.UserID = ID;
            var user = await UserManager.FindByIdAsync(ID);
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();


            model.UserRoles = RolesManager.Roles.Where(x => userRoleIDs.Contains(x.Id)).ToList();
            model.Roles = RolesManager.Roles.Where(x => !userRoleIDs.Contains(x.Id)).ToList();
            return PartialView("_UserRoles", model);
        }



        [HttpPost]
        public async Task<JsonResult> UserRoles(UserActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID)) //update record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.Id = model.ID;
                user.Name = model.Name;
                user.PhoneNumber = model.Contact;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Role = model.Role;
                result = await UserManager.UpdateAsync(user);

            }
            else
            {
                var User = new User();
                User.Name = model.Name;
                User.PhoneNumber = model.Contact;
                User.Email = model.Email;
                User.Password = model.Password;
                User.UserName = model.Email;
                User.Role = model.Role;
                result = await UserManager.CreateAsync(User);

            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return json;
        }




        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userID, string roleID, bool isDelete = false)
        {
            JsonResult json = new JsonResult();

            var user = await UserManager.FindByIdAsync(userID);
            var role = await RolesManager.FindByIdAsync(roleID);

            if (user != null && role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(userID, role.Name);
                }
                else
                {
                    result = await UserManager.RemoveFromRoleAsync(userID, role.Name);
                }
                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Operation" };

            }


            return json;
        }
    }
}