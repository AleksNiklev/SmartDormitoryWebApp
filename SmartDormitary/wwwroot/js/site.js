// Write your JavaScript code.
//"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
connection.on("notify",
    function(name, value, measurementType) {
        $.notify.addStyle("alertNotify",
            {
                html:
                    "<div>" +
                        "<div class='alert alert-danger alert-dismissible' role='alert'>" +
                        "<span data-notify-text/>" +
                        "</div>" +
                        "</div>"
            });
        $.notify(`Sensor ${name} has reached critical value of ${value}${measurementType}`,
            {
                style: "alertNotify"
            });
    });

connection.on("sensorUpdateData",
    function (sensorId, measurementType, sensorValue, sensorTypeMinVal, sensorTypeMaxVal, sensorMinVal, sensorMaxVal) {
        if ($('#' + sensorId)[0]) {
            console.log($('#' + sensorId))
            $.get("/Sensor/GetSensorById",
                { id: sensorId },
                function (response, status) {

                    let staticZones;
                    if (measurementType === "(true/false)") {

                        staticZones = [
                            { strokeStyle: "#F03E3E", min: 0.5, max: 1.0 }, // Red
                            { strokeStyle: "#30B32D", min: 0.0, max: 0.5 } // Green
                        ];
                    } else {
                        staticZones = [
                            { strokeStyle: "#F03E3E", min: 0, max: sensorMinVal * 1 }, // Red
                            {
                                strokeStyle: "#30B32D",
                                min: sensorMinVal * 1,
                                max: sensorMaxVal * 1
                            }, // Green
                            {
                                strokeStyle: "#F03E3E",
                                min: sensorMaxVal * 1,
                                max: (sensorTypeMaxVal * 1) + (20 * (sensorTypeMaxVal * 1)) / 100
                            } // Red
                        ];
                    }

                    let opts = {
                        angle: -0.15, // The span of the gauge arc
                        lineWidth: 0.3, // The line thickness
                        radiusScale: 1, // Relative radius
                        pointer: {
                            length: 0.63, // // Relative to gauge radius
                            strokeWidth: 0.064, // The thickness
                            color: "#000000" // Fill color
                        },
                        staticLabels: {
                            font: "13px sans-serif", // Specifies font
                            labels: [0, sensorTypeMinVal, sensorTypeMaxVal], // Print labels at these values
                            color: "#000000", // Optional: Label text color
                            fractionDigits: 0 // Optional: Numerical precision. 0=round off.
                        },
                        renderTicks: {
                            divisions: 5,
                            divWidth: 1.1,
                            divLength: 0.7,
                            divColor: "#333333",
                            subDivisions: 3,
                            subLength: 0.5,
                            subWidth: 0.6,
                            subColor: "#666666"
                        },
                        limitMax: true, // If false, max value increases automatically if value > maxValue
                        limitMin: true, // If true, the min value of the gauge will be fixed
                        colorStart: "#6FADCF", // Colors
                        colorStop: "#8FC0DA", // just experiment with them
                        strokeColor: "#E0E0E0", // to see which ones work best for you
                        generateGradient: true,
                        highDpiSupport: true, // High resolution support
                        staticZones: staticZones
                    };

                    let target = document.getElementById(sensorId); // your canvas element
                    let gauge = new Gauge(target).setOptions(opts); // create sexy gauge!

                    if (measurementType === "(true/false)") {

                        gauge.colorStart = "#E0E0E0";
                        gauge.minValue = 0.0;
                        gauge.maxValue = 1.0;
                        gauge.animationSpeed = 1; // set animation speed (32 is default value)
                        gauge.set(sensorValue);
                    } else {
                        gauge.colorStart = "#E0E0E0";
                        gauge.minValue = 0;
                        gauge.maxValue = (sensorTypeMaxVal * 1) + (20 * (sensorTypeMaxVal * 1)) / 100;
                        gauge.animationSpeed = 1; // set animation speed (32 is default value)
                        gauge.set(sensorValue * 1);
                    }

                    gauge.set(response.value);

                    let alpha = Math.round(((Date.now() - Date.parse(response.timeStamp)) / 1000) >= 30);
                    if (alpha === 1) {
                        $("#liveStatus").removeClass();
                        $("#liveStatus").addClass("ml-1 text-danger fa fa-times");
                        $("#liveStatus").attr("data-original-title", "No");
                    } else {
                        $("#liveStatus").removeClass();
                        $("#liveStatus").addClass("ml-1 text-success fa fa-check");
                        $("#liveStatus").attr("data-original-title", "Yes");
                    }

                    let delta = Math.round((+new Date - Date.parse(response.timeStamp)) / 1000);

                    let minute = 60,
                        hour = minute * 60,
                        day = hour * 24,
                        week = day * 7;

                    let lastUpdated = "Never";

                    if (delta < 5) {
                        lastUpdated = "just now";
                    } else if (delta < minute) {
                        lastUpdated = delta + " seconds ago";
                    } else if (delta < 2 * minute) {
                        lastUpdated = "a minute ago";
                    } else if (delta < hour) {
                        lastUpdated = Math.floor(delta / minute) + " minutes ago";
                    } else if (Math.floor(delta / hour) === 1) {
                        lastUpdated = "1 hour ago";
                    } else if (delta < day) {
                        lastUpdated = Math.floor(delta / hour) + " hours ago";
                    } else if (delta < day * 2) {
                        lastUpdated = "yesterday";
                    }

                    $("#timeStampField").text(lastUpdated);
                    $("#valueField").text(response.value);
                    $("#chart-text-value").text(response.value);
                });
        }
    });

connection.start().catch(function(err) {
    return console.error(err.toString());
});