
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using mvcSample.DataAccess;

namespace mvcSample.Controllers
{
    public class HomeController : Controller
    {
        [MyActionFilter]
        public ActionResult Index()
        {
            List<AspNetUser> users;
            using (var dbContext = new aspnetUsersDataContext())
            {
               users =  dbContext.AspNetUsers.ToList();

            }
           
            users.Add(new AspNetUser(){ UserName = @"I'm from an azure database"});
            return View(users);
        }

        public ActionResult About(string name="deafult")
        {
            ViewBag.Message = "Your application description page. " + name;
           
            return View();
        }

        public ActionResult Contact()
        {
          

            return View();
        }

        [HttpPost]
        [Route("bchen/{id}")] //must access via localhost:1101/bchen/2
        public JsonResult GetData(int id)
        {
            return  Json(new
            {
                data = "mydata",
                id
            });
        }

        public class MyType
        {
            [Required]
            public string Name { get; set; }
            
            public int Age { get; set; }
        }

        /*
         HTTP Methods. The framework only chooses actions that match the HTTP method of the request, determined as follows:
        You can specify the HTTP method with an attribute: AcceptVerbs, HttpDelete, HttpGet, HttpHead, HttpOptions, HttpPatch, HttpPost, or HttpPut.
        Otherwise, if the name of the controller method starts with "Get", "Post", "Put", "Delete", "Head", "Options", or "Patch", then by convention the action supports that HTTP method.
        If none of the above, the method supports POST.
         * 
         * how to pass JSON using http get?
         * $.ajax({
                    url: 'http://localhost:1104/Home/GetData',
                    type: 'GET',
                    data: { Name:'bo',Age:34 },
                    success: function(result) {
                        // process the results
                    }
                });
         * ===================>    GET http://localhost:1104/Home/GetData?Name=bo&Age=34   you don't have to use URLEncode becuase JSON model binder will do the work       
         */
        public JsonResult GetData(MyType input)
        {
            if (ModelState.IsValid)
            {
                return Json(input, JsonRequestBehavior.AllowGet);
            }

            return Json(new {Error = "invalid input"}, JsonRequestBehavior.AllowGet);

        }

    }
}