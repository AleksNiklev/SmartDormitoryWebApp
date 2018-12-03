// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
connection.on("notify", function () {
    console.log("test");
    $.notify("Hello World", "danger");
})

connection.start().catch(function (err) {
    return console.error(err.toString());
});
