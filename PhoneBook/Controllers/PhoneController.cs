using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using PhoneBook.Filters;
using Entity;

namespace PhoneBook.Controllers
{
    [NdrAut]
    public class PhoneController : Controller
    {

        // GET: Phone
        public ActionResult PhoneList()
        {
            var Phoneb = new Business.PhonesBusiness();
            var Phonelist = new List<Phone>();

            var res = Phoneb.GetPhoneList();
            if (!res.IsSuccess)
            {
                return Content(
                    string.Format(
                        "<script>alert('{0}');</script>",
                        res.MSG));
            }

            return View(res.Data);
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {

            PhonesBusiness Phoneb = new PhonesBusiness();
            var res = Phoneb.GetPhoneList();
            if (!res.IsSuccess)
            {
                return Content(
                    string.Format(
                        "<script>alert('{0}');</script>",
                        res.MSG));
            }
            var Phone = res.Data.First(c => c.Id == id);

            return View(Phone);
        }
        [HttpPost]
        public ActionResult Edit(Phone Phone)
        {
            PhonesBusiness Phoneb = new PhonesBusiness();
            var res = Phoneb.UpdatePhone(Phone);
            if (!res.IsSuccess)
            {
                return Content(
                    string.Format(
                        "<script>alert('{0}');</script>",
                        res.MSG));
            }

            return RedirectToAction("PhoneList");

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Create(Phone Phone)
        {
            PhonesBusiness Phoneb = new PhonesBusiness();
            var res = Phoneb.AddPhone(Phone);
            if (!res.IsSuccess)
            {
                return Content(
                    string.Format(
                        "<script>alert('{0}');</script>",
                        res.MSG));
            }
            return RedirectToAction("PhoneList");
        }

       
        [HttpGet]
        public ActionResult Delete(int id)
        {
            PhonesBusiness Phoneb = new PhonesBusiness();
            var res = Phoneb.DeletePhone(id);
            if (!res.IsSuccess)
            {
                return Content(
                    string.Format(
                        "<script>alert('{0}');</script>",
                        res.MSG));
            }
            return RedirectToAction("PhoneList");
        }


    }
}