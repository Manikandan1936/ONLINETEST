using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace MCQ_PROJECT.Controllers
{
    public class MCQController : Controller
    {
        //
        // GET: /MCQ/

        public ActionResult Admin_Login_Page()
        {

            return View();
        }

        public ActionResult Question_Page()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }

            else { return RedirectToAction("Admin_Login_Page"); }
        }

        Mutlipel_Choice_QuestionEntities1 db_context = new Mutlipel_Choice_QuestionEntities1();



        public JsonResult Admin_Login(string User_Name, string Password)
        {

            var Admin = db_context.Login_Admin(User_Name, Password).FirstOrDefault(a => a.User_Name == User_Name && a.Password == Password);

            if (Admin != null)
            {
                FormsAuthentication.SetAuthCookie(User_Name, false);
                Session["admin_id"] = Admin.Admin_Id;
                return Json(Admin, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Subject_Page()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }

            else { return RedirectToAction("Admin_Login_Page"); }
        }

        public JsonResult save_subjects(string Subjects,Subject_Table sub)
        {
              int admin_id = (int) Session["admin_id"];

             var subjects = db_context.save_subject(Subjects,admin_id);

             return Json(new { success = true, admin_id = admin_id }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Show_Subjects(DataTableParameters DT)
        {
            var subject = new DataTableResultSet_SubjectList();
            subject.draw = DT.Draw;


            List<Subject_Table> show_data = db_context.Subject_Table.ToList();

            subject.recordsTotal = show_data.Count;
            subject.recordsFiltered = show_data.Count;
            subject.data = show_data;

            return Json(subject, JsonRequestBehavior.AllowGet);

        }


        public JsonResult Store_Subjectid(int SUBJECT_ID)
        {
            Session["subject_id"] = SUBJECT_ID;

            return Json(JsonRequestBehavior.AllowGet);
        }


        public JsonResult Drop_Downlist()
        {
        var Subjects = db_context.Subject_Table.Select(s => new

            {
                Value = s.Subject_Id,
                Text = s.Subjects

            }).ToList();

        return Json(Subjects, JsonRequestBehavior.AllowGet);

        }


        public JsonResult Questions_Options(Question_Table Questions, List<Option_Table> Options)
        {
            var subject_id = (int) Session["subject_id"];
            var created_by = (int) Session["admin_id"];

            Questions.Subject_Id = (int)subject_id;
            Questions.Created_By = (int)created_by;

            Questions.Created_Date = DateTime.UtcNow;

            db_context.Question_Table.Add(Questions);
            db_context.SaveChanges();

            foreach (var option in Options)
            {
                option.Question_Id = Questions.Question_Id;
                db_context.Option_Table.Add(option); 
            }

            db_context.SaveChanges();

            return Json("success");
        }

        public ActionResult Test_Table_View()
        {
            return View();
        }

        public JsonResult save_testtable(Test_Table Test)
        {
            db_context.Test_Table.Add(Test);
            db_context.SaveChanges();

            return Json("Success");
        }


        public JsonResult show_testtable_data(DataTableParameters DT)
        {

            var Test = new DataTableResultSet_TestTableList();
            Test.draw = DT.Draw;

            List<Test_Table> Test_Table = db_context.Test_Table.ToList();

            Test.recordsTotal = Test_Table.Count;
            Test.recordsFiltered = Test_Table.Count;
            Test.data = Test_Table;

            return Json(Test, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Show_Questions()
        {
            return View();
        }


        public JsonResult Question_Datatable(DataTableParameters DT)
        {
            var Questions = new DataTableResultSet_Questionslist();
            Questions.draw = DT.Draw;

            var Question_Table = db_context.Question_Table.ToList();

            Questions.recordsTotal = Question_Table.Count;
            Questions.recordsFiltered = Question_Table.Count;
            Questions.data = Question_Table;

            return Json(Questions, JsonRequestBehavior.AllowGet);
        }




    }
}
