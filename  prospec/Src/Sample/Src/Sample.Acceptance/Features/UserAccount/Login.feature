Feature: login
	In order to use features that require to be authenticated
	As an anonymous user
	I want to login
	
Background:
	Given the following users exist
	| UserName | Password   | FirstName | LastName | Email        | DateOfBirth |
	| john.doe | MyPassword | John      | Doe      | john@doe.com | 1/12/1970   |
	And I am on the log in page

Scenario: log in with existing user name and valid password
	When I submit the following login information
	| Field    | Value      |
	| UserName | john.doe   |
	| Password | MyPassword |
	Then I should be redirected to the home page
	
Scenario: log in with unexisting user name
	When I submit the following login information
	| Field    | Value              |
	| UserName | unexistingUserName |
	| Password | anyPassword        |
	Then I should continue on the login page
	And I should see the message 'The user name or the password are invalid'

Scenario: log in with existing user name but invalid password
	When I submit the following login information
	| Field    | Value           |
	| UserName | john.doe        |
	| Password | invalidPassword |
	Then I should continue on the login page
	And I should see the message 'The user name or the password are invalid'
	
Scenario: log in with empty user name
	When I submit the following login information
	| Field    | Value       |
	| UserName |             |
	| Password | anyPassword |
	Then I should continue on the login page
	And I should see the message 'The user name is required'
	
Scenario: log in with existing user name but empty password
	When I submit the following login information
	| Field    | Value    |
	| UserName | john.doe |
	| Password |          |
	Then I should continue on the login page
	And I should see the message 'The password is required'