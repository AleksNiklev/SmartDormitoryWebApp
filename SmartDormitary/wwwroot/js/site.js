// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
connection.on("notify",
    function(name, value, measurementType) {
        $.notify.addStyle('alertNotify',
            {
                html:
                    "<div>" +
                        "<div class='alert alert-danger alert-dismissible' role='alert'>" +
                            "<span data-notify-text/>" +
                        "</div>" +
                    "</div>"
            });
        $.notify("Sensor " + name + " has reached critical value of " + value + "" + measurementType,
            {
                style: 'alertNotify'
            });
    });

connection.start().catch(function (err) {
    return console.error(err.toString());
});
