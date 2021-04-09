# Calculator service

Calculator Service is capable of some basic arithmetic operations, like add, subtract, square, etc. along with a history
service keeping track of requests sharing a common identifier.

## Installation

Use the Docker file to install to API. This image is to Windows.

***You need to have the Docker Client installed on your machine.***

From the main directory (which where is the Docker file), enter in a command editor (like Powershell) the following commands:

```docker
dotnet publish -c Release
docker build -t calculator_service -f DockerFile .
docker run -p 8081:80 calculator_service
```
The API is running in http://localhost:8081/calculator

## Packages

- AutoMapper v8.1.1
- NLog v1.7.2
- Swashbuckle.AspNetCore
- AutoFixture v4.16.0
- Moq v4.16.1
- xunit v.2.4.1
- xunit.runner.visualstudio v2.4.3


## Usage

Set the CalculatorService.Client as default on the project, run the application and the console application is open with instructions to use the calculator.

## License
[MIT](https://choosealicense.com/licenses/mit/)