﻿using Should;
using TechTalk.SpecFlow;

namespace Sample.Acceptance.Steps.UI
{
    [Binding]
    public class CommonSteps : SampleStepsContext
    {
        [Then(@"I should see the message '(.*)'")]
        public void Then_I_Should_See_The_Message(string message)
        {
            Driver.ContainsText(message).ShouldBeTrue();
        }
    }
}
