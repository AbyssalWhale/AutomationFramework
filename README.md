# .Net Selenium Tests
This repository only serves as a demonstration software for demostration of following skills:
- automation testing with using .Net with Selenium;
- DevOps with using AWS, GitHub Action, Microsoft Azure DevOps.
- test are written against solution: https://github.com/AbyssalWhale/game-store

# Technologies Used

The following technologies and tools are used in this repository:
- C# + .Net;
- Coverlet Collector;
- Bogus;
- Dapper; 
- DotNetSeleniumExtras PageObjects;
- JunitXml;
- Newtonsoft Json;
- NUnit;
- Docker.

# DevOps
There are two pipelines configured in the repository, which are automatically triggered whenever a new pull request (PR) is opened. These pipelines serve as 'Checks' to ensure the quality and functionality of the code. The pipelines are configured in two different places:
- GitHub Actions: This pipeline runs all stages automatically, as they are public and can be reviewed by anyone;
- Microsoft Azure DevOps: This pipeline also runs automatically, but only for the build and publish artifact stages. The remaining stages require some manual activity to be completed. This configuration is done to optimize cost savings.

The main jobs performed by the pipelines are as follows:
- Check out and run tests;
- publish the test results to the corresponding pull request;
- publishing test results to Jira ZephyrScale is applicable for Microsoft Azure DevOps. GitHub Actions is coming;

#Conclusion
I would like to emphasize the following points:
- Ongoing Development: Everything in this repository, including the Game Store, are currently in development. I actively switch between projects to continuously enhance my skills in different technologies. It's important to note that this is not the final or most refined version of the software and tests;
- Skill Demonstration: The main objective of this repository is to showcase my skills and capabilities. While the software and tests may not be the absolute best or most comprehensive versions, they serve as tangible examples of my proficiency in the respective technologies. They demonstrate my ability to work on real-world projects and provide insight into my development approach.

By highlighting these points, it is clear that the repository serves as a demonstration of skills rather than representing finalized or production-ready software.
