# SolutionRepo
Software Engineering Design Patterns Final Assessment

## Requirement
This assessment is designed to gauge your ability to solve a problem and explain your solution, not necessarily your ability to code in any specific syntax. 
You will provide a short demonstration of your planned approach to the problem. 
You may add as much code, pseudocode, or notes to your project as you wish to aid in explaining your solution, 
but you do not need to code anything -- you only need to explain your planned solution. 
Be as detailed as possible in your explanation; show where and what changes you will make, 
preferably with names and properties picked out already, and indicate why you are making these changes.

For this assessment, you will not need to make any changes to data access or the database. If, for example, you update a Model, I will not expect any new Migrations to be made.

Project Brief

Now that your Final Project is up to the established spec, your employer has asked you to implement new changes. Your plan should maximize the ease of maintenance and extension for the project.

Those requirements are as follows:

1. New Ticket Types

Right now, your application tracks the "Type" of a ticket with a data table related to the Ticket in question. It has been decided that this is not sufficient. All Tickets will share the same properties as those described in the original spec,
but each ticket will now belong to a special type, which will have its own special data.

The first two types are Bug Reports (which must track Error Codes and Error Logs, both strings), and Service Requests, which have their own subset of Types of Requests (an Enum). We can assume that more special types of tickets will be added later.

2. Calculating Service-Level Agreement Deadlines 

Each ticket now needs to track a Service Level Agreement, which works like a set deadline. This consists of two properties, a response deadline (when the ticket needs to be assigned) 
and a breach deadline (when the ticket should be resolved by). Each deadline will be formatted in an amount of hours, stored in an integer.

Your project manager expects that you will need to use a Design Pattern to implement this change.

These properties will need to be calculated separately by the system whenever a new ticket is created. Each different type of ticket will apply a base calculation with the Priority, 
and multiplied by a different amount depending on the ticket type. 
For example, a High Priority ticket might have a base Response Deadline of 1 hour, and for a Bug Report, the multiplier is 2: the Response Deadline is calculated, then, to 2 hours.

Both the Breach Deadline and Response Deadlines will be modified by a number of different factors that can be added to the ticket.
 If, for example, the "White Glove Client" Modifier is added to the ticket, it will multiple the available hours by 0.8.
 If the "Backlog Reissue" modifier is added to the ticket, the Breach Deadline has 100 hours added to it.
 We should be able to add any number of modifiers to a ticket when it is created.



You will have three hours to prepare a solution brief and then present it to me. Provide a link to your solution repository, and the name of your new solution branch, as submission.
