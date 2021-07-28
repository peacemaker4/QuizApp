using Newtonsoft.Json;
using QuizApp.DB;
using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class TestController : Controller
    {
        private static Random rng = new Random();
        Context db;
        public TestController()
        {
            db = new Context();
        }
        public ActionResult Index()
        {
            if (Request.Cookies["user"] == null)
            {
                return RedirectToAction("Enter", "User", null);
            }
            else
            {
                var user=JsonConvert.DeserializeObject<User>(Request.Cookies["user"].Value);
                ViewData["userFirstName"] = user.FirstName;
                ViewData["userLastName"] = user.LastName;
            }
            return View();
        }
        [HttpGet]
        public ActionResult TakeTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TakeTest(BuildTest test)
        {
            if (ModelState.IsValid)
            {
                if(test!=null)
                    return RedirectToAction("TestPage", new { questions = test.Questions, testType=test.TestType });
            }
            return View();
        }
        [HttpGet]
        public ActionResult TestPage(int questions, string testType)
        {
            List<Test> myTest = new List<Test>();
            switch (testType)
            {
                case "Mix":
                    myTest= db.Tests.OrderBy(x=>Guid.NewGuid()).Take(questions).ToList();
                    break;
                case "Numbers":
                    myTest = db.Tests.Where(x=>x.Prompt=="число").OrderBy(x => Guid.NewGuid()).Take(questions).ToList();
                    break;
                case "Strings":
                    myTest = db.Tests.Where(x => x.Prompt == "строка").OrderBy(x => Guid.NewGuid()).Take(questions).ToList();
                    break;
            }
            ViewData["MyTest"] = myTest;
            TempData["TimeStart"] = DateTime.Now;
            return View();
        }
        [HttpPost]
        public ActionResult TestPage()
        {
            Dictionary<int, string> myDict =
           new Dictionary<int, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("question"))
                {
                    var kkey=  Regex.Replace(key, "[^0-9]+", string.Empty);
                    myDict.Add(Convert.ToInt32(kkey),Convert.ToString(Request.Form[key]));
                }
            }
            int answers = 0;
            foreach (var item in myDict)
            {
                var quest= db.Tests.SingleOrDefault(t => t.Id == item.Key);
                if (quest.Answer.ToLower() == item.Value.ToLower())
                {
                    answers++;
                }
            }
            return RedirectToAction("TestPageResult", new { questions = myDict.Count, answers = answers });
        }
        [HttpGet]
        public ActionResult TestPageResult(int questions, int answers)
        {
            var now = DateTime.Now;
            var startTime = Convert.ToDateTime(TempData["TimeStart"]);
            var user = JsonConvert.DeserializeObject<User>(Request.Cookies["user"].Value);
            var result = new UserTest() { Questions = questions, Answers = answers, FirstName = user.FirstName, LastName=user.LastName, StartTime = startTime, EndTime = now };
            db.UserTests.Add(result);
            db.SaveChanges();

            ViewData["Questions"] = questions;
            ViewData["Answers"] = answers;
            if (answers == 0)
                ViewData["Percent"] = 0;
            else
                ViewData["Percent"] = 100 / (Convert.ToDouble(questions) /(answers));
            var spend = (now - startTime);
            ViewData["Spend"] = spend.Minutes+" мин "+ spend.Seconds +" сек";
            
            return View();
        }
        [HttpGet]
        public ActionResult ResultsTimeSelect()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResultsTimeSelect(ResultTime time)
        {
            return RedirectToAction("GetTestResults", new { start = time.StartTime, end = time.EndTime });
        }
        [HttpGet]
        public ActionResult GetTestResults(DateTime start, DateTime end)
        {
            List<UserTest> tests = db.UserTests.Where(d => d.StartTime >= start && d.EndTime <= end).ToList();
            ViewData["Results"] = tests;
            return View();
        }
    }
}