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
            
                return View();
        }

        Mutlipel_Choice_QuestionEntities1 db_context = new Mutlipel_Choice_QuestionEntities1();

        //login admin

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
              return View();
        }

        //save subjects

        public JsonResult save_subjects(string Subjects, Subject_Table sub)
        {
            int admin_id = (int)Session["admin_id"];

            var subjects = db_context.save_subject(Subjects, admin_id);

            return Json(new { success = true, admin_id = admin_id }, JsonRequestBehavior.AllowGet);
        }


        //show subjects in datatable

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

        //edit subject table

        public JsonResult Edit_Subject(int Subject_Id)
        {
            var Edit_Subjects = db_context.Edit_Subjects(Subject_Id).FirstOrDefault();

            return Json(Edit_Subjects, JsonRequestBehavior.AllowGet);
        }

        // update subjecs

        public JsonResult Subjects_Update(int Subjects_Id, string Subjects)
        {
            var Update_Subject = db_context.Subject_Table.FirstOrDefault(s => s.Subject_Id == Subjects_Id);

            Update_Subject.Subjects = Subjects;
            db_context.SaveChanges();

            return Json(Update_Subject, JsonRequestBehavior.AllowGet);
        }

        //store subject id

        public JsonResult Store_Subjectid(int SUBJECT_ID)
        {
            Session["subject_id"] = SUBJECT_ID;

            return Json(JsonRequestBehavior.AllowGet);
        }


        //show subjects in dropdownlist
        public JsonResult Drop_Downlist()
        {
            var Subjects = db_context.Subject_Table.Select(s => new

                {
                    Value = s.Subject_Id,
                    Text = s.Subjects

                }).ToList();

            return Json(Subjects, JsonRequestBehavior.AllowGet);

        }


        // save questions and options

        public JsonResult Questions_Options(Question_Table Questions, List<Option_Table> Options)
        {
            var subject_id = (int)Session["subject_id"];
            var created_by = (int)Session["admin_id"];

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


        //save test in test table
        public JsonResult save_testtable(Test_Table Test)
        {
            db_context.Test_Table.Add(Test);
            db_context.SaveChanges();

            return Json("Success");
        }


        // show test table data in datatable

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


        // view questions in datatable

        public ActionResult View_Questions()
        {
            return View();

        }

        public JsonResult Question_View(DataTableParameters DT, int Test_Id)
        {
            var Questions = new DataTableResultSet_QuestionView();
            Questions.draw = DT.Draw;

            var View_Questions = db_context.View_Questions().Where(v => v.Test_Id == Test_Id).ToList();

            Questions.recordsTotal = View_Questions.Count();


            Questions.recordsTotal = View_Questions.Count;
            Questions.recordsFiltered = View_Questions.Count;
            Questions.data = View_Questions;

            return Json(Questions, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Show_Questions()
        {
            return View();
        }


        // show the questions in datatable
        public JsonResult Question_Datatable(DataTableParameters DT, int subjectId)
        {
            var Questions = new DataTableResultSet_Questionslist();
            Questions.draw = DT.Draw;

            var Question_Table = db_context.Question_Table.Where(s => s.Subject_Id == subjectId).ToList();

            //var Question_Table = db_context.Question_Table.ToList();


            Questions.recordsTotal = Question_Table.Count;
            Questions.recordsFiltered = Question_Table.Count;
            Questions.data = Question_Table;

            return Json(Questions, JsonRequestBehavior.AllowGet);
        }


        // show the subjects in dropdown list
        public JsonResult Choose_Subjects()
        {
            var Subjects = db_context.Subject_Table.Select(s => new

            {
                Value = s.Subject_Id,
                Text = s.Subjects

            }).ToList();

            return Json(Subjects, JsonRequestBehavior.AllowGet);
        }



        // test mapping table

        public JsonResult Test_maping( List<int> Question_Id)
        {
            var created_by = (int)Session["admin_id"];
            var test_id = (int)Session["test_id"];

            foreach (var questionId in Question_Id)
            {
                var testMapping = new Test_Maping
                {
                    Created_By = created_by,
                    Test_Id = test_id,
                    Question_Id = questionId,
                    Created_Date = DateTime.UtcNow
                };

                db_context.Test_Maping.Add(testMapping);
            }

            db_context.SaveChanges();

            return Json("success");

        }

        // show test mapping table

        public ActionResult Test_Mapping()
        {
            return View();
        }

        public JsonResult Show_Testmappingdata(DataTableParameters DT)
        {
            var Test = new DataTableResultSet_Test_Maping();
            Test.draw = DT.Draw;

            var Test_Mapping = db_context.Test_Maping.ToList();

            Test.recordsTotal = Test_Mapping.Count;
            Test.recordsFiltered = Test_Mapping.Count;
            Test.data = Test_Mapping;

            return Json(Test, JsonRequestBehavior.AllowGet);
        }

        // show the test in driodown list

        public JsonResult Select_Test()
        {
            var Subjects = db_context.Test_Table.Select(s => new

            {
                Value = s.Test_Id,
                Text = s.Test_Name

            }).ToList();

            return Json(Subjects, JsonRequestBehavior.AllowGet);

        }

        // get the test id

        public JsonResult Store_TestId(int TEST_ID)
        {
            Session["test_id"] = TEST_ID;

            return Json(JsonRequestBehavior.AllowGet);
        }





        public ActionResult Invaite_User()
        {

            return View();
        }

        public JsonResult Save_Email( List<string> Email)
        {

            //var split_emails = Email.Select(e => e.Trim()).ToList();

            foreach (var email in Email)
            {
                db_context.User_Invaite_Table.Add(new User_Invaite_Table { User_Email = email });
            }

       
            db_context.SaveChanges();

            return Json("success");
        }

    }
}
