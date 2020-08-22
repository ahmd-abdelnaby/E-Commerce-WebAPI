using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApiProject.Model;

[assembly: OwinStartup(typeof(WebApiProject.Startup1))]

namespace WebApiProject
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888


            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
                Provider=new OAuthTokenCreate()
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            EnableCorsAttribute enableCors = new EnableCorsAttribute("*", "*", "*");
            
            
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors(enableCors);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }

    internal class OAuthTokenCreate : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            ApplicationDbContext dbContext = new ApplicationDbContext();
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(dbContext);
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

            IdentityUser user = manager.Find(context.UserName, context.Password);
            if(user==null)
            {
                context.SetError("Grant_error", "UserName and Pass Not Found!!");
                return; 
            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.UserName));
            claimsIdentity.AddClaim(new Claim("age", "22"));
            
            context.Validated(claimsIdentity);
        }
    }
}
