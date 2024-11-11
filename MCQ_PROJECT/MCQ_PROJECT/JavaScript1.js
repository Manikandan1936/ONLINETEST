$(document).ready(function () {
    $('#loginForm').on('submit', function (e) {
        e.preventDefault();

        var userName = $('#user').val();
        var password = $('#password').val();

        $.ajax({
            type: "POST",
            url: '/MCQ/Admin_Login',
            data: { User_Name: userName, Password: password },
            success: function (response) {
                
                window.location.href = "/MCQ/Subject_Page";
            },
            error: function () {
                alert('An error occurred. Please try again.');
            }
        });
    });
});


//save subjects

$(document).ready(function () {
    $('#subject').on('click', function (e) {
        e.preventDefault();


        var subjects = $('#sub').val();
       //var created = 0;

        $.ajax({
            type: "POST",
            url: '/MCQ/save_subjects',
            data: { Subjects: subjects},
            success: function (response) {
                if (response.success) {
                 
                    alert(' successful! subject inserted' + response.admin_id   );
                   
                } else {
                    
                    alert('insert falid');
                }
            },
            error: function () {
                alert('An error occurred. Please try again.');
            }
        });
    });
});


//show subjects in datatable

$(document).ready(function () {

    $("#Subject_Table").DataTable({

        "destroy": true,
        "pagingtype": "full_numbers",
        "ordering": false,

        ajax: {
            type: "GET",
            url: "/MCQ/Show_Subjects",
            contentType: "application/json",

            data: function (d) {

                alert(JSON.stringify(d));
                return JSON.stringify(d);
            },

            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            },
        },

        columns: [
             { "data": "Subject_Id", "autowidth": true },
             { "data": "Subjects", "autowidth": true },
             { "data": "Created_By", "autowidth": true },

             {
                 mRender: function (data, type, row) {
                     return '<a onclick="Edit_Subject (' + row.Subject_Id + ')" class = "btn btn-primary">EDIT </a>'
                 }
             },

              {
                  mRender: function (data, type, row) {
                      return '<a onclick="Delete_Subject (' + row.Subject_Id + ')" class = "btn btn-danger">DELETE </a>'
                  }
              },

        ]

    });
});

//edit subject table

function Edit_Subject(subject_id) {
    $('#sub_id');
    $('#sub');
    
    $.ajax({
        url: "/MCQ/Edit_Subject",
        type: "GET",
        data: { Subject_Id: subject_id },
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#sub_id').val(result.SUBJECT_ID);
            $('#sub').val(result.Subjects);
          
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


//update subjects

function Update_Subjects() {

    var Update_Subject = {
        Subject_Id: $('#sub_id').val(),
        Subjects: $('#subject').val(),
        
    };
    $.ajax({
        url: "/MCQ/Subjects_Update",
        data: Update_Subject,
        type: "POST",
       
        dataType: "json",
        success: function (result) {
           
            $('#sub_id').val("");
            $('#subject').val("");
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//show the subjects in dropdown list

 $(document).ready(function () {
        $.ajax({
            url: '/MCQ/Drop_Downlist',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                //alert(JSON.stringify(data))
                var $dropdown = $('#subjectDropdown');
                $dropdown.empty();
                $dropdown.append('<option value="">Select a subject</option>');
                $.each(data, function (index, subject) {
                    $dropdown.append('<option value="' + subject.Value + '">' + subject.Text + '</option>');
                });
            },
            error: function (xhr, status, error) {
                console.error('Error fetching subjects: ' + error);
            }
        });

        $('#subjectDropdown').change(function () {
            var selectedSubjectId = $(this).val();
            $.ajax({
                url: '/MCQ/Store_Subjectid',
                type: 'POST',
                data: { SUBJECT_ID: selectedSubjectId },
                success: function (response) {  
                    //alert(response); 
                },
                error: function () {
                    alert("Error saving subject ID.");
                }
            });
        });


    });


// create questions and options

 $(document).ready(function(){

     $('#btn-question').click(function (e) {
         e.preventDefault();

         var questionData = {
             Questions: $('#question').val(),
          
         };

         var optionData = [
             {
                 Options: $('#options').val(),
                 Option_Text: $('#option_text').val(),
                 Is_Correct: $('#select_true_false').val() === "1",},

           {  Options: $('#options_b').val(),
               Option_Text: $('#option_text_two').val(),
               Is_Correct: $('#select_true_false_two').val() === "1",},

           {  Options: $('#options_c').val(),
               Option_Text: $('#option_text_three').val(),
               Is_Correct: $('#select_true_false_three').val() === "1",},

          {
              Options: $('#options_d').val(),
              Option_Text: $('#option_text_four').val(),
              Is_Correct: $('#select_true_false_four').val() === "1",
          }


             
             ];

         $.ajax({
             url: '/MCQ/Questions_Options',
             type: "POST",
             contentType: 'application/json; charset=utf-8',
             data:  JSON.stringify( { 
                 Questions: questionData,
                 Options: optionData
             }),
             success: function (response) {
                 alert(  JSON.stringify( response));
             },
             error: function () {
                 alert("An error occurred.");
             }
         });
     });

 })

 //save test table

 $(document).ready(function () {
     $('#btn-test-table').on('click', function (e) {
         e.preventDefault();

         var test = {

             Test_Name: $('#test-name').val(),
             Created_Date: $('#create_date').val(),
             End_Date: $('#end_date').val(),
             Duration: $('#duration').val()
         }
         $.ajax({
             type: "POST",
             url: '/MCQ/save_testtable',
             contentType: 'application/json; charset=utf-8',
             data: JSON.stringify({ Test: test }),
             success: function (response) {

             },
             error: function () {
                 alert('An error occurred. Please try again.');
             }

         });
     });
 });


 //show test table datas

 $(document).ready(function () {

     $("#Test_Table").DataTable({

         "destroy": true,
         "pagingtype": "full_numbers",
         "ordering": false,

         ajax: {
             type: "GET",
             url: "/MCQ/show_testtable_data",
             contentType: "application/json",


             data: function (d) {

                 alert(JSON.stringify(d));
                 return JSON.stringify(d);
             },

             error: function (xhr, err) {
                 alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                 alert("responseText: " + xhr.responseText);
             },
         },

         columns: [
              { "data": "Test_Id", "autowidth": true },
              { "data": "Test_Name", "autowidth": true },

            {
                "data": "Created_Date",
                "render": function (data) {
                    return moment(data).format("DD/MM/YYYY HH:mm:ss");
                }, "autowidth": true
            },
               
               {
                   "data": "End_Date",
                   "render": function (data) {
                       return moment(data).format("DD/MM/YYYY HH:mm:ss");
                   }, "autowidth": true
               },

                 {
                     "data": "Duration",
                     "render": function (data) {
                         return data.Hours + ":" + data.Minutes;
                     }, "autowidth": true
                 },


                  {
                      mRender: function (data, type, row) {

                          return '<a href="/MCQ/Question_Page" (' + row.Test_Id + ')" class = "btn btn-success">ADD QUESTIONS</a>'
                      }
                  },

                   {
                       mRender: function (data, type, row) {

                           return '<a  onclick="viewQuestions (' + row.Test_Id + ')" class = "btn btn-dark">VIEW QUESTIONS</a>'
                       }
                   },

                   {
                       mRender: function (data, type, row) {

                           return '<a  onclick= "get_testid (' + row.Test_Id + ')" class = "btn btn-dark">INVAITE USER</a>'
                       }
                   },
         ]

     });
 });


//view questions

 function viewQuestions(testId) {
     window.location.href = "/MCQ/View_Questions?Test_Id=" + testId;
 }


 $(document).ready(function () {
     var urlParams = new URLSearchParams(window.location.search);
     var testId = urlParams.get('Test_Id');

     alert(urlParams);

     if (testId) {
         $.ajax({
             url: '/MCQ/Question_View',
             type: "GET",
             data: { Test_Id: testId },
             success: function (data) {
                 $('#View_Questions').DataTable({
                     data: data.data, 
                     columns: [
                         { "data": "Question_Id", "autoWidth": true },
                         { "data": "Subject_Id", "autoWidth": true },
                         { "data": "Test_Id", "autoWidth": true },
                         { "data": "Questions", "autoWidth": true },
                     ],
                     destroy: true,
                     autoWidth: true,
                     paging: true, 
                     searching: true 
                 });
             },
             error: function (xhr, status, error) {
                 alert("An error occurred: " + error);
             }
         });
     }
 });


 // show the questions in datatable

 $(document).ready(function () {
    
     function loadQuestionsTable(subjectId) {

         $("#Questions_Table").DataTable({

             "destroy": true,
             "pagingtype": "full_numbers",
             "ordering": false,

             ajax: {
                 type: "GET",
                 url: "/MCQ/Question_Datatable",
                 contentType: "application/json",

                 data: function (d) {
                     d.SUBJECTID = subjectId;
                     alert(JSON.stringify(d));

                     return d;
                 },

                 error: function (xhr, err) {
                     alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                     alert("responseText: " + xhr.responseText);
                 },
             },

             columns: [

                  { "data": "Subject_Id", "autowidth": true },
                  { "data": "Question_Id", "autowidth": true },
                  { "data": "Questions", "autowidth": true },

                {
                    "data": null,
                    "render": function (data, type, row) {
                        return '<input type="checkbox" class="question-checkbox" value="' + row.Question_Id + '" />';
                    },
                    "orderable": false
                }

             ]

         });
     }

     // choose the subjects in dropdown list 

     //loadQuestionsTable();

     $('#choose_subject').on('change', function () {
         var subjectId = $(this).val();
         loadQuestionsTable(subjectId);
     });



     $.ajax({
         url: '/MCQ/Choose_Subjects',
         type: 'GET',
         dataType: 'json',
         success: function (data) {
             var $dropdown = $('#choose_subject');
             $dropdown.empty();
             $dropdown.append('<option value="">Select a subject</option>');
             $.each(data, function (index, subject) {
                 $dropdown.append('<option value="' + subject.Value + '">' + subject.Text + '</option>');
             });
         },
         error: function (xhr, status, error) {
             console.error('Error fetching subjects:', xhr);
         }
     });

     // test mapping

     $('#submit-questions').on('click', function () {
         var selectedQuestions = [];

         $('.question-checkbox:checked').each(function () {
             selectedQuestions.push($(this).val());
         });

         
             $.ajax({
                 url: '/MCQ/Test_maping',
                 type: "POST",
                 data: JSON.stringify({
                     Question_Id: selectedQuestions
                 }),
                 contentType: 'application/json; charset=utf-8',
                 success: function (response) {
                     if (response === "success") {
                         alert('Questions saved successfully');
                     } else {
                         alert('An error occurred while saving.');
                     }
                 },
                 error: function (xhr, status, error) {
                     alert('Error: ' + xhr.responseText);
                 }
             });
        
     });
 });


// show_testmapping_data 

 $(document).ready(function () {

     $("#Test_Mapping").DataTable({

         "destroy": true,
         "pagingtype": "full_numbers",
         "ordering": false,

         ajax: {
             type: "GET",
             url: "/MCQ/Show_Testmappingdata",
             contentType: "application/json",

             data: function (d) {

                 alert(JSON.stringify(d));
                 return JSON.stringify(d);
             },

             error: function (xhr, err) {
                 alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                 alert("responseText: " + xhr.responseText);
             },
         },

         columns: [
              { "data": "Test_Id", "autowidth": true },
              { "data": "Question_Id", "autowidth": true },
              {
                  "data": "Created_Date",
                  "render": function (data) {
                      return moment(data).format("DD/MM/YYYY HH:mm:ss");
                  }, "autowidth": true
              },
               { "data": "Created_By", "autowidth": true },
         ]

     });
 });


//test_id dropdown
 $(document).ready(function () {
     $.ajax({
         url: '/MCQ/Select_Test',
         type: 'GET',
         dataType: 'json',
         success: function (data) {
             //alert(JSON.stringify(data))
             var $dropdown = $('#select_test');
             $dropdown.empty();
             $dropdown.append('<option value="">Select a test</option>');
             $.each(data, function (index, test) {
                 $dropdown.append('<option value="' + test.Value + '">' + test.Text + '</option>');
             });
         },
         error: function (xhr, status, error) {
             console.error('Error fetching subjects: ' + error);
         }
     });

     $('#select_test').change(function () {
         var SelectTestid = $(this).val();
         $.ajax({
             url: '/MCQ/Store_TestId',
             type: 'POST',
             data: { TEST_ID: SelectTestid },
             success: function (response) {
                 //alert(response); 
             },
             error: function () {
                 alert("Error saving TEST ID.");
             }
         });
     });


 });


// user

 function get_testid(test_id) {

     alert(test_id);
     window.location.href = "/MCQ/Invaite_User/" + test_id;

 }

