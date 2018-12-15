# SmartDormitoryWebApp
> Our system can collect data from all over the world.

  ![alt text](https://raw.githubusercontent.com/AleksNiklev/SmartDormitoryWebApp/develop/SmartDormitary/wwwroot/images/LandingPage.png)
<hr/>

## Description

SmartDormitoryWebApp is application that collects data from various sensors located in college dormitories all over the world. The collected data can be analyzed by independent research organizations.

Participants should have installed specific sensor equipment supplied by the organization and register it to SmartDormitoryWebApp.
The following sensor types should be supported:
  -	Temperature, measured in °C
  -	Humidity sensor, measured in percent
  -	Electric power consumption sensor, measured in Watts 
  -	Window /door sensor. Allowed values: true/false(True – when the window/door is open; False – when the window door is closed)
  -	Noise sensor, measured in Decibels
  
<hr/>

## Features
SmartDormitoryWebApp has some of the following functionalities:
  - Login/Register functionalities 
  - Dynamic graphic representation of a sensor
  - Register/Update Sensor Feature
  - Sensor alarm - logged user gets notification when one or few sensors values are not in the acceptable range
  - Cool Admin Panel
  ![alt text](https://raw.githubusercontent.com/AleksNiklev/SmartDormitoryWebApp/develop/SmartDormitary/wwwroot/images/AdminPanel.png)


## Technologies

The General technologies used in the developing the SmartDormitoryApp are these:
  - ASP.NET Core 
  - Razor for all of the apps pages
  - SignalR for real time notifications 
  - AJAX for dynamic representation of the sensors
  - Bootstrap 4
  - Microsoft SQL Server Manager for data storage and Entity 
  - 87% Unit test code coverage of the business logic
  Framework Code First approach for data access. Here is how the database looks like:
  ![alt text](https://raw.githubusercontent.com/AleksNiklev/SmartDormitoryWebApp/develop/SmartDormitary/wwwroot/images/Database.png)



