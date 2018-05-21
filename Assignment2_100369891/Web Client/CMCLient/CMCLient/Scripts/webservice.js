// This function send the data into createEventsTable to display the table with the informations inside
function getAllEvents() {
    $.ajax({
        url: 'http://localhost:1148/Calendars',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            createEventsTable(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}


function LogIn() {

    var User = {
        Email: $('email').val
    };

    $.ajax({
        url: 'http://localhost:1148/Users' + User.Email,
        type: 'GET',
        data: JSON.stringify(User.Email),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert("Success!")
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}

//This function takes all the information from a specific day
function getAllEventsByDay() {

    var Event = {
        Date: $('day').val
    };

    $.ajax({
        url: 'http://localhost:1148/Calendars',
        type: 'GET',
        data: JSON.stringify(Event.Date),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert("succes");
            $.each(data, function (index, data){

                if (Event.Date == data.Date)
                {
                    alert(Event.Date);
                }    
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}

// This function is changing the values of the information
function editEventValue(eventId) {

    var success = "Your event have been changed! Check it in your events table"
    var Event = {
        Id: eventId,
        Title: $('#eventtitle').val(),
        Date: $('#eventday').val(),
        Location: $('#eventtime').val(),
        Description: $('#eventdesc').val()
    };

    $.ajax({
        url: 'http://localhost:1148/Calendars/' + eventId,
        type: 'PUT',
        data: JSON.stringify(Event),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#neweventform").html(success);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}

//This function is used to add a new event in the calendar
function addEvent() {

    var success = "Your event have been added!"
    var Event = {
        Title: $('#eventtitle').val(),
        Date: $('#eventday').val(),
        Location: $('#eventtime').val(),
        Description: $('#eventdesc').val()
    };

    $.ajax({
        url: 'http://localhost:1148/Calendars',
        type: 'POST',
        data: JSON.stringify(Event),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#newevent").html(success);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}

// This function is used to register
function AddUser() {

    var success = "Register successfuly!"
    var User = {
        Name: $('#username').val(),
        Email: $('#email').val(),
        Password: $('#password').val()
    };

    if (User.Email == $('#cemail').val() && User.Password == $('#cpassword').val() && User.Email != null && User.Password != null) {

        $.ajax({
            url: 'http://localhost:1148/Users',
            type: 'POST',
            data: JSON.stringify(User),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#newuser").html(success);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
            }
        });

    }
    else {
        alert("Your Email or Password do not match!");
    }
}

// This function display a table with all the events
function createEventsTable(event) {
    var strResult = '<div class="col-md-12">' +
        '<table class="table table-bordered table-hover">' +
        '<thead>' +
        '<tr>' +
        '<th>Title</th>' +
        '<th>Date</th>' +
        '<th>location</th>' +
        '<th>Description</th>' +
        '<th>&nbsp;</th>' +
        '<th>&nbsp;</th>' +
        '</tr>' +
        '</thead>' +
        '<tbody>';
    $.each(event, function (index, event) {
        strResult += "<tr><td>" + event.Title + "</td><td> " + event.Date + "</td><td>" + event.Location + "</td><td>" + event.Description + "</td><td>";
        strResult += '<input type="button" value="Edit Event" class="btn btn-sm btn-primary" onclick="editEvent(' + event.Id + ');" />';
        strResult += '</td><td>';
        strResult += '<input type="button" value="Delete Event" class="btn btn-sm btn-primary" onclick="deleteEvent(' + event.Id + ');" />';
        strResult += "</td></tr>";
    });
    strResult += "</tbody></table>";
    $("#neweventform").html(strResult);
}

//This function create a table with all the events on a specific day
function byDayTable(event) {
    var strResult = '<div class="col-md-12">' +
        '<table class="table table-bordered table-hover">' +
        '<thead>' +
        '<tr>' +
        '<th>Title Event</th>' +
        '<th>Location</th>' +
        '<th>Description</th>' +
        '<th>&nbsp;</th>' +
        '<th>&nbsp;</th>' +
        '</tr>' +
        '</thead>' +
        '<tbody>';
    $.each(event, function (index, event) {
        strResult += "<tr><td>" + event.Title + "</td><td>" + event.Location + "</td><td>" + event.Description + "</td><td>";
        strResult += '<input type="button" value="Edit Event" class="btn btn-sm btn-primary" onclick="editEvent(' + event.Id + ');" />';
        strResult += '</td><td>';
        strResult += '<input type="button" value="Delete Event" class="btn btn-sm btn-primary" onclick="deleteEvent(' + event.Id + ');" />';
        strResult += "</td></tr>";
    });
    strResult += "</tbody></table>";
    $("#bydaytable").html(strResult);
}

// This function delete the selected event          <----CHANGE---->
function deleteEvent(id) {
    $.ajax({
        url: 'http://localhost:1148/Calendars/' + id,
        type: 'DELETE',
        dataType: 'json',
        success: function (data) {
            getAllEvents();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}

// This function gives the selected event to edit
function editEvent(eventID) {
    $.ajax({
        url: 'http://localhost:1148/Calendars/' + eventID,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            createEditEventForm(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR + '\n' + textStatus + '\n' + errorThrown);
        }
    });
}

// This is a simple Cancel function
function Cancel() {
    $("#neweventform").html("");
}

// This function is used to add an event form with the previous details from the event prepared to change
function createEditEventForm(event) {
    var strResult = '<div class="col-md-12">';
    strResult += '<form class="form-horizontal" role="form">';
    strResult += '<div class="form-group"><label for="eventtitle" class="col-md-3 control-label">Event Title</label><div class="col-md-9"><input type="text" class="form-control" id="eventtitle" value="' + event.Title + '" ></div></div>';
    strResult += '<div class="form-group"><label for="eventday" class="col-md-3 control-label">Date</label><div class="col-md-9"><input type="text" class="form-control" id="eventday" value="' + event.Date + '" ></div></div>';
    strResult += '<div class="form-group"><label for="eventtime" class="col-md-3 control-label">Location</label><div class="col-md-9"><input type="text" class="form-control" id="eventtime"  value="' + event.Location + '" ></div></div>';
    strResult += '<div class="form-group"><label for="eventdesc" class="col-md-3 control-label">Description</label><div class="col-md-9"><input type="text" class="form-control" id="eventdesc" value="' + event.Description + '" ></div></div>';
    strResult += '<div class="form-group"><div class="col-md-offset-3 col-md-9"><input type="button" value="Edit Event" class="btn btn-sm btn-primary" onclick="editEventValue(' + event.Id + ');" />&nbsp;&nbsp;<input type="button" value="Cancel" class="btn btn-sm btn-primary" onclick="Cancel();" /></div></div>';
    strResult += '</form></div>';
    $("#neweventform").html(strResult);

}