namespace SDSailsAdminUI.Controllers
{
    using SDSailsBusiness.Implementation;
using SDSailsBusiness.Interfaces;
using SDSailsCommon.Utility;
using SDSailsModel.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

    public class LoginController : Controller
    {
        public const string invalidLoginErrorMessage = "Invalid login. Try again.";

        private readonly ICommonBL _commonBL;

        public LoginController(ICommonBL commonBL)
        {
            _commonBL = commonBL;
        }

        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(UserInfo userInfo) 
        {
            bool isValidUser = false;
            try
            {
            
                if (ModelState.IsValid)
                {
                    if ((!string.IsNullOrEmpty(userInfo.UserName)) && (!string.IsNullOrEmpty(userInfo.Password)))
                    {

                        UserInfo userInformation = _commonBL.Login(userInfo);

                        Session["IsLoggedInUserId"] = userInformation.UserID;

                        if (userInformation != null)
                        {
                            isValidUser = true;
                        }

                        if (isValidUser)
                        {
                            FormsAuthentication.SetAuthCookie("LoggedInUser", false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.LoginErrorMessage = invalidLoginErrorMessage;
                            return View("Login", userInfo);
                        }
                    }
                    else
                    {
                        //ViewBag.LoginErrorMessage = invalidLoginErrorMessage;
                        return View("Login", userInfo);
                    }

                }
            }
            catch (Exception ex)
            {
                ServerLog.LogIt("ValidateLogin Controller Failure", 
                    ServerLog.DisplayInTextArea(ServerLog.DisplayInTextArea(ServerLog.ObjectToXMLString(ex.Message))));
            }

            return View("Login", userInfo);
        }

        [HttpGet]
        //[Authorize]
        public ActionResult Logout()
        {
            UserInfo userInfo = new UserInfo();

            /*Tip: Session.Clear does not kill a Session, it clears all values. 
            Session.Abandon actually kills the Session.*/
            Session.Abandon();

            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            return View("Login", userInfo);
        }
	}
}