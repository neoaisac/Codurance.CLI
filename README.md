Isaac Llopis (C) 2015
isaac.llopis.aracil@gmail.com

Codurance.CLI Social Networking Exercise
INSTRUCTIONS
------------

($ is the root path of the solution)

Executable file:
$/Codurance.CLI/bin/Debug/Codurance.CLI.exe

The application runs in Command Line mode in Windows using
the .Net Framework 4.5.

When the application shows the "greater than" symbol, it is
loaded and accepting commands.

COMMANDS
--------
> quit
Closes the application

> [user name] -> [message]
Posts a message in the wall of the user.
If the user does not exist it is created.

> [user name]
Shows the list of messages posted by the user.
Messages are ordered from oldest to newest.

> [user name 1] follows [user name 2]
Adds the second user to the list of followed users from
the first one.
A user cannot follow itself, or a non existing user.

> [user name] wall
Shows the list of messages posted by the user or the
followed users.
Messages are ordered from oldest to newest.

ERRORS
------
When an error occurs a message is prompted to the user.
If your Command Line client allows for it, it will show in
red characters. When an error occurs the application does
not necessarily have to crash. It will continue accepting
commands.

ABOUT THE SOURCE CODE
---------------------

The provided solution is a Visual Studio (C#) Console
Application, produced with Visual Studio 2015 Community
Edition and running on .Net Framework 4.5.

The solution is provided with source code and contains
the following projects.

- $/Codurance.CLI : Main project that defines the CLI exe-
cutable.

- $/Codurance.Business.Model : Definition of entities and
contracts used accross the application.

- $/Codurance.Business.Commands : Definition of the execu-
table commands.

- $/Codurance.Repository.SQLCE : Implementation of DB sto-
rage layer with Entity Framework in SQLCE. The database is
self-destructive. The "quit" command ensures that the data-
base is destroyed after use. Note that the DB is recreated
in each use. The user running the application must have
writing rights in the path of the application for it to be
created.

- $/Codurance.Shared : Definition of generic con-
tracts, patterns and practices, and extension methods to
be used across a wide range of elements.

- $/Codurance.Tests : Unit tests for some business
objects. The tests are created with NUnit and use Moq for
mocking. Tests focus in the core of the application, the
Commands in the Codurance.Business.Commands project.

- $/packages : NuGet packages used by the solution.

- $/Codurance.CLI.sln : Visual Studio 2015 Solution File.
Open this file in Visual Studio 2015 to load the full set
of projects.

- $/README.txt : This explanatory file.

The application is developed using:
- .Net Framework 4.5
- Unity for Dependency Injection
- NUnit and Moq for Unit testing
- Entity Framework and SQLCE for database storage
- Chain of command pattern for the command handling
- SOLID principles and loose coupling
- Wrappers with interfaces when .Net Static classes
  are needed

THANKS
------

Thank you for the opportunity. Happy coding!

