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


        
        // show the question and options

        public ActionResult Questions_Options_Page()
        {
           
            return View();
        }


        public JsonResult Show_QuestionOptions(int Test_Id)
        {
            var questionIds = db_context.Question_Id_Mapping1(Test_Id).ToList();

            var questions = db_context.Question_Table
                                       .Where(q => questionIds.Contains(q.Question_Id))
                                       .ToList(); 

            var Options = db_context.Option_Table.ToList();

            var questionOptions = questions.Select(q => new
            {
                Question_Id = q.Question_Id,
                Question_Text = q.Questions,
                Options = Options.Where(o => o.Question_Id == q.Question_Id)
                                 .Select(o => new { o.Option_Id, o.Option_Text })
                                 .ToList()
            }).ToList();

            return Json(questionOptions, JsonRequestBehavior.AllowGet);
        }
    }
}