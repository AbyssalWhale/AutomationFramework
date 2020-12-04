# Automation Framework

1. Set up visual studio from Microsoft official site;
2. Open solution in VS;
3. Set configure file (TestConfigurator -> RunSettings ->runstanceSettings.runsettings) for tests in Test -> Test Settings (Visual Studio);
4. Select the 'Test Explorer' on the lef side in VS (Test -> Windows -> Test Explorer);
5. Right click on the test and select 'Run selected tests';
6. Wait until test is done with execution;
7. Test logs (shoots, CSV report) are generated in the root test project folder. For example for RegressionUiTests path is: ...\AutomationFramework\RegressionUiTests\TestsData\TestsReports\TestName;

Tools behind:
- C#, .NetCore;
- NUnit;
- Selenium;
- Dapper;
- Bogus;
- Serilog;
- RestSharp.



