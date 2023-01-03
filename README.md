# Solution overview:
Solution consists of 3 projects. All of them use .NET 6.0 framework.    
- AutomationCore - main project with managers, utils etc that are required by objects from the 'TestsConfigurator' project;
- TestsConfigurator - project with models (POMs, APIs etc), fixtures, and reusettings. It has reference to the 'AutomationCore' project only;
- RegressionTests - project with UI and API regression tests. It has reference to the 'TestsConfigurator' project only;

Tools under the hood:
- .net;
- NUnit;
- Selenium;
- Dapper;
- Bogus;
- Serilog;
- RestSharp;
- JUnitTestLogger;

# Solution' projects overview:
- AutomationCore - 
- TestsConfigurator -
- RegressionTests - 

