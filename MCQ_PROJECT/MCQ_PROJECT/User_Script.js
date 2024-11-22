
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


// store the test_id in session



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

        ],

    });

});

