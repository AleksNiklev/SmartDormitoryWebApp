// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
connection.on("notify",
    function(name, value, measurementType) {
        $.notify.addStyle('alertNotify',
            {
                html:
                    "<div>" +
                        "<div class='alert alert-info alert-dismissible' role='alert'>" +
                            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" +
                            "<span data-notify-text/>" +
                        "</div>" +
                    "</div>"
            });
        $.notify("Sensor" + name + " has value of " + value + " " + measurementType + ".",
            {
                style: 'alertNotify'
            });
    });

connection.start().catch(function (err) {
    return console.error(err.toString());
});
