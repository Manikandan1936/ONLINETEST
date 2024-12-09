﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCQ_PROJECT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class Mutlipel_Choice_QuestionEntities1 : DbContext
    {
        public Mutlipel_Choice_QuestionEntities1()
            : base("name=Mutlipel_Choice_QuestionEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin_Table> Admin_Table { get; set; }
        public DbSet<Subject_Table> Subject_Table { get; set; }
        public DbSet<Option_Table> Option_Table { get; set; }
        public DbSet<Question_Table> Question_Table { get; set; }
        public DbSet<Test_Maping> Test_Maping { get; set; }
        public DbSet<User_Invaite_Table> User_Invaite_Table { get; set; }
        public DbSet<User_Table> User_Table { get; set; }
        public DbSet<Answer_Table> Answer_Table { get; set; }
        public DbSet<Test_Table> Test_Table { get; set; }
    
        public virtual ObjectResult<Login_Admin_Result> Login_Admin(string user_name, string password)
        {
            var user_nameParameter = user_name != null ?
                new ObjectParameter("user_name", user_name) :
                new ObjectParameter("user_name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Login_Admin_Result>("Login_Admin", user_nameParameter, passwordParameter);
        }
    
        public virtual int save_subject(string subjects, Nullable<int> created_by)
        {
            var subjectsParameter = subjects != null ?
                new ObjectParameter("subjects", subjects) :
                new ObjectParameter("subjects", typeof(string));
    
            var created_byParameter = created_by.HasValue ?
                new ObjectParameter("created_by", created_by) :
                new ObjectParameter("created_by", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("save_subject", subjectsParameter, created_byParameter);
        }
    
        public virtual ObjectResult<show_subjects_Result> show_subjects()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<show_subjects_Result>("show_subjects");
        }
    
        public virtual ObjectResult<string> Subjects_Dropdown(Nullable<int> subject_id)
        {
            var subject_idParameter = subject_id.HasValue ?
                new ObjectParameter("subject_id", subject_id) :
                new ObjectParameter("subject_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Subjects_Dropdown", subject_idParameter);
        }
    
        public virtual ObjectResult<Edit_Subjects_Result> Edit_Subjects(Nullable<int> subject_id)
        {
            var subject_idParameter = subject_id.HasValue ?
                new ObjectParameter("subject_id", subject_id) :
                new ObjectParameter("subject_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Edit_Subjects_Result>("Edit_Subjects", subject_idParameter);
        }
    
        public virtual int Updtae_Subjects(Nullable<int> subject_id, string subjects)
        {
            var subject_idParameter = subject_id.HasValue ?
                new ObjectParameter("subject_id", subject_id) :
                new ObjectParameter("subject_id", typeof(int));
    
            var subjectsParameter = subjects != null ?
                new ObjectParameter("subjects", subjects) :
                new ObjectParameter("subjects", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Updtae_Subjects", subject_idParameter, subjectsParameter);
        }
    
        public virtual ObjectResult<View_Questions_Result> View_Questions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<View_Questions_Result>("View_Questions");
        }
    
        public virtual int User_Email(string user_Email)
        {
            var user_EmailParameter = user_Email != null ?
                new ObjectParameter("User_Email", user_Email) :
                new ObjectParameter("User_Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("User_Email", user_EmailParameter);
        }
    
        public virtual ObjectResult<After_Login_Result> After_Login(string user_email)
        {
            var user_emailParameter = user_email != null ?
                new ObjectParameter("user_email", user_email) :
                new ObjectParameter("user_email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<After_Login_Result>("After_Login", user_emailParameter);
        }
    
        public virtual int Show_Options(Nullable<int> question_id)
        {
            var question_idParameter = question_id.HasValue ?
                new ObjectParameter("question_id", question_id) :
                new ObjectParameter("question_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Show_Options", question_idParameter);
        }
    
        public virtual int Show_Questions(Nullable<int> question_id)
        {
            var question_idParameter = question_id.HasValue ?
                new ObjectParameter("question_id", question_id) :
                new ObjectParameter("question_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Show_Questions", question_idParameter);
        }
    
        public virtual ObjectResult<Show_Options1_Result> Show_Options1(Nullable<int> question_id)
        {
            var question_idParameter = question_id.HasValue ?
                new ObjectParameter("question_id", question_id) :
                new ObjectParameter("question_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Show_Options1_Result>("Show_Options1", question_idParameter);
        }
    
        public virtual ObjectResult<Show_Questions1_Result> Show_Questions1(Nullable<int> question_id)
        {
            var question_idParameter = question_id.HasValue ?
                new ObjectParameter("question_id", question_id) :
                new ObjectParameter("question_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Show_Questions1_Result>("Show_Questions1", question_idParameter);
        }
    
        public virtual int Question_Mapping(Nullable<int> test_id)
        {
            var test_idParameter = test_id.HasValue ?
                new ObjectParameter("test_id", test_id) :
                new ObjectParameter("test_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Question_Mapping", test_idParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Question_Id_Mapping(Nullable<int> test_id)
        {
            var test_idParameter = test_id.HasValue ?
                new ObjectParameter("test_id", test_id) :
                new ObjectParameter("test_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Question_Id_Mapping", test_idParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Question_Id_Mapping1(Nullable<int> test_id)
        {
            var test_idParameter = test_id.HasValue ?
                new ObjectParameter("test_id", test_id) :
                new ObjectParameter("test_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Question_Id_Mapping1", test_idParameter);
        }
    
        public virtual ObjectResult<After_Login_Email_Result> After_Login_Email(string user_email)
        {
            var user_emailParameter = user_email != null ?
                new ObjectParameter("user_email", user_email) :
                new ObjectParameter("user_email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<After_Login_Email_Result>("After_Login_Email", user_emailParameter);
        }
    }
}
