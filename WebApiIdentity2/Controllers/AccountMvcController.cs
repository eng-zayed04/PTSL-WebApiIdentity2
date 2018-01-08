using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebApiIdentity2.Models;
using WebApiIdentity2.Providers;
using WebApiIdentity2.Results;

namespace WebApiIdentity2.Controllers
{
    public class AccountMvcController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        // GET: /Account/Register
        [System.Web.Http.AllowAnonymous]
        public ActionResult RegisterUser()
        {
            return View();
        }

        // POST api/Account/Register
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser(RegisterViewModel model)
        {
            //if (!System.Web.Http.ModelBinding.ModelState.IsValid)
            //{
            //    return BadRequest(System.Web.Http.ModelBinding.ModelState);
            //}

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                //return View();
            }

            //if (!result.Succeeded)
            //{
            //    return RedirectToRoute("Default", new { controller = "Home", action = "Index" });
            //}

            //return Ok();
            return View(model);

        }
    }
}
