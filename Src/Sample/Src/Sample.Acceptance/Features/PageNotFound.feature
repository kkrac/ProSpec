Feature: Page not found
	In order to continue my navigation in the site
	As a user
	I want to be informed that a URL does not exist

Scenario: go to unexisting page
	When I go to an unexisting page
	Then the http status should be '404'
	And I should see the message 'The resource cannot be found'