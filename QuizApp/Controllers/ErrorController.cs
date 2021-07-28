using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult TotalError(string errorMsg, int code)
        {
            ViewData["errorMsg"] = errorMsg;
            ViewData["code"] = code;
            return View();
        }
        public ActionResult TotalErrorMsg(string errorMsg)
        {
            ViewData["errorMsg"] = errorMsg;
            return View();
        }
    }
}