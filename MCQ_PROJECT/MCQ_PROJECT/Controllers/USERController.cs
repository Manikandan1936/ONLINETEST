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

        public ActionResult Login_User(FormCollection User_Data, Test_Table Test)
        {
            var User_Name = User_Data["user_login"].ToString();
            var Email = User_Data["login_email"].ToString();

            var User = db_context.User_Table.FirstOrDefault(u => u.Name == User_Name && u.Email_Id == Email);

            if (User != null)
            {
                Session["Email"] = User.Email_Id;
                ViewBag.login = " Login successfull";
                return RedirectToAction("After_Login", "USER");
            }

            else
            {
                return View("User_Login");
            }
          
        }

        public ActionResult After_Login()
        {
            return View();
        }


        public JsonResult Login_Data()
        {
            var Email = Session["Email"].ToString();


            var Get_Email = db_context.After_Login(Email).ToList();



            return Json(Get_Email,JsonRequestBehavior.AllowGet);
        }

      
        // Instruction page test

        public ActionResult Instruction_Page()
        {

            return View();
        }


        public JsonResult Run_Time(int testId )
        {
            var testDetails = db_context.Test_Table.FirstOrDefault(t => t.Test_Id == testId);

            return Json(testDetails, JsonRequestBehavior.AllowGet);
           
        }
        // show the question and options

        public ActionResult Questions_Options_Page()
        {
           
            return View();
        }


        public JsonResult Show_QuestionOptions(int Test_Id)
        {
            var questions = db_context.Show_Questions(Test_Id).ToList();

            var options = db_context.Show_Options(Test_Id).ToList();

            var Question_Options = new
            {

                Questions = questions,
                Options = options
            };

            return Json(Question_Options, JsonRequestBehavior.AllowGet);
        }
    }
}