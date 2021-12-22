# Idea-bank-for-AAportland-P3AAU
This repository is a webserver developed as a P3 project at Aalborg University. This system is made for Aalborg Portland, which is a factory producing cement in Denmark. They needed an ideabank where employees can submit ideas for projects, like improving the production. Project leaders will act as administrators in the system and can review these ideas.

## Startup guide 
Before the web application is started a connection to a database should be created. In the file "appsettings.json" in "IdeaBank" the connection string for a database can be specified. The default connection string is for a local database so if you wish to run the web application locally on your computer you do not need to change this. The tables for the database can be created by running the command: "updata-database" in the Package Manager Console. After this, the web application can be started and data for some of the tables will automatically be created. This data is a requirement for the web application to work and for a successful run of the tests in "Testing". If the Business Units and Departments need to be changed it is best to delete all data in "BusinessUnitsTbl" and "DepartmentsTbl" then change the field variables in "BusinessLogicLib.Service.Config.cs". 

## Directory guide 
In this repository we use a layered architecture where we have four different layers.

* Ideabank, which is considered as the UI layer.
* BusinessLogicLib, where all logic is handled. 
* RepositoryLib, where all database requests is handled.
* DataBaseLib, where we have our database.

The Ideabank layer contains Bootstrap 5 from [getbootstrap.com](https://getbootstrap.com/)

All the mentioned code except for the ones with a link to the source of the code, is developed and written by CS-21-SW-3-17 at Aalborg University.

## Technical content in repository
We use Blazor Server which is a web framework designed to run server-side in ASP.NET Core and an Entitity Framework SQL database.
