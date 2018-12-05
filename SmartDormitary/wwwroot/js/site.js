// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
connection.on("notify", function (name, value, measurementType) {
    $.notify("Sensor" + name + " has value of " + value + " " + measurementType + ".", "danger");
})

connection.start().catch(function (err) {
    return console.error(err.toString());
});
