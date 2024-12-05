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

## Async Programming
- Traditional Synchronous programming - program execution is blocked
- Poor performance (Synchronous programming)
- Async/await keywords
- More requests
- asynchronous programming allows the program to continue executing other tasks while waiting for the long running operation to complete, resulting in a much better performance and responsiveness
- return type for async is Task<IActionResult>
- ToList() == ToListAsync()
- Asynchronous comes from Microsoft.EntityFrameworkCore;
- FirstOrDefault = FirstOrDefaultAsync
- Add() = AddAsync()
- use await if you see dbcontext
- SaveChanges() = SaveChangesAsync()
- Remove() method doesn't have a asyncronous method, so it is still synchronous method

## Repository Pattern
- Design pattern to separate the data access layer from the application
- provides interface without exposing implementation
- Helps create abstraction
- Repository pattern typically involves creating an abstraction layer between the application and the data store, which is implemented by a concrete repository class.
- The repository class is responsible for performing CRUD operations that is create, read, update and delete on the data store and it exposes a set of methods that the application can use to interact with the data .
- It is the controller that is talking to the database using that dbcontext. That is also a wrong practice and using repository design pattern, we can eliminate that by adding an abstraction layer in between
- repositories can be added in between the controller and the database so that all the operations on the database is then handled by the repository
- The DbContext class is injected inside the repository rather than inside the controller, and it is the repository that then injects in the controller. So the controller will use the repository instead of using the dbcontext.
- By doing that, the controller now has no awareness of what's being called through the DbContext, whether it's a SQL server database or a MongoDb database, it has no idea about it.
- Controller is just using the interface method exposed by the interface repository and the implementation is hidden behind the implementation repository. Using that, you can switch the logic and the data stores behind the implementation repository. For example, you can use entity framework core to store your changes in a database, or you can just use an in-memory database by creating another implementation repository. All of those changes are happening behind the repository and the controller has no knowledge about the data stores at all.
  ### Benefits
  - Decoupling
  - Consistency
  - Performance
  - Multiple data sources(switching)
  ![Screen](https://github.com/user-attachments/assets/36c3b435-0ece-42a2-9e88-82a42b1b1b80)
 ### Few Important points related to Repository pattern
 - if we want to create repository for the region, a region domain model which we will use to just interact with the regions table, we would need a region repository and after that, after we have created the interface which exposes the CRUD operations, that is, create, read, delete and update.
 - we would then need and implementation, a concrete implementation that would actually implement all these methods for us.
 - Now the implementation can be named as a SqlRegionRepository.
 - Guid.newGuid() is a method that gives a new guid address.
 - ager hamare paas, many database hai to hamara repository interface apne methods ki difination kis database se uthayega, ye decide hota hai program.cs file me, jahan ham log btate hai ki kis interface ki implementation kis file se uthana hai, yah inject karne wale place pe hota hai.
 - Task<Region> --> means can not be a null value --> type should be same in interface and also at where you implemented it
 - Task<Region?> --> means can be a null value.  --> type should be same in interface and also at where you implemented it

