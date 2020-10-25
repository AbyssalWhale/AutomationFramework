# Automation Framework

1. Set up visual studio from Microsoft official site;
2. Open solution in VS;
3. Set configure file (RegressionUiTests -> RunSettings ->nstanceSettings.runsettings) for tests in Test -> Test Settings;
4. Select the 'Test Explorer' on the lef side in VS;
5. Right click on the 'CheckArticleCoordinates' test and select 'Run selected tests;
6. Wait until test is done with execution;
7. Test logs (shoots, CSV report) are generated in following path: ...\AutomationFramework\RegressionUiTests\TestsData\TestsReports\CheckArticleCoordinates;
8. To run test on another browser. Open file RegressionUiTests -> RunSettings ->nstanceSettings.runsettings. Change value of property Browser from/to firefox/chrome. Save file by CTRL + S;
9. Repeat Step 5.

# Test Scenario

1. Open https://www.google.com/ and verify page and the the page is loaded;
2. Go to https://www.wikipedia.org/ and verify page and the the page is loaded;
3. Find and open the 'Giga Berlin' article page on wiki. Verify page and the the page is loaded;
4. Get coordinats of Giga Berlin;
5. Open new tab in browser and go to: https://www.google.com/maps. Verify page and the the page is loaded;
6. Find Giga Berlin with coordinats. Check address, plus codes, and header picture. 
