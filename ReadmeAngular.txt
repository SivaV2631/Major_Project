Serverside (response is in JSON/XML)
	API (Application Programming Interface)
	Open API (HTTP REST API calls) - GET, POST, PUT, DELETE
	Open API Version 2.0 Documentation (Swagger)


Clientside (HTTP/HTTPS request)
	HTML/CSS/JavaScript
	JavaScript Frameworks: React, Angular, Vue.JS, jQueryUI, Ext.Js, ...
	Native Technologies: Blazor, Console, Web App, Mobile App (Java/Object C)
	API Client / Web App


AJAX: Asynchronous JavaScript and XML (XmlHttpRequest)
JSON: JavaScript Object Notation
Multipurpose Internet Mail Extensions (MIME type)
REST: Representation State Transfer
Further Learning:
	CORS: Cross-Origin Resource Sharing (www.keycdn.com/support/cors) 
	JWT: JavaScript Web Token (identity authorization rules)


Request / Response (JSON)
Content Negotation 		content-type="application/json"
				content-type="application/xml"
<html>																	content-type="text/html"
	<head>
		<link rel="stylesheet" href="styles.css" type="text/css" />		content-type="text/css"
		<script type="text/javascript" src="demo.js"></script>			content-type="text/javascript"
	</head>
	<body>
		<img src="logo.jpg" />											content-type="image/jpeg"

======================================================================	


PREPARE THE PROJECT TEMPLATE

01	Create the "ASP.NET CORE with Angular" Project
	This creates the ASP.NET Web API Server Application
	along with the built-in Angular SPA (Single Page Application) in the \ClientApp folder.

02	Build and Run the application to ensure that everything is working fine.



BUILD THE SERVERSIDE API 

01. Add the following Nuget Packages:
		Microsoft.EntityFrameworkCore.SqlServer						Framework Version (3.x)
		Microsoft.EntityFrameworkCore.Tools							Framework Version (3.x)
		Microsoft.AspNetCore.Diagnostics.EntityFramework			Framework Version (3.x)
		Microsoft.VisualStudio.Web.CodeGeneration.Design			Framework Version (3.x)
		Swashbuckle.AspNetCore										Latest Version
		Microsoft.TypeScript.MSBuild								Latest Version

02.	Add the Model "Category"

03. Add the DbContext and register the Model in the DbContext

04. Define the Database Connection String in the appsetting.json

05.	Register the EF Core services to use SQL Server in the Startup.cs 

06. Build the Project (and generate the migrations to scaffold the database, if needed)

07. Add the API Controller to expose the Categories Data.

08. Build, and Run the Application to check if the API returns the Data,

09. Register the Swagger Documentation generation for OpenAPI documentation in Startup.cs file
	Add the Assembly Attribute to ensure that the Swagger/Swashbuckle generates the complete API Documentation

10. Build, and Run the Application to check if Swashbuckle generates the API documentation
    https://localhost:xxxx/swagger



BUILD THE CLIENT-SIDE Angular Application to perform CRUD operations on Categories

01.	Create a Folder named "category" for the Angular Component in the folder "src\app".
    Please note use camelCase for the folder name (since JavaScript uses CamelCasing convention)

02. Add the TypeScript Angular Component File "category.component.ts"

03. Define the Type of the data from the API as a Angular Interface in the "category.interface.ts" file.
    Expose the data in the component as a property.
	And use the exposed data in the UI component.

04. Add the HTML UI Layout for the Angular Component "category.component.html"
    Register the HTML UI Layout in the component.

05. Add the Custom CSS for the Angular Component "category.component.css"
    Register the CSS in the styleUrls defined in the component.

06. In the Angular Application Module file "app.module.ts":
	(a) Register the Angular Component
	(b) Register the Route

07. Finally, add the menu option in the Nav-Menu Component file "nav-menu\nav-menu.component.html" file

08. Build and Run the application.
