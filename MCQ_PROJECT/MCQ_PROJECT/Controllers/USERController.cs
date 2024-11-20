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

            var Name = db_context.User_Table.FirstOrDefault(u => u.Name == User_Name);

            if (Name == null)
            {
                ViewBag.login = "Login Failed: Username is incorrect.";
                return View("User_Login");
            }

            var User = db_context.User_Table.FirstOrDefault(u => u.Name == User_Name && u.Email_Id == Email);

            if (User == null)
            {
                ViewBag.login = "Login Failed: Email is incorrect.";
                return View("User_Login");
            }

            Session["User_ID"] = User.User_Id;
            Session["Email_Id"] = User.Email_Id;

            ViewBag.login = "User Login Successful";
            return RedirectToAction("After_Login", "USER");
        }

        public ActionResult After_Login()
        {
            return View();
        }


        public JsonResult Login_Data(DataTableParameters DT, int Test_Id)
        {
            var Test = new DataTableResultSet_TestTableList1();
            Test.draw = DT.Draw;


            //int user_id = Convert.ToInt32( Session["User_ID"] );

            var data_table = db_context.Test_Table.Where(e => e.Test_Id == Test_Id).ToList();

            Test.recordsTotal = data_table.Count;
            Test.recordsFiltered = data_table.Count;
            Test.data = data_table;

            return Json(Test, JsonRequestBehavior.AllowGet);

        }

      
        // start test

        public ActionResult Start_Test()
        {

            return View();
        }

    }
}