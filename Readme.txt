* What is "ProSpec"?

ProSpec is a light library used to make your ATDD/BDD tests with SpecFlow + WatiN easier. It provides a set of classes to abstract your code from the implementation of your UI tests.
Even though it is centered on web applications with WatiN, it could be extended so as to use other browser interface such as Selenium, or even to test other types of applications (ie: Winforms).


* What does the package contain?

ProSpec comes with the framework itself, and a small Sample application.
You can find the the ProSpec.sln under \ProSpec and the Sample.sln under {...}\Src\Sample if you want to open the application in isolation.
Otherwise, you can see the Sample projects in the same ProSpec solution.

* Pre-requisites to run tests in Sample App:

1.- Install SQL Server Express edition
2.- Specify where ASP.NET built-in Dev server is installed. To do so, go to {...}\Src\Sample\Src\Sample.Configuration\Acceptance\ui.config

and modify the following node accordingly:

<serverPath>C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0\WebDev.WebServer20.EXE</serverPath>

3.- Go to {...}\Src\Sample\Setup\Database and execute RestoreDb.bat. That will restore the database that comes with the example


* Ok, I did this. Now, how do I run the tests?

Go to the folder {...}\Src\Sample\Build and look for the file Run.Browser.Tests.bat.

Run it. And check for the report generate under {...}\Src\Sample\Reports\Acceptance\UI\Browser.