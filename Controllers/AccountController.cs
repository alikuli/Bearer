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
<<<<<<< HEAD

using System.Data.Entity;
using ModelsClassLibrary.Models;
using Bearer.Models;
//using AppDbx.Models;
=======
//using Bearer.;
using System.Data.Entity;
using ModelsClassLibrary.Models;
using AppDbx.Models;
>>>>>>> Persons-fork


namespace Bearer.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private UserManager _userManager;
        //private ApplicationDbContext _db;

        public AccountController()
        {
            //db = new ApplicationDbContext();
        }

        public AccountController(UserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        //public AccountController(UserManager userManager, ApplicationSignInManager signInManager, ApplicationDbContext dbContext)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //    db = dbContext;
        //}

        

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

        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //public ApplicationDbContext db
        //{
        //    get
        //    {
        //        return _db ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
        //    }
        //    private set
        //    {
        //        _db = value;
        //    }
        //}
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl,  bool showInvalidLoginError = false)
        {
            ViewBag.ReturnUrl = returnUrl;

            if(showInvalidLoginError)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            //In this program, the phone number of the user will behave like his name!

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            //var user = UserManager.FindByName(model.Email);
            //var user=UserManager.Users.FirstOrDefault(x=>x.PhoneNumber==model.Phone);
            var user = UserManager.FindByName(model.Phone);
            
            if (user != null)
            {
                var resultIsPhoneConfirmed = await UserManager.IsPhoneNumberConfirmedAsync(user.Id.ToString());
                
                //if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                if (!resultIsPhoneConfirmed)
                {
                    //ViewBag.Message = "Your email is not confirmed. We have sent you an email. Please click the link in the email.";
                    //ViewBag.Title = "Email Not Confirmed!";
                    ViewBag.Message = "Your phone is not confirmed. We have sent you an SMS. Please Reply Back my SMS to number indicated.";
                    ViewBag.Title = "SMS Not Confirmed!";
                    return View("Error");
                }


                if (!user.Active)
                {
                    ViewBag.Message = "Your account needs to be activated by a human after due diligence. Please wait for account activation.";
                    ViewBag.Title = "Account Not Activated";
                    return View("Error");
                }


                var result = await SignInManager.PasswordSignInAsync(model.Phone, model.Password, model.RememberMe, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:

                        return RedirectToAction("SaveLastLogin", new { Controller = "Users", Id = user.Id, returnUrl = returnUrl });
                    //return RedirectToLocal(returnUrl);

                    case SignInStatus.LockedOut:
                        return RedirectToAction("SaveLastLockout", "Users", new { Id = user.Id });
                    //return View("Lockout");

                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                    case SignInStatus.Failure:
                        return RedirectToAction("SaveLastLoginFailure", new { Controller = "Users", Id = user.Id });

                    default:
                        return RedirectToAction("SaveLastLoginFailure", new { Controller = "Users", Id = user.Id });
                    //ModelState.AddModelError("", "Invalid login attempt.");
                    //return View(model);

                }
            }
            ModelState.AddModelError("", "There was a problem. user not found.");
            return View(model);

        }

        //
        // GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    return View();
        //}

        [AllowAnonymous]
        public  ActionResult VerifyCode(string phoneNumber)
        {
            // Require that the user has already logged in via username/password or external login
            //if (!await SignInManager.HasBeenVerifiedAsync())
            //{
            //    return View("Error");
            //}
            //return View(new VerifyCodeViewModel { UserId=userId});
            VerifyPhoneNumberViewModel vpvm = new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber};
            //return VerifyCode (vpvm,"xxxx" );
            return View(vpvm);
        }


        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode(VerifyPhoneNumberViewModel model)
        {
            //the code used here is a dummy to help with redirection
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            //var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);


            var userFound = UserManager.FindByName(model.PhoneNumber);
            var result = UserManager.ChangePhoneNumber(userFound.Id, userFound.PhoneNumber, model.Code);
            
            //if the result is successful, then the phone number is now verified. Therefore switch on verify
            
            switch (result.Succeeded)
            {
                case true:
                    userFound.PhoneNumberConfirmed = true;
                    var updateresult =  UserManager.Update(userFound);
                    if (!updateresult.Succeeded)
                    {
                        string additionalMsg = "Update Failed. Enter New number";
                        SendSmsCode(userFound.Id, userFound.PhoneNumber, additionalMsg);

                    }
                    break;
                default:
                    //we need to do something here to fail a brute force attack
                    //Best is to fail silently.... and send a new SMS number
                    string msg = "Your try was wrong. If it was not you, then someone is trying to hack your number. Pls inform us immediately.";
                    SendSmsCode(userFound.Id, userFound.PhoneNumber, msg);

                    break;
            }


            switch (result.Succeeded)
            {
                case true: return View("Login", new LoginViewModel { Phone= userFound.PhoneNumber,Password=string.Empty});
                default:
                    ModelState.AddModelError("", "Unable to update user Verification.");
                    return View(model);
            }



        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string userCreatingAccount = model.Email;
                string userCreatingAccount = "";

                if (User!=null)//possibly another user will be entering another user's email
                {
                    if (!string.IsNullOrEmpty(User.Identity.Name))
                    {
                        userCreatingAccount = User.Identity.Name;
                    }
                }

                //name and phone numbers are the same. 


                var user = new User { UserName = model.Phone, PhoneNumber=model.Phone, CreatedDate = new DateTimeAdapter().UtcNow, CreatedUser = userCreatingAccount, LockoutEnabled=true, Active=false};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    try
                    {
                        return SendSmsCode(user.Id, model.Phone);
                        //return RedirectToAction("GetCodeOrLogin");
                    }
                    catch(Exception e)
                    {
                        string error= MakeErrorMesage(string.Format( "There was an error sending SMS to Phone: '{0}'",model.Phone),e);
                        ModelState.AddModelError("",error);
                    }
                }

                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult GetCodeOrLogin(VerifyPhoneNumberViewModel fVM)
        {
            return View(fVM);
        }
        /// <summary>
        /// This is a helper that sends out an SMS.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private ActionResult SendSmsCode(string userId,string phone,string AddMessage="")
        {
            string code = UserManager.GenerateChangePhoneNumberToken(userId, phone);

            try
            {
                if (!string.IsNullOrEmpty(AddMessage))
                {
                    AddMessage = " - " + AddMessage;
                }
                UserManager.SendSms(userId, string.Format("Please confirm your phone no: '{0}' by sending Code: {1} {2}", phone, code, AddMessage));
                VerifyPhoneNumberViewModel vpv=new VerifyPhoneNumberViewModel { PhoneNumber = phone };
                return RedirectToAction("GetCodeOrLogin", vpv);
            }
            catch 
            {

                throw;

            }

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
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // GET: /Account/ConfirmPhone
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmPhone(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            string phoneNumber= (await UserManager.FindByIdAsync(userId)).PhoneNumber;
            var result = await UserManager.ChangePhoneNumberAsync(userId, phoneNumber,code);

            return View(result.Succeeded ? "ConfirmPhone" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword(string phone="")
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel() { Phone = phone };
            return View(model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Phone);
                //var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                if( (!(await UserManager.IsPhoneNumberConfirmedAsync(user.Id))))
                {

                    ViewBag.Message = "Your phone is not confirmed. We have sent you an SMS. Please Reply Back my SMS to number indicated.";
                    ViewBag.Title = "SMS Not Confirmed!";
                    return View("Error");

                }
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
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
            //var user = await UserManager.FindByNameAsync(model.Email);
            var user = await UserManager.FindByNameAsync(model.Phone);

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
                var user = new User { UserName = model.Email, Email = model.Email };
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
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
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
        #endregion
    }
}