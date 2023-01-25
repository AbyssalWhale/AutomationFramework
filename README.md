# Solution overview:
Solution consists of 3 projects. All of them use .NET 6.0 framework.    
- AutomationCore - main project with managers, utils etc that are required by objects from the 'TestsConfigurator' project;
- TestsConfigurator - project with models (POMs, APIs etc), fixtures, and reusettings. It has reference to the 'AutomationCore' project only;
- RegressionTests - project with UI and API regression tests. It has reference to the 'TestsConfigurator' project only;
- Playwright things are coming in...;

Tools under the hood:
- .net;
- NUnit;
- Selenium;
- Dapper;
- Bogus;
- Serilog;
- RestSharp;
- JUnitTestLogger;
- playwright - is in progress.

# Solution' projects overview:
- AutomationCore:
  - AssertAndErrorMsgs - assert and exception messages that should be used by exceptions and asserts;
  - Enums - data that can be reused by tests, POMs, and APIs during test execution. For example: drop downs options, menu option etc;
  - Managers - tool's wrappers that are used by POMs and APIs during test execution. If tool does not need a wrapper - it's initialised directly in test fixture/POM/API. 
  - Utils - additional tools that can be used at any level (POMs, API routes, tests etc);
- TestsConfigurator:
  - Fixtures - has all fixtures for all tests. Notice that any fixture should be inherited from the 'TestsSuitsFixture' class;
  - Models - contains all POMs and API models including API routes; 
  - RunSettings - run settings for specific branch/instance;
  - Projects - has reference to the 'AutomationCore' project;
- RegressionTests: 
  - API - all API regression tests. Tests sorting: Endpoint name -> request type;
  - UI -  all UI regression tests. Tests sorting: Page name.
  - TestsResults - contains tests attachment for each run. Notice: folder is added to .gitignore. 
  - Projects - has reference to the 'TestsConfigurator' project;
- PlaywrightCore:
  - in progress
- RegressionTestsPW:
  - in progress

