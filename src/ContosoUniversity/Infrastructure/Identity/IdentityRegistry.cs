namespace ContosoUniversity.Infrastructure.Identity
{
    using DAL;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Models;
    using StructureMap;
    using StructureMap.Pipeline;
    using System;
    using System.Web;

    public class IdentityRegistry : Registry
    {
        public IdentityRegistry()
        {
            For<UserManager<User, int>>().Use("Configura UserManager", ctx =>
            {
                var db = ctx.GetInstance<SchoolContext>();
                var userStore = new UserStore(db);
                var userManager = new UserManager<User, int>(userStore);

                userManager.UserValidator = new UserValidator<User, int>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = false
                };

                userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false
                };

                userManager.UserLockoutEnabledByDefault = true;
                userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
                userManager.MaxFailedAccessAttemptsBeforeLockout = 3;

                return userManager;
            })
            .LifecycleIs<TransientLifecycle>(); 

            For<SignInManager<User, int>>().Use("Configure SignInManager", ctx =>
            {
                var userManager = ctx.GetInstance<UserManager<User, int>>();
                var owinContext = HttpContext.Current.GetOwinContext();
                return new SignInManager<User, int>(userManager, owinContext.Authentication);
            })
            .LifecycleIs<TransientLifecycle>();

            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication).LifecycleIs<TransientLifecycle>();
        }
    }
}