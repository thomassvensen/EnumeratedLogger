# EnumeratedLogger

Add-on on top of Log4Net that structures logging via enums. For an introduction, read my blog at [open.bekk.no/better-logging-using-enumeration](http://open.bekk.no/better-logging-using-enumeration).
Available via [Nuget](https://www.nuget.org/packages/EnumeratedLogger).

## Release notes

### 1.1.1
 * This fixes the duplicated logging of the enum name, logging the enum integer id was the desired behaviour. (Thanks you @estien !)

### 1.1.0
 * Added ability to specify log class via type parameter (...Log&lt;T&gt;(msg)).
 * Added [LogToConsole] attribute to easily get log output when running tests.
 * Fixed issue with wrong order of ':' and enum name in output.
 * Added a test case to (manually...) verify correct output.

### 1.0.2
 * Improved build process


### 1.0.1
 * First proper version on NuGet
