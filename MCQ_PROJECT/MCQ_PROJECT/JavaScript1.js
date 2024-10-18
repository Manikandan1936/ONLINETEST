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
                if (response) {
                    // Login successful
                    //alert('Login successful!');
                    window.location.href = '/MCQ/Subject_Page';
                } else {
                    // Invalid credentials
                    alert('Invalid username or password!');
                }
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