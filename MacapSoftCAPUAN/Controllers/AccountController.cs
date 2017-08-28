using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MacapSoftCAPUAN.Models;
using System.Collections.Generic;
using MacapSoftCAPUAN.BO;

namespace MacapSoftCAPUAN.Controllers
{
    
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;

        public AccountController()
        {
            context = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ApplicationDbContext UsersContext { get; set; }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmailAsync(model.Email).Result;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            if (user != null) {
                if (user.Activo == false) {
                    ModelState.AddModelError("", "El usuario está inactivo, comuníquese con el coordinador del CAP.");
                    return View(model);
                }
            }
                
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "HistoriaClinica");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    if (user != null)
                    {
                        if (user.Activo == false)
                        {
                            ModelState.AddModelError("", "El usuario está inactivo, comuníquese con el coordinador del CAP.");
                            return View(model);
                        }
                        else
                        {
                            ModelState.AddModelError("", "El usuario y/o contraseña que has introducido son incorrectas, si el error persiste comuníquese con el coordinador del CAP.");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "El usuario y/o contraseña que has introducido son incorrectas, si el error persiste comuníquese con el coordinador del CAP.");
                    }
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        //[Authorize(Roles = "Administrador")]
        public ActionResult Register()
        {
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Activo = true };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    return RedirectToAction("Index", "HistoriaClinica");
                }
                if (result.Errors.Contains("Name "+ user.UserName +" is already taken."))
                {
                    ModelState.AddModelError("", "El usuario ya existe");
                }
                else
                {
                    ModelState.AddModelError("", "Las contraseñas deben tener al menos una letra o un carácter de dígito.Las contraseñas deben tener al menos una minúscula('a' - 'z').Las contraseñas deben tener al menos una mayúscula('A' - 'Z').");
                }

            }

            // If we got this far, something failed, redisplay form
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            IdentityResult result;
            try
            {
                result = await UserManager.ConfirmEmailAsync(userId, code);
            }
            catch (InvalidOperationException ioe)
            {
                // ConfirmEmailAsync throws when the userId is not found.
                ViewBag.errorMessage = ioe.Message;
                return View("Error");
            }

            if (result.Succeeded)
            {
                return View();
            }

            // If we got this far, something failed.
            AddErrors(result);
            ViewBag.errorMessage = "ConfirmEmail failed";
            return View("Error");

            //if (userId == null || code == null)
            //{
            //    return View("Error");
            //}
            //var result = await UserManager.ConfirmEmailAsync(userId, code);
            //if (result.Succeeded)
            //{
            //    return View("ConfirmEmail");
            //}
            //AddErrors(result);
            //return View();

            //var result = await UserManager.ConfirmEmailAsync(userId, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                //if (ModelState.IsValid)
                //{
                //    var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                //    var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                //    var callbackUrl = Url.Action("ResetPassword", "Account",new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //    await UserManager.SendEmailAsync(user.Id, "Reset Password","Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                //    return View("ForgotPasswordConfirmation");

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");

                //string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", $"Please reset your password by using this {code}");
                //return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //return RedirectToAction("Index", "HistoriaClinica");
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login","Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ListaUsuarios()
        {
            List<SelectListItem> listaItemsActivo = new List<SelectListItem>();
            SelectListItem itemActivo = new SelectListItem();
            itemActivo.Text = "Activo";
            itemActivo.Value = "1";
            listaItemsActivo.Add(itemActivo);

            SelectListItem itemInactivo = new SelectListItem();
            itemInactivo.Text = "Inactivo";
            itemInactivo.Value = "0";
            listaItemsActivo.Add(itemInactivo);

            ViewBag.ItemsHabilitarUsuario = listaItemsActivo.ToList(); 

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public JsonResult ListarUsuarios()
        {
            //var UsersContext = new ApplicationDbContext();
            //var listaUsuarios = getAllUsers();
            var applicationDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var users = from user in applicationDbContext.Users
                        from role in applicationDbContext.Roles
                        where role.Users.Any(r => r.UserId == user.Id)
                        select new
                        {
                            Id = user.Id,
                            nombreUsuario = user.UserName,
                            Email = user.Email,
                            Role = role.Name,
                            Estado = user.Activo? "Activo":"Inactivo"
                        };

            var listaUsuarios = users.ToList();
            return Json(listaUsuarios, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public JsonResult EditRoleUser(string idColumna, string rolEditado , string estadoUsuario)
        {
            var UsersContext = new ApplicationDbContext();
            var oldUser = UserManager.FindById(idColumna);
            var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
            var oldRoleName = UsersContext.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;
            AccountBO accBO = new AccountBO();
            if(rolEditado != null) {
                if (oldRoleName != rolEditado)
                {
                    UserManager.RemoveFromRole(idColumna, oldRoleName);
                    UserManager.AddToRole(idColumna, rolEditado);
                }

                bool estadoUs = false;
                if (estadoUsuario == "1")
                {
                    estadoUs = true;
                    accBO.editarUsuario(oldUser, estadoUs);
                }
                else
                {
                    estadoUs = false;
                    accBO.editarUsuario(oldUser, estadoUs);
                }
            }
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult cambiarContraseña() {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult> CambiarContraseña(LoginViewModel model, string nuevaContra)
        {
            AccountBO accBO = new AccountBO();
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var email = System.Web.HttpContext.Current.User.Identity.Name;
            model.Email = email;
            model.Active = true;
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            var user = UserManager.FindByEmailAsync(model.Email).Result;
            if (model.Password != nuevaContra) {
                if (result == Microsoft.AspNet.Identity.Owin.SignInStatus.Success)
                {
                    user.PasswordHash = null;
                    //var user1 = new ApplicationUser { UserName = model.Email, Email = model.Email, Activo = true };
                    var result1 = await UserManager.AddPasswordAsync(userId, nuevaContra);
                    //accBO.listarUsuarios(user, nuevaContra);

                    if (result1.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        //await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                        return RedirectToAction("Index", "HistoriaClinica");
                    }
                }
            }

            TempData["mensajeError"] = "Error de validación, esto se presenta por los siguientes aspectos: la contraseña anterior no coincide, no ingreso una contraseña válida o digitó la misma contraseña.";
            ViewBag.ErrorMessage = "Email not found or matched";
            return RedirectToAction("CambiarContraseña", "Account");
            //return Json("Ok",JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult RecuperarContraseña()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult> RecuperarContraseña(RecuperarContraseñaViewModel model, string nuevaContra)
        {
            var user = UserManager.FindByEmailAsync(model.Email).Result;
            IdentityResult result1;
            if (user != null)
            {
                user.PasswordHash = null;
                result1 = await UserManager.AddPasswordAsync(user.Id, nuevaContra);
                if (result1.Succeeded)
                {
                    return View("ExitoRecuperarContrasena");
                    //return RedirectToAction("Index", "HistoriaClinica");
                }
                if (result1.Errors.Contains("Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9'). Passwords must have at least one uppercase ('A'-'Z')."))
                {
                    ModelState.AddModelError("", "Las contraseñas deben tener al menos una letra o un carácter de dígito. Al menos una minúscula('a' - 'z'), una mayúscula('A' - 'Z').");
                    return View(model);
                }
                else {
                    if (result1.Errors.Contains("Passwords must have at least one non letter or digit character."))
                    {
                        ModelState.AddModelError("", "La contraseña debe tener al menos un caracter (_-*#).");
                        return View(model);
                    }
                    if (result1.Errors.Contains("Passwords must have at least one digit ('0'-'9')."))
                    {
                        ModelState.AddModelError("", "La contraseña debe tener al menos un digito entre 0-9");
                        return View(model);
                    }else
                    {
                        ModelState.AddModelError("", "Las contraseñas deben tener al menos una minúscula('a' - 'z'), al menos una mayúscula('A' - 'Z') y al menos un caracter (_-*#).");
                        return View(model);
                    }
                }
            }
            if (user == null) {
                ModelState.AddModelError("", "No existe el correo ingresado.");
                return View(model);
            }
            
            ModelState.AddModelError("", "Las contraseñas deben tener al menos una letra o un carácter de dígito.Las contraseñas deben tener al menos una minúscula('a' - 'z').Las contraseñas deben tener al menos una mayúscula('A' - 'Z').");
            return View(model);
            //return RedirectToAction("RecuperarContraseña", "Account");
        }

        #endregion
    }
}