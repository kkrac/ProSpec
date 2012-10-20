Feature: Confirm account
	In order to log in
	As an unconfirmed user
	I want to confirm my account

Background:
	Given the following temporary accounts exist
	| UserName | Token    |
	| john.doe | 12345678 |

Scenario: confirm account with valid user name and valid token
	When I confirm an account with the following information
	| UserName | Token    |
	| john.doe | 12345678 |
	Then the account should become active
	And I should be redirected to the login page

Scenario: confirm account with valid user name and invalid token
	When I confirm an account with the following information
	| UserName | Token |
	| john.doe | 345   |
	Then I should continue on the account confirmation page
	And I should see the message 'The user name or the token are invalid'

Scenario: confirm account with invalid user name and valid token
	When I confirm an account with the following information
	| UserName  | Token |
	| john.doe2 | 123   |
	Then I should continue on the account confirmation page
	And I should see the message 'The user name or the token are invalid'

Scenario: confirm account with invalid user name and invalid token
	When I confirm an account with the following information
	| UserName  | Token |
	| john.doe2 | 345   |
	Then I should continue on the account confirmation page
	And I should see the message 'The user name or the token are invalid'