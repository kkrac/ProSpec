* What is "ProSpec"?

ProSpec is a light library used to make your ATDD/BDD tests with SpecFlow + WatiN easier. It provides a set of classes to abstract your code from the implementation of your UI tests.
Even though it is centered on web applications with WatiN, it could be extended so as to use other browser interface such as Selenium, or even to test other types of applications (ie: Winforms).


* What does the package contain?

ProSpec comes with the framework itself, and a small Sample application.
You can find the the ProSpec.sln under \ProSpec and the Sample.sln under \ProSpec\Src\Sample if you want to open the application in isolation.
Otherwise, you can check the the Sample projects in the same ProSpec solution.


* What to bear in mind before starting to play with the Sample app?

The Sample app uses the ASP.NET built-in Dev server to run your tests.
Before you run the tests, you need to specify the correct path where the Dev server is installed in your machine and where you have the Sample app.
You can do so by modiyfing the following file:

\ProSpec\Src\Sample\Src\Sample.Configuration\Acceptance\ui.config

There, look for the tag <serverPath> and update it accordingly.
And modify the <physicalPath> to specify where the Sample App resides in your machine too.


* Ok, I did this. Now, how do I run the tests?

Go to the folder \ProSpec\Src\Sample\Build and look for the file Run.Browser.Tests.bat.

Run it. And check for the report generate under \ProSpec\Src\Sample\Reports\Acceptance\UI\Browser.


* Is that it?
That's it. Now start using ProSpec to write your own tests. Simple. Easy. Fast.