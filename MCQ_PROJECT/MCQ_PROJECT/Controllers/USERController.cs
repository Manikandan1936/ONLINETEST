using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCQ_PROJECT.Controllers
{
    public class USERController : Controller
    {
        //
        // GET: /USER/

        Mutlipel_Choice_QuestionEntities1 db_context = new Mutlipel_Choice_QuestionEntities1();



        public ActionResult User_Registration()
        {
            return View();
        }

        // user registration

        public JsonResult User_Registrations(User_Table User_Register)
        {
            db_context.User_Table.Add(User_Register);
            db_context.SaveChanges();

            return Json("success");
        }

        // show user datas

        public JsonResult Show_Userdata(DataTableParameters DT)
        {
            var User = new DataTableResultSet_User_Table();
            User.draw = DT.Draw;

            var User_Datas = db_context.User_Table.ToList();

            User.recordsTotal = User_Datas.Count;
            User.recordsFiltered = User_Datas.Count;
            User.data = User_Datas;

            return Json(User, JsonRequestBehavior.AllowGet);
        }

        public ActionResult User_Login()
        {
            return View();
        }

        // user login

        public ActionResult Login_User(FormCollection User_Data)
        {
            var User_Name = User_Data["user_login"].ToString();
            var Email = User_Data["login_email"].ToString();

            var User = db_context.User_Table.FirstOrDefault(u => u.Name == User_Name && u.Email_Id == Email);

            if (User != null)
            {
                ViewBag.login = "User Login Successfull";
                return RedirectToAction("User_Registration", "USER");
            }

            else {

                ViewBag.login = "Login Falied";
                return View("User_Login");
            }

        }
        
    }
}