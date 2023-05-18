using Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhoneBook.Controllers
{
    public class NdrAccController : Controller
    {
        // GET: NdrAccount
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password,string returnUrl)
        {
            if (password == "a" && username != null)
            {
                FormsAuthentication.SetAuthCookie(username,
                    createPersistentCookie: false);

                HttpContext.Response.Cookies.Add(
                    new HttpCookie("username", username));

                return Redirect(returnUrl ?? Url.Action(
                    actionName: EnglishResourse.PhoneListAction
                    , controllerName: EnglishResourse.PhoneController));

            }
            else
            {
                ModelState.AddModelError(PhoneResource.incorect,
                    PhoneResource.incorect);
                return View();
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action(
                    actionName: EnglishResourse.PhoneListAction
                    , controllerName: EnglishResourse.PhoneController));
        }


    }
}