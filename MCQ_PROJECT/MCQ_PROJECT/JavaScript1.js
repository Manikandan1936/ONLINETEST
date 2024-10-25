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


        ]

    });
});


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
                },
                "autowidth": true
            },

               {
                   "data": "End_Date",
                   "render": function (data) {
                       return moment(data).format("DD/MM/YYYY HH:mm:ss");
                   },
                   "autowidth": true
               },

                 {
                     "data": "Duration",
                     "render": function (data) {

                         return data.Hours + ":" + data.Minutes ;
                     },


                     "autowidth": true
                 }



         ]

     });
 });





 // show the questions in datatable

 $(document).ready(function () {

     $("#Questions_Table").DataTable({

         "destroy": true,
         "pagingtype": "full_numbers",
         "ordering": false,

         ajax: {
             type: "GET",
             url: "/MCQ/Question_Datatable",
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
              { "data": "Question_Id", "autowidth": true },
              { "data": "Questions", "autowidth": true },

            {
                "data": null, // Checkbox column
                "render": function (data, type, row) {
                    return '<input type="checkbox" class="question-checkbox" value="' + row.Question_Id + '" />';
                },
                "orderable": false // Disable sorting on checkbox column
            }

         ]

     });

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