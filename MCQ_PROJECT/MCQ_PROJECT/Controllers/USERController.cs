using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Http;

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

            Session["user_id"] = User.User_Id;

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

        public JsonResult Dictionary_Values( int Question_Id,int Option_Id)
        {

            Dictionary<int, int> answers = new Dictionary<int, int>();

            if (Session["answers"] == null)
            {
                answers.Add(Question_Id, Option_Id);
                Session["answers"] = answers;
            }
            else
            {
                Dictionary<int, int> Convert_value = (Dictionary<int, int>)Session["answers"];
                Convert_value.Add(Question_Id,Option_Id);

                Session["answers"] = Convert_value;
            }

            return Json("Success");
        }


        public JsonResult Save_Answer(int Test_Id, int Question_Id, int Option_Id)
        {
             

            Dictionary<int, int> Last_Answer = new Dictionary<int, int>();

            if (Session["last_answer"] == null)
            {
                Last_Answer.Add(Question_Id, Option_Id);
                Session["last_answer"] = Last_Answer;
            }

            var Answers = Session["answers"] as Dictionary<int, int>;
            var Submit_Answer = Session["last_answer"] as Dictionary<int, int>;

            foreach (var Get_Answer in Answers)
            {
                var Ans = new Answer_Table
                {
                    Question_Id = Get_Answer.Key,
                    Option_Id = Get_Answer.Value,
                    Tset_Id = Test_Id,
                    User_Id = Convert.ToInt32(Session["user_id"]),
                    TestAttended_Date = DateTime.Now,
                    Attend_Test = true
                };
                db_context.Answer_Table.Add(Ans);
            }

            foreach (var Final_Answer in Submit_Answer)
            {
                var Save = new Answer_Table
                {
                    Question_Id = Final_Answer.Key,
                    Option_Id = Final_Answer.Value,
                    Tset_Id = Test_Id,
                    User_Id = Convert.ToInt32(Session["user_id"]),
                    TestAttended_Date = DateTime.Now,
                    Attend_Test = true
                };
                db_context.Answer_Table.Add(Save);
            }

            db_context.SaveChanges();
            return Json("success");
        }

      
        public JsonResult UpdateTestStatus(int testId)
        {
           
                // Find the test record by Test_Id
            var test = db_context.Test_Table.SingleOrDefault(t => t.Test_Id == testId);

                if (test != null)
                {
                    test.Status = true; 
                    db_context.SaveChanges();
                    return Json(new { success = true, message = "Test status updated successfully." });
                }

                return Json(new { success = false, message = "Test not found." });
            }
        

    }
}