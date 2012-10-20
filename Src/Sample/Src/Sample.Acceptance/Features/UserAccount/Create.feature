Feature: Create account
	In order to log in
	As an anonymous user
	I want to create an account

Scenario: navigate to sign up page
	Given I am on the log in page
	When I go to the sign up page
	Then I should be redirected to the sign up page

Scenario: create account with valid data
	Given I am on the sign up page
	When I submit the following user account information
	|	Field					|	Value		|
	|	UserName				|	john.doe	|
	|	FirstName				|	John		|
	|	LastName				|	Doe			|
	|	DateOfBirth				|	1-13-1970	|
	|	Email					|	john@doe.com|
	|	Password				|	MyPassword	|
	|	PasswordConfirmation	|	MyPassword	|
	|	AcceptsTerms			|	true		|
	Then I should be redirected to the temporary account page