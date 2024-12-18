﻿
// invait user

//window.get_testid = function (test_id) {
//    testId = test_id;
//    alert("Test ID: " + test_id); 
//    window.location.href = "/MCQ/Invite_User/" + test_id;
//};

$(document).ready(function () {

    $('#btn_user').on('click', function (e) {
        e.preventDefault();

        var emails = $('#u_email').val().split(',');

        var test_id = sessionStorage.getItem("Test_Id");

        alert(test_id);

        $.ajax({
            url: '/MCQ/Save_Email',
            type: 'POST',
            data: JSON.stringify({ Test_Id: test_id, Email: emails }),
            contentType: 'application/json; charset=utf-8',
            success: function (response) {

                alert('Emails saved successfully!');
            },
            error: function (error) {
                alert('Error saving emails.');
            }
        });
    });
});

// show user email


$(document).ready(function () {

    $("#Invaite_Table").DataTable({

        "destroy": true,
        "pagingtype": "full_numbers",
        "ordering": false,

        ajax: {
            type: "GET",
            url: "/MCQ/Show_Useremail",
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
             { "data": "Invaite_Id", "autowidth": true },
              { "data": "Test_Id", "autowidth": true },
             { "data": "User_Email", "autowidth": true },
                 
        ]

    });
});


//user registration

$(document).ready(function () {
    $('#u_btn').on('click', function (e) {
        e.preventDefault();

        var user_register = {

            Name: $('#u-name').val(),
            Email_Id: $('#u_email').val(),
            Mobile_Number: $('#u_number').val(),
            gender: $('#u_gender').val(),
            Registration_Date: $('#u_date').val()
        }

        $.ajax({

            type: "POST",
            url: "/USER/User_Registrations",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ User_Register: user_register }),
            success: function (response) {

                alert("User Successfully  Register")
            },

            error: function () {
                alert('An error occurred. Please try again.');
            },

        });

    });
});

// show user data

$(document).ready(function () {
   
    $("#User_Table").DataTable({

        "destroy": true,
        "pagingtype": "full_numbers",
        "ordering": false,

        ajax: {
            type: "GET",
            url: "/USER/Show_Userdata",
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
             { "data": "User_Id", "autowidth": true },
              { "data": "Name", "autowidth": true },
             { "data": "Email_Id", "autowidth": true },
             { "data": "Mobile_Number", "autowidth": true },
              { "data": "gender", "autowidth": true },
             {
                 "data": "Registration_Date",
                 "render": function (data) {
                     return moment(data).format("DD/MM/YYYY HH:mm:ss");
                 }, "autowidth": true
             },
        ]

    });
});



// after login

$(document).ready(function () {

    $("#Email_Table").DataTable({

        "destroy": true,
        "pagingtype": "full_numbers",
        "ordering": false,

        ajax: {
            type: "GET",
            url: "/USER/Login_Data",
            contentType: "application/json",
            dataSrc: "",
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            },
        },

        columns: [
             { "data": "Test_Id", "autowidth": true },
           {
               "data": "Created_Date",
               "render": function (data) {
                   return moment(data).format("DD/MM/YYYY HH:mm:ss");
               }, "autowidth": true
           },

            {
                "data": "Start_Date",
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
       data: "Status",
       render: function (data) {
           if (data === true) {
               return '<span class="badge bg-success">Test Attended</span>';
           } else {
               return '<span class="badge bg-danger">Test Not Attended</span>';
           }
       },
       autoWidth: true,
   },


           {
               "data": "Percentage",
               "render": function (data) {
                   return data.toFixed(0) + " %";
               },
               "autoWidth": true
           },

                {
                    mRender: function (data, type, row) {

                        if (row.Status === true) {
                            return '<button class=\'btn btn-secondary\' disabled>Already Attended</button>';
                        }
                        else {

                            return '<a onclick="Test_Id(' + row.Test_Id + ', \'' + row.Duration.Hours + ':' + row.Duration.Minutes + '\')" class="btn btn-success">TAKE TEST</a>';
                        }
                    }
                },

        ],

    });

});



// get the testid and duration in session storage

function Test_Id(test_id,duration) {

    alert("Test ID: " + test_id + "\nDuration: " + duration);

    sessionStorage.setItem("Test_Id", test_id)
    sessionStorage.setItem("Test_Duration", duration);

    window.location.href = "/USER/Instruction_Page?Test_Id=" + test_id;
}


// show question and options 


$(document).ready(function () {
    var questions = [];
    var currentIndex = 0;
    var testId = sessionStorage.getItem('Test_Id');
    var duration = sessionStorage.getItem('Test_Duration');
    var totalTime = 0;
    var userResponses = {};

    var timeParts = duration.split(':').map(Number);
    var hours = timeParts[0];
    var minutes = timeParts[1];
    var totalTime = (hours * 60 + minutes) * 60;

    var remainingTime = sessionStorage.getItem('remainingTime')
       ? parseInt(sessionStorage.getItem('remainingTime'), 10)
       : totalTime;

    function formatTime(seconds) {
        var minutes = Math.floor(seconds / 60);
        var sec = seconds % 60;
        return minutes.toString().padStart(2, '0') + ':' + sec.toString().padStart(2, '0');
    }

    function updateTimer() {
        if (remainingTime <= 0) {

            clearInterval(timerInterval); // Stop the timer
            alert('Time is up! The test has been completed.');
            $('#nextbutton, #backbutton').prop('disabled', true); // Disable buttons
            sessionStorage.removeItem('remainingTime');


        } else {
            remainingTime--;
            sessionStorage.setItem('remainingTime', remainingTime);
            $('#timer').text('Time Remaining: ' + formatTime(remainingTime));
        }
    }


    function fetchQuestions() {

        $.ajax({
            url: '/USER/Show_QuestionOptions',
            type: 'GET',
            data: { Test_Id: testId },
            dataType: 'json',
            success: function (result) {
                questions = result;

                if (questions.length > 0) {
                    displayQuestion(questions[currentIndex]);
                    $('#nextbutton').show();


                    if (!sessionStorage.getItem('timerStarted')) {
                        timerInterval = setInterval(updateTimer, 1000);
                        sessionStorage.setItem('timerStarted', true);

                    }
                } else {
                    alert('No questions available.');
                }


            },
            error: function () {
                alert('Failed to load questions.');
            }
        });
    }

    function displayQuestion(question) {

        var templateSource = $('#Question_Template').html();
        var template = Handlebars.compile(templateSource);

        question.index = currentIndex + 1;

        var questionHtml = template(question);
        $('#questionContainer').html(questionHtml);

        $('#backbutton').toggle(currentIndex > 0);
        $('#nextbutton').toggle(currentIndex < questions.length - 1);
        $('#submit_questions').toggle(currentIndex === questions.length - 1);

    }

    $('#nextbutton').click(function () {
        var selectedOption = $('input[name="options"]:checked');
        var selectedOptionId = selectedOption.val();
        var currentQuestionId = questions[currentIndex].Question_Id;


        if (selectedOptionId) {
            $.ajax({
                url: '/USER/Dictionary_Values',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    Test_Id: testId,
                    Question_Id: currentQuestionId,
                    Option_Id: selectedOptionId
                }),
                success: function (response) {
                },
                error: function () {
                    alert("Failed to save response.");
                }
            });
        }

        if (currentIndex < questions.length - 1) {
            currentIndex++;
            displayQuestion(questions[currentIndex]);
        }

    });

    $('#submit_questions').click(function () {

        var selectedOption = $('input[name="options"]:checked');
        var selectedOptionId = selectedOption.val();
        var currentQuestionId = questions[currentIndex].Question_Id;

        $.ajax({
            url: '/USER/Save_Answer',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Test_Id: testId,
                Question_Id: currentQuestionId,
                Option_Id: selectedOptionId
            }),
            success: function (response) {
                alert("Answers submitted successfully!");

                $.ajax({
                    url: '/USER/UpdateTestStatus',
                    type: 'POST',
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    data: { testId: testId },
                    success: function (statusResponse) {
                        if (statusResponse.success) {
                            alert(statusResponse.message);
                        } else {
                            alert("Failed to update test status.");
                        }
                    },
                    error: function () {
                        alert("Error while updating test status.");
                    }
                })

            },
            error: function () {
                alert("Failed to submit answers.");
            }
        });
    });


    $('#backbutton').click(function () {
        if (currentIndex > 0) {
            currentIndex--;
            displayQuestion(questions[currentIndex]);
        }
    });

    fetchQuestions();

    var timerInterval = setInterval(updateTimer, 1000);
});


// view results

$(document).ready(function () {
   
    $("#View_Test_Table").DataTable({

        "destroy": true,
        "pagingtype": "full_numbers",
        "ordering": false,

        ajax: {
            type: "GET",
            url: "/USER/View_Marks",
            contentType: "application/json",

            dataSrc: "data",

            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            },
        },

        columns :[

            { "data": "Name" , "autowidth" : true},
            { "data": "Email_Id", "autowidth": true },
            { "data": "Test_Name", "autowidth" : true },
            { "data": "Total_Answer" , "autowidth" : true },
            { "data": "Correct_Answer", "autowidth": true },
             {
                 "data": "Percentage",
                 "autoWidth": true,
                 "render": function (data, type, row) {
                     return data ? data.toFixed(0) + "%" : "0%";
                 }
             }
        ]
    })
})