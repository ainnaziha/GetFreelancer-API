# [.NET Core 6 Web API CRUD Application](https://gf-app-zeta.vercel.app/)

This repository contains a .NET Core 6 Web API CRUD (Create, Read, Update, Delete) application for managing freelancers. The API provides endpoints to perform the following actions:

1. **Get List of Freelancers**: Retrieve a list of freelancers.
2. **Update Freelancer**: Update information about an existing freelancer.
3. **Add New Freelancer**: Create a new freelancer entry.
4. **Delete Freelancer**: Remove a freelancer from the database.

## Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [Authentication](#authentication)
  - [Endpoints](#endpoints)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

### Prerequisites

Before you can run this application, ensure you have the following prerequisites installed on your system:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Git](https://git-scm.com/)

## Applying Migrations and Updating the Database

To apply the Entity Framework Core migrations and update the database, follow these steps:

1. Open the Package Manager Console in Visual Studio.

2. Run the `update-database` command to apply any pending migrations and update the database.
   

The API should now be running locally on [http://localhost:7027](http://localhost:7027).

## Usage

### Authentication

To access the API endpoints, you need to include an `x-api-key` header in your requests for authorization.

### Endpoints

#### Get List of Freelancers
- **Endpoint**: `GET /api/freelancer`
- **Description**: Retrieve a list of all freelancers.

#### Update Freelancer
- **Endpoint**: `PUT /api/freelancer/{id}`
- **Description**: Update information about an existing freelancer.

#### Add New Freelancer
- **Endpoint**: `POST /api/freelancer`
- **Description**: Create a new freelancer entry.

#### Delete Freelancer
- **Endpoint**: `DELETE /api/freelancer/{id}`
- **Description**: Remove a freelancer from the database.
