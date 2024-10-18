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
    }
}
