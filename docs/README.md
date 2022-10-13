Application is built with .NET 6.0 with Clean Architecture and SOLID principles.

Application is ready to run in DEBUG as well as RELEASE build.

There are unit tests and integration tests for different scenarios.

Integration test output will be a csv file in the bin folder with the given appconfig file. 

Unit test scenarios could be more but considering time limitation, I haven't completed all.

Tech stack:

.NET 6.0, Serilog for logging with multiple sinks, Polly for the retry mechanism, XUnit, XUnit.DependencyInjection, Moq, FluentAssertions.
