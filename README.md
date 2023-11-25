
Football World Cup Score Board Project

ntroduction
This document details the Football World Cup Score Board project, designed to track and display scores for football matches. The project consists of a core library (ScoreBoardLib), two console applications (static and dynamic versions), and a test suite (ScoreBoardLib.Tests).

Project Structure
ScoreBoardLib
A library managing football match data and implementing core functionalities of the score board. Key components include:

ScoreBoardService: Main service for managing football matches.
Match: A model representing a football match.
IScoreBoard: Interface for scoreboard operations.
IMatchRepository: Interface for match data management.
ISortingStrategy: Interface for defining sorting strategies for match summaries.
ScoreBoardLib.Tests
A unit testing project that follows Test-Driven Development (TDD) principles. It ensures the reliability and quality of ScoreBoardLib through comprehensive test coverage.

Console Applications
Static Console Application
A simple, static console application providing basic functionalities to interact with the score board. It demonstrates the core capabilities of ScoreBoardLib in a straightforward, menu-driven manner.

Features:

Start a game
Update scores
Finish a game
Display the score summary
Dynamic Console Application
An advanced, dynamic console application that incorporates design patterns for a more flexible and extensible user interface. It allows real-time interaction and can be easily adapted or extended.

Features:

Enhanced user input validation and error handling
Real-time updates to the scoreboard
Extensible design for future enhancements
Design Patterns
Key design patterns used in this project:

Repository Pattern: Abstracts data layer in ScoreBoardLib.
Strategy Pattern: Defines sorting strategies in ScoreBoardLib.
Factory Pattern: Used in the dynamic console application for creating ScoreBoardService instances.
Builder Pattern (Test Data Builder): Creates complex test objects in ScoreBoardLib.Tests.
Dependency Injection: Employed in ScoreBoardService for injecting dependencies.

Usage
Building the Program
To build the console applications and the library, navigate to the root directory of the project and run:

dotnet build
This command compiles the project and its dependencies.

Running the Console Applications
To run a console application, navigate to the respective project directory and execute:

dotnet run
Follow the menu-driven interface to interact with the score board.

Testing
To run the tests in ScoreBoardLib.Tests, navigate to the test project directory and execute:

dotnet test
This command will run all the unit tests in the project, outputting the results to the console.