# Documentation

## Routing
- Routing is used to map the url of a request to a controller and then its action method

## DbContext Class
- Maintaining Connection to Db
- Track Changes
- Perform CRUD Operations
- Bridge between domain models and the database
 
## DbSet
- A DbSet is a property of DbContext class that represents a collection of entities or domain models in the database

## Dependency Injection
- Design pattern to increase maintainability and testability of applications by reducing the coupling between components
- DI built into ASP.NET Core == it means it is a part of ASP.NET CORE
- DI container is responsible for creating and managing instances
- At its Core, DI works on this fundamental that instead of instantiating objects within a class, those objects are passed in as parameters to the class, like passing it to the constructor or the method instead.
- ASP.NET Core provides a built in container that can be used to manage the dependencies of an application. The DI container is responsible for creating and managing instances of services which are registered with the container when the application starts

## Finding and object in the database using id
- you can use Find() method but as an argument, you can only pass id.
- instead of using Find() method, you can use Linq query and here instead of passing, you can pass name or any other property as well but for that you need to pass that property instead of id in Action method arguments. The query is dbContext.tableName.FirstOrDefault(x => x.Id == id)

## DTOs (Data Transfer Objects)
- Used to transfer data between different layers
- Typically contain a subset of the properties in the domain model
- For example transferring data over a network
  ### Advantages of DTOs
  - Seperation of Concerns
  - Performance
  - Security
  - Versioning
    
   ![Screenshot (5)](https://github.com/user-attachments/assets/1146a394-a06c-4604-9213-52068314d963)
