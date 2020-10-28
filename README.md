# Automation Framework

1. Set up visual studio from Microsoft official site;
2. Open solution in VS;
3. Set configure file (RegressionUiTests -> RunSettings ->nstanceSettings.runsettings) for tests in Test -> Test Settings;
4. Select the 'Test Explorer' on the lef side in VS;
5. Right click on the test and select 'Run selected tests;
6. Wait until test is done with execution;
7. Test logs (shoots, CSV report) are generated in following path: ...\AutomationFramework\RegressionUiTests\TestsData\TestsReports\CheckArticleCoordinates;
8. To run test on another browser. Open file RegressionUiTests -> RunSettings ->nstanceSettings.runsettings. Change value of property Browser from/to firefox/chrome. Save file by CTRL + S;
9. Repeat Step 5.
