using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiProject.Model;

namespace WebApiProject.Controller
{
    public class AccountController : ApiController
    {
        public async Task<IHttpActionResult> postUser(userModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationDbContext dbContext = new ApplicationDbContext();
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(dbContext);
            UserManager<IdentityUser> Manager = new UserManager<IdentityUser>(userStore);

            IdentityUser user = new IdentityUser();
            user.UserName = model.Name;
            user.PasswordHash = model.Password;


            IdentityResult result = await Manager.CreateAsync(user,model.Password);

            if (result.Succeeded)
                return Created("", "UserAdded");
            return BadRequest(result.Errors.ToList()[0]);
        }
    }
}
