# Documentation

1. Web application of Farm management
2. DFS of the matrix

## Web application

Create a web application for farm management that consists of one page. It will be used only by one
farmer. He/she can list, add, and remove animals in the system. Every animal has only a “Name”
property that should be unique.
The frontend should be written on Angular. The backend is on ASP.NET Core. Use in-memory storage on
the server side.
The source code should be production ready.

Project contains the following namespaces and organization modules:

- [Backend based on .NET Core WebAPI](./src/farmManagement/backend)
Uses service architecture principles for easy testing and team work.

- [Backend Web](./src/farmManagement/backend/FarmManagement.API)
.NET Core Web API application.

- [Backend Dal](./src/farmManagement/backend/FarmManagement.Dal)
Common library for Data Access Layer.
Includes exception hierary, general repository and entities which are specific for implementation of access layer.

- [Backend Dal InMemory](./src/farmManagement/backend/FarmManagement.Dal.InMemory)
Certain library for Data Access Layer implemented via InMemory.
Uses LazyCache (https://github.com/alastairtree/LazyCache) and certain implementation of repositories.

- [Backend Models](./src/farmManagement/backend/FarmManagement.Models)
Common library for Models.
Includes DTO, request/response objects.

- [Backend Services](./src/farmManagement/backend/FarmManagement.Services)
Includes implementation of service layouts for the farm, models mappings profiles.

- [Backend unit tests](./src/farmManagement/backend/tests)
Uses Autofixture and NUnit.

- [Frontend based on Angular](./src/farmManagement/frontend/FarmManagement.Web)
 Uses service architecture principles for easy testing and team work.

- [Frontend unit tests](./src/farmManagement/frontend/FarmManagement.Web/src/app/pages/animals/tests)
Uses Karma and Jasmine.

- [Scripts](./src/farmManagement/backend)
The folder contains docker, circleci, git and other scripts.


### How to run

1. Under the folder (./src/farmManagement/frontend/FarmManagement.Web) perform `npm install`
2. Run WebAPI project

## DFS of the matrix (Deep First Search)

Create a [Application](./src/dfsSample) that accepts a matrix of values 0 and 1. The application should
output only one value into the console – number of areas formed of number 1.
The matrix is presented as a string value where ‘,’ is used as a separator for columns, ‘;’ is used as
a separator for rows. For instance, “1,0,1;0,1,0” string value should be converted to the matrix
[[1,0,1], [0,1,0]].

![DFS overview](./assets/dfs.svg)