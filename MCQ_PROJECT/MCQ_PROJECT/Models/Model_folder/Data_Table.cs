using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MCQ_PROJECT
{
    public class Data_Table
    {
    }


    public class DataTableParameters
    {

        public List<DataTableColumn> Columns { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public List<DataOrder> Order { get; set; }
        public Search Search { get; set; }
        public int Start { get; set; }
        public string TransactionId { get; set; } //Added the purpose of getting additional paramenter 
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int IsPaid { get; set; }
        public string Date { get; set; }
        public string FileNumber { get; set; }
        public string selectbutton { get; set; }
        public string CreatedDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SelectedValue { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Filter { get; set; }
    }

    public class Search
    {
        public bool Regex { get; set; }
        public string Value { get; set; }
    }

    public class DataTableColumn
    {
        public int Data { get; set; }
        public string Name { get; set; }
        public bool Orderable { get; set; }
        public bool Searchable { get; set; }
        public Search Search { get; set; }

    }

    public class DataOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }


    }

    public class DataTableResultSet_SubjectList
    {
        /// <summary>Array of records. Each element of the array is itself an array of columns</summary>
        //public List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        //public List<AdminEntity> ContainerData = new List<AdminEntity>();

        public List<Subject_Table> data = new List<Subject_Table>();

        public List<Subject_Table> ContainerData = new List<Subject_Table>();


        /// <summary>value of draw parameter sent by client</summary>
        public int draw;

        /// <summary>filtered record count</summary>
        public int recordsFiltered;

        /// <summary>total record count in resultset</summary>
        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class DataTableResultSet_TestTableList
    {


        public List<Test_Table> data = new List<Test_Table>();

        public List<Test_Table> ContainerData = new List<Test_Table>();


        /// <summary>value of draw parameter sent by client</summary>
        public int draw;

        /// <summary>filtered record count</summary>
        public int recordsFiltered;

        /// <summary>total record count in resultset</summary>
        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class DataTableResultSet_Questionslist
    {

        public List<Question_Table> data = new List<Question_Table>();

        public List<Question_Table> ContainerData = new List<Question_Table>();
       
        public int draw;
      
        public int recordsFiltered;
      
        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class DataTableResultSet_QuestionView
    {

        public List<View_Questions_Result> data = new List<View_Questions_Result>();

        public List<View_Questions_Result> ContainerData = new List<View_Questions_Result>();

        public int draw;

        public int recordsFiltered;

        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class DataTableResultSet_Test_Maping
    {

        public List<Test_Maping> data = new List<Test_Maping>();

        public List<Test_Maping> ContainerData = new List<Test_Maping>();

        public int draw;

        public int recordsFiltered;

        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class DataTableResultSet_User_Email
    {

        public List<User_Invaite_Table> data = new List<User_Invaite_Table>();

        public List<User_Invaite_Table> ContainerData = new List<User_Invaite_Table>();

        public int draw;

        public int recordsFiltered;

        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class DataTableResultSet_User_Table
    {

        public List<User_Table> data = new List<User_Table>();

        public List<User_Table> ContainerData = new List<User_Table>();

        public int draw;

        public int recordsFiltered;

        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


}