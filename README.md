# My First C# .Net API (Work in Progress, mostly for learning purposes)
[![.NET](https://github.com/tylerkibble/MyApi/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tylerkibble/MyApi/actions/workflows/dotnet.yml)
## Description

MyApi is a web service that provides a set of endpoints for clients to interact with. It is developed using .NET framework and C#.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- You have installed the latest version of [.NET](https://dotnet.microsoft.com/download)
- You have a Windows machine. This code may work on Mac OS or Linux machine, but it was developed and tested on Windows.

## Installation

Clone this repository to your local machine and navigate to the project directory:

`git clone https://github.com/tylerkibble/MyApi` 
<br/>

`cd MyApi`


## Usage

To run MyApi, follow these steps:

`dotnet run`

## Swagger Docs

http://localhost:5186/swagger/index.html

## API Endpoints

- `GET /weatherforecast`: Get a weather forecast.
- `POST /users`: Create a new user.
- `GET /users/{id}`: Get a user by id.
- `PUT /users/{id}`: Update a user.
- `DELETE /users/{id}`: Delete a user.
- `POST /register`: Register a new user.
- `POST /login`: Log in a user.
- `POST /upload`: Handle file upload.
- `GET /products`: Get products with pagination and filtering.
- `GET /products/{id}`: Get a product by id, return 404 if not found.

## Contributing

1. Fork the project.
2. Create a new branch (`git checkout -b feature`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add a new feature'`).
5. Push to the branch (`git push origin feature`).
6. Open a pull request.

## License

This project uses the following license: [MIT](https://opensource.org/licenses/MIT).