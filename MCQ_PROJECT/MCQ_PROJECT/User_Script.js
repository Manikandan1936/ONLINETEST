
// invait user

$(document).ready(function () {
    $('#btn_user').click(function () {
        var emails = $('#u_email').val().split(',');
        alert(emails)

        $.ajax({
            url: '/MCQ/Save_Email',
            type: 'POST',
            data: JSON.stringify({ Email: emails }),
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
             { "data": "User_Email", "autowidth": true },
                 
        ]

    });
});
