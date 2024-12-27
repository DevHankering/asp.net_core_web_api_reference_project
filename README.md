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
 - Difinition(interface) and implemetation(concrete class)
 - Controller me return **NotFound()** likhte hai aur repository ki concrete class me **return null** likhte hai.
 - May be null here ka matlab hai ki koi if condition lagana hai jisse ki if value null ho to manage ho sake for example jab ham db se call krte hai if(a == null) {return NotFound()}

## Automapper
- Auto Mapper is a popular object to object mapping library for dotnet applications, including ASP.NET Core .
- it allows us to simplify the mapping process between two objects with different structures by defining mapping between their properties
- Object to Object mapping
- Simplification
- Map between DTOs and Domain Models and Vice-versa
- Quite powerful apart from just simple object to object mapping
- In ASP.NET Core, Automapper is commonly used to map between domain models and view models or DTOs.
- It can also be easily be integrated into your application using NuGet packages and configured using its fluent API.
- From Nuget package manager, search and download **Automapper**
- Mappings can be created using the method createmap
- CreateMap<TSource, TDestination>();
- CreateMap<TSource, TDestination>().ReverseMap();
- CreateMap<TSource, TDestination>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName)).ReverseMap();
- inside controller --> mapper.Map<DestinationType>(source);
- inside program.cs --> builder.Services.AddAutoMapper(typeof(className));
- after injecting in program.cs, we can use it anywhere in out application, to use just create the constructor and import it as a parameter like this `IMapper mapper`
- since we want to use it in our controller, create a constructor in the file and inside (), write IMapper mapper --> where IMapper becomes datatype and mapper become variable name but before that assign mapper as private field using ctrl+. and selecting field option.

## Data Seeding
- Data seeding refers to the process of populating a database with an initial set of data. This is often done to set up a development or testing environment by inserting predefined data into tables, allowing developers, testers, or administrators to work with sample data. It ensures that the database has all the necessary records to function properly during the application development process.
- During the initial setup of a new project or system, predefined records such as users, categories, or configurations are seeded to ensure that the system operates properly.
  ### How to Do Data Seeding
  1. First, delete the tables from the database because otherwise you might get the error, that the table already exists.
  2. we do data seeding using `onModelCreating` method.
  3. generally, data seeding is done in dbContext file, So go there and write `override onModelCreating` Now you will get suggessions and in one suggession modelbuilder will be written, select that, hece you will get the structor for overriding.
  4. Inside the structure, we will create new list and seed the list into the database.
  5. When creating the list you will need the Id for every list item. For Id or identifier, we can not write `Guid.NewGuid()` which will give us a new guid id , becuase at every migration, `Guid.NewGuid()` will assign a new Guid id to the list item which we don't want.
  6. So to solve this, we will go to **C# Interactive** window, to go there --> View>Other Windows>C# Interactive --> here will type `Guid.NewGuid()` and this will give us a GuidId, So just copy and paste it into your list item Id as `Guid.Parse(guid_id that you got)`
  7. After creating the list, To seed the list is very simple --> `modelBuilder.Entity<List_Data_Type>().HasData(Provide_the_list_here);` --> this will seed the list into the database.
  8. Now we will run the migration, but before running, just close your IDE or application and re open it, then run migration,
  9. That's it, you data seeding is complte.
  10. When posting data, swagger will provide you with a dummy id, so don't get cofused with it, just remove the id and past the id from the database that you seeded in database.

## Navigation Properties in EF Core
- Entity framwork core has a way to get the related table information like region, difficulty information inside the walk table instead of IDs and that is what navigation property.
- Navigation properties are used to fetch related data from the database.
- Navigation properties are typically defined in the form of an object of collection of object that reference another entity or entities
  ### How to Use Navigation property
  - jab ham log data get krte hai using `.ToList()` method, to isse pahle you can write `.Include(Navigation_Property_name_as_a_string)` or `Include(x => x.Navigation_Property_name_as_a_string)` which is type safe. you can do chainning also with `.Include`.
  - .Include is used to get table information instead of foreign key.
  - Now in our DTOs instead of giving field of RegionID and DifficultyID, just write the navigation property, and Now when you will fetch the table, the related tables's information will also be printed, so we don't need IDs for people to see, but if you want you can have them as well.

## Model Validations
- we want to act on those endpoints that are accepting data
- since only Update and Create takes request, we are going to use Model validations on these. So go to your controller and start writting.
- Go to request Dto, if your model property can take null value, it's fine. but if it can not, we will use `[required]` notation, see the examples in the project.
- also you can decide the min and max length value as well in here there.
- `ModelState.IsValid` returns the boolean value --> is out models or dto meets all the conditions, it returns true, otherwise it returns false.
- 400 stands for **Bad Request**
- ModelState is an Object, we return this as a return object, and also this object holds the error message that you provided in Dto.
  ### Using Custom Action Filter For Validation
  - Instead of writting `ModelState.IsValid` again and again, we have a slightly advanced technique which is using Custom Action Filter for validating.
  - First of All, create a new folder --> CustomActionFilter and inside this create an class Named ValidateModelAttribute.
  - Now inherit the class with ActionFilterAttribute.
  - Now inside the class body, write override OnActionExecuting and select the structure for it.
  - Now delete everything that is written inside this structures' body and start writting `if(context.ModelState.IsValid == false){context.Result = new BadRequestResult();}`
  - Now beneath [HttpPost] and [HttpPut], just write [ValidateModel] and it's done. Now you don't need to write if(ModelState.IsValid) every time.

 ## Authentication and Authorization - JWT Tokens
  ### Authentication
  - The process to determine a user's Identity
  - Username and Password
  - By using authentication, we check if we trust the user.
  ### Authorization
  - User permission
  - Roles, Policies, Claims
  - Check if User has ReadOnly or ReadWrite Role
  ### JWT
  - server creates it and pass it to the client
  - Through JWT, we securely transmite information between parties as a JSON object.
  - The users will type in, their username and password using a login form and the website will send this information to the API. The API will check the username and password and if this information is correct, it will generate a JWT token for the website user. The website will then use this JWT token as a way to make HTTP calls to the API to access the resources of the API and get data from the API. The API will check if the token is correct and if it is, the API will return the data that the website asked for. If the website doesn't send a JWT or it i invalid, then no data is returned from the API. This is the authentication flow.
    ![jwt(1)](https://github.com/user-attachments/assets/30638293-8835-4c44-848c-2dc60733b472)
![JWT(2)](https://github.com/user-attachments/assets/bc0568fc-b581-45d0-9fee-cfd395106d49)
### Install Some NuGet Packages For Our Authentication Process
   ![NuGet_package-list](https://github.com/user-attachments/assets/e7315ad4-13b2-4215-9bd2-9fe1a24fc951)
 - after install the NuGet packages, Go to appsettings.json, and after connection string, create a new object, see the project for reference.
 - ### Steps
    1. ### Install 4 Nuget packages
       - Microsoft.AspNetCore.Authentication.JwtBearer
       - Microsoft.IdentityModel.Tokens
       - System.IdentityModel.Tokens.Jwt
       - Microsoft.AspNetCore.Identity.EntityFrameworkCore
    2. ### Configure JWT Settings in appsettings.json
       ```
        "Jwt": {
            "Issuer": "yourdomain.com",
            "Audience": "yourdomain.com",
            "SecretKey": "your-very-secret-key-here"
        }

       ```
       
       - you can find YourDomain here --> means applicationURL --> thirdClickOnProjectName>GoToProperties>General>OpenDebugLaunchProfilesUI>https>AppURL
      4. ### Configure JWT authentication in program.cs file
         - **builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,**   // claims token is issued by me
            **ValidateAudience = true,**  // Ensures that the token is intended for your application (or another specific resource).
            **ValidateLifetime = true,**  // Ensures that the token is still valid and has not expired. If the token is expired, it will be rejected.
            **ValidIssuer = builder.Configuration["Jwt:Issuer"],** //Only tokens from this issuer will be accepted.
            **ValidAudience = builder.Configuration["Jwt:Audience"],**  // This ensures the token is meant for your application (or specific API) and not some other application.
            **IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))**    //  This key is crucial for verifying that the token hasn't been tampered with. If the signature doesn't match, the token is rejected.  // we will give the encoded version of the key.
        **};
    });**
    - **AddAuthentication** method enables authentication for your application.
  
    - **.AddJwtBearer(options => { ... })** -->  It defines how the JWTs will be validated when received in HTTP requests.
    - **Note:** The secret key is usually kept private and should not be exposed or checked into version control. It's often stored securely (e.g., in environment variables or a secrets management system).
    - ![Capture-14](https://github.com/user-attachments/assets/8e34b48b-c00e-4fcb-a2f2-4233c60ee232)
    - ![Capture-13](https://github.com/user-attachments/assets/539e3059-6b95-4abd-a61e-ff4ac78d07bb)
   4. ### Add athentication into middleware pipeline -->  app.UseAuthentication();
      - ![Capture-15](https://github.com/user-attachments/assets/34c4dbe0-8ad9-4f7d-a4cf-a7c3decfeef3)
      - **app.UseAuthentication();**  --> should be above **app.UseAuthorization** --> because authentication happens before authorization
      - Ager controller ke upper **[Authorize]** likh diya jaye to jo bhi http request aayengi is controller ke paas, vo sabhi bina JWT authentication ke data access or post data nhee kar payengi.
      - ![Capture-16](https://github.com/user-attachments/assets/a7d00e6f-6151-4936-bb01-d1265efb4b43)
    5. ### Register and Login [With Roles]
      - Setting Up Auth Database
      - ![Capture-17](https://github.com/user-attachments/assets/51d12895-be0a-4d1e-aa6e-8972796dd4c4)
      - Now Add new **ConnectionString** into appsettings.json file and just change the database name, otherwise it is same as default string.
      - Now Create a new DbContext file into Data folder --> inside the data folder, we only have one DbContext at the moment which deals with the tables like student,address,images,regions, walks, and difficulties. Now we need a DbContext that deals with the authentication tables like users and roles. So now we will create **AuthDbContext**.
      - This AuthDbContext will inherit from **identityDbContext** and this comes from the package **Microsoft.AspNetCore.Identity.EntityFrameworkCore;**
        - *Now we need a constructor inside the AuthDbContext, So just right click on the className and press ctrl + .  and now click on *Generate constructor with Options parameter* .
        - Now we will inject this dbContext into our **program.cs** file. it will be injected just like a normal Dbcontext.
        - **Remember** --> when we have more than one DbContext, we need to specify in constructor in our DbContext file, which DbContext we are going to use. So instead of writting **DbContextOptions** inside our constructor, we are going to use DbContextOptions< TypeOfDbContext >  --> Normally TypeOfDbContext is the name of our DbContext or DbContext class like AuthDbContext.
        - Now inside the **AuthDbContext** , we will create **roles** and we want to **seed** some roles into the database so that when we do a user registration, we can supply these roles and create a user with these roles.
        - We will use **override onModelCreating** method to seed some data into the database.
        - Here we will create a list called roles and the type will be IdentityRole which comes from *Microsoft.AspNetCore.Identity*.
        - Inside IdentityRole, we have 4 properties
        - the first one is Id which should be an string type. --> since we are going to use Guid id --> we will get new Guid from **C#Interactive** after typing **Guid.NewGuid()** --> we will store this into a variable as a string, and now we will use this variable as a value for the Id.
        - at the end, we will seed this list inside the builder object. --> we will provide roles list to **IdentityRole** entity. --> ```builder.Entity<IdentityRole>().HasData(roles);```
         ![Screenshot (18)](https://github.com/user-attachments/assets/397792bb-c83d-4d18-91b1-283e987bd194)
        - `ConcurrencyStamp` : Used for concurrency control, to track and verify that an entity (like a role) has not been changed by another process before updating it.
        - `Name` : The user-friendly name of the role, like `"Reader"` or `"Writer"`.
        - `NormalizedName` : A standardized (usually uppercase) version of the `Name`, used for case-insensitive comparisons.
        - By using both `Name` and `NormalizedName`, the system is able to store both the readable form and an efficient version for comparisons, improving both usability and performance in lookups.
        - ### IdentityRole
          - In ASP.NET Core Identity, an `IdentityRole` is a class that represents a role in a system that uses role-based authorization.
          -  Instead of assigning individual permissions directly to each user, you assign users to roles, and then you define what permissions are associated with those roles.
          -  For example:
              - A Reader role might allow users to only view content.
              - A Writer role might allow users to view and edit content.
     ## Key Components of the `IdentityRole` Class
   - The `IdentityRole` class, which is part of the `Microsoft.AspNetCore.Identity` namespace, typically contains the following properties:
      1. `Id`:
         - Type: `string`
         - Represents a unique identifier for the role.
         - This is often a GUID (Globally Unique Identifier) or a string, depending on how the Identity system is configured.
      2. `Name`:
         - Type: `string`
         - Represents the name of the role (e.g., "Admin", "Reader", "Writer").
         - This is typically a user-friendly string that can be displayed to users or administrators.
      3. `NormalizedName`:
         - Type: `string`
         - Represents the normalized version of the role name (usually uppercase), used for case-insensitive role comparisons.
         - It helps to optimize searches and role lookups to ensure consistent comparisons across case variations. For example, "admin" and "ADMIN" would be treated as the same role because of the normalized name.
      4. `ConcurrencyStamp`:
         - Type: `string`
         - A unique stamp used for concurrency control, ensuring that the role hasn’t been modified by another process when you're making changes to it. This is used for optimistic concurrency checking, which helps prevent overwriting changes when multiple users or processes are working with the same role.
    ## How is `IdentityRole` Used?
   In a typical application using ASP.NET Core Identity:
   1. **Role Creation**: You define and create roles for your application, such as "Admin", "User", "Reader", or "Editor", depending on the needs of your application. This is done by creating instances of the `IdentityRole` class and adding them to the `RoleManager<IdentityRole>`.
   2. **Role Assignment**: You assign users to these roles using the `UserManager<TUser>` and `RoleManager<IdentityRole>` services, typically by calling `AddToRoleAsync` on the user manager. Once a user is assigned a role, they inherit the permissions associated with that role.
   3. **Role-based Authorization**: Once roles are assigned to users, you can use role-based authorization to restrict access to parts of your application. For example, you can use attributes like `[Authorize(Roles = "Admin")]` to specify that only users in the "Admin" role can access certain pages or perform certain actions.
      ## Example: Creating and Using `IdentityRole` in ASP.NET Core
      - ### **1.Creating Roles Programmatically**
         ```
         public class ApplicationDbContextSeed
         {
            public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
           {
             // Check if the "Admin" role exists, if not, create it.
             if (!await roleManager.RoleExistsAsync("Admin"))
             {
             await roleManager.CreateAsync(new IdentityRole("Admin"));
             }

             // Check if the "User" role exists, if not, create it.
             if (!await roleManager.RoleExistsAsync("User"))
              {
              await roleManager.CreateAsync(new IdentityRole("User"));
               }
             }
          }
           ```
         - ### **2. Assigning a Role to a User**
           ```
                public async Task AssignRoleToUser(UserManager<ApplicationUser> userManager, string userId, string roleName)
           {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
              {
                await userManager.AddToRoleAsync(user, roleName);
               }
            }
           ```
           - ### **3. Role-based Authorization**
             - You can use roles to control access to certain parts of your application.
               ```
                      [Authorize(Roles = "Admin")]
                     public IActionResult AdminDashboard()
                 {
                      return View();
                 }
               ```
      ## Summary of `IdentityRole`
      - `IdentityRole` is a class used to represent a role in ASP.NET Core Identity, which is part of the user authentication and authorization system.
      - It is typically used to manage user roles and assign permissions based on those roles.
      - Roles are created and assigned to users, and role-based authorization helps control access to different parts of the application.
 ## Note:
 - If you have more than one dbContext into you asp.net core application, you will this command for migration --> `add-migration "comment" -Context "dbContextName"` and for dababase update, we will write `update-database -context "dbContextName`
 - Inside tables, in `AspNetRole` we have roles that we created. and when users are created, they will end up in the `ASPNetUsers` table
   # Now we will inject identity to our solution
   - we add the indentity in `program.cs` file above the authentication.
     ```
     builder.Services.AddIdentityCore<IdentityUser>()
          .AddRoles<IdentityRole>()
          .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
          .AddEntityFrameworkStores<NZWalksAuthDbContext>()
          .AddDefaultTokenProviders();


     builder.Services.Configure<IdentityOptions>(options =>
     {
          options.Password.RequireDigit = false;
          options.Password.RequireLowercase = false;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireUppercase = false;
          options.Password.RequiredLength = 6;
          options.Password.RequiredUniqueChars = 1;
     });

     ```
     ![Screenshot (27)](https://github.com/user-attachments/assets/7534001a-7f5b-4526-a8ff-0839b6c10110)

     # NEXT: -> Now we will create a new controller which is Auth Controller and we will create the new register action method so that users can request a user to get created inside our identity database.
     - When someone wants to create a new user, they will have to supply us with the username and password information. So we will have to create a requestDto for this register method.
     -  we use userManager class that identity provides us to register a user or to create a user. so we have to inject the user manager class. in our AuthController in Constructor.
     -  Now we will create a this auth controller.
     -  After creating Auth controller --> we will register a user.
     -  Now that we have a user created, the user will log in to our application using the username and password that they have and we will be checking the username and password and seeing if we have the user registered. after that we will create a token for them once they have successfully told us that they are a valid user.
     -  So for that we need a login action method and this will be another post method.
    
    ## Creating the User
   ```
   var identityUser = new IdentityUser
   {
       UserName = registerRequestDto.Username,
       Email = registerRequestDto.Username
   };
   ```
   - `IdentityUser` is the default class that represents a user
  
     ## Creating the User in the Database
     ```
     var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
     ```
     - `userManager.CreateAsync` is called to attempt to create the user in the database with the provided username and password.
     - The result is stored in the `identityResult` variable, which contains information about whether the user creation was successful or not (success/failure, error messages, etc.).
    
       ```
            if (identityResult.Succeeded)
        {
           //Add roles to this User
           if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
        {
            identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
        
             if (identityResult.Succeeded)
           {
            return Ok("User was registered! Please login.");
           }
         }
       }

       ```
       - If the `CreateAsync` operation was successful (`identityResult.Succeeded` is `true`), the code proceeds to check if any roles are provided in the `registerRequestDto.Roles` list.
       - If roles are provided (i.e., `Roles` is not `null` and contains at least one role), the `userManager.AddToRolesAsyn` method is called to assign the specified roles to the newly created user.
              - The method takes the `identityUser` (newly created user) and a list of roles (`registerRequestDto.Roles`) to assign to the user.
              - After attempting to add the roles, the result is again checked. If adding the roles is successful (`identityResult.Succeeded` is `true`), an HTTP 200 OK response is returned with the message: `"User was registered! Please login."`.

         ## Summary of Flow:
          - A new `IdentityUser` is created using the username and email from the `registerRequestDto.`
          - The user is attempted to be created with the specified password.
          - If the user creation is successful, the code checks whether any roles are provided.
          - If roles are provided, the roles are added to the user.
          - If everything is successful, the method returns a success message (`200 OK`).
          - If any step fails (user creation or role assignment), it returns a failure message (`400 Bad Request`).
          - `userManager` is an instance of `UserManager<IdentityUser>`, which is responsible for managing users in ASP.NET Identity (e.g., creating users, assigning roles).
          - `registerRequestDto` is a Data Transfer Object (DTO) that contains properties like `Username`, `Password`, and `Roles`. The `Roles` property is a list of roles the user should be assigned to after creation.
        
    ```
    var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if(user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Create Token

                    return Ok();
                }
            }

            return BadRequest("Username or password incorrect");
    ```

   ```
   var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
   ```
   - This method tries to find a user in the database based on their email (or username in this case).
   - `userManager`: This is an instance of UserManager<TUser>.
   - `UserManager` is used for interacting with user data and handling common user management tasks like creating, deleting, updating users, and checking user credentials.
   - `userManager.CheckPasswordAsync`: This method checks if the provided password (`loginRequestDto.Password`) matches the password for the found `user` object.
     ```
      var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
     ```
     -  If a user was found, this line asynchronously checks whether the provided password matches the hashed password stored in the database for that user.
    

## JWT Creation
- ## 1.Method Signature
  ```
  public string CreateJWTToken(IdentityUser user, List<string> roles)
  
  ```
- ### **Return Type**: `string` — This method returns a JWT as a string.
- ### **Parameters**:
     - `IdentityUser user` — The user for whom the token is being created. It includes properties like `Email`, `UserName`, etc.
     - `List<string> roles` — A list of roles associated with the user, e.g., "Admin", "User", etc.
 
- ## 2.Create Claims
  ```
  var claims = new List<Claim>();

  claims.Add(new Claim(ClaimTypes.Email, user.Email));

  ```
  - **Claims**: Claims are key-value pairs that represent the user's information, which will be embedded in the JWT.
  - `ClaimTypes.Email` is used to add the user's email to the list of claims. `user.Email` is the email address of the authenticated user.
    ```
    foreach(var role in roles)
    {
         claims.Add(new Claim(ClaimTypes.Role, role));
    }

    ```
    - **Roles**: A loop iterates over the list of roles and adds each role to the claims. The `ClaimTypes.Role` indicates that this claim type will represent the user's roles.
   
 - ## 3.Create the Signing Key
   ```
   var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
   ```
   - **SymmetricSecurityKey**:  This is the key used for signing the JWT. It uses a secret key, which is fetched from the application's configuration settings (`configuration["Jwt:Key"]`).
   - `Encoding.UTF8.GetBytes()` is used to convert the string key into a byte array, which is required for the `SymmetricSecurityKey` constructor.
  
- ## 4.Signing Credentials
  ```
  var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
  ```
  - **SigningCredentials**: These define how the JWT will be signed. In this case:
      - The `key` is the signing key created earlier.
      - `SecurityAlgorithms.HmacSha256` specifies the signing algorithm — HMAC with SHA-256, which is a secure hash algorithm for signing the JWT.
   
  - ## 5.Create the JWT Token
    ```
    var token = new JwtSecurityToken(
    configuration["Jwt:Issuer"],
    configuration["Jwt:Audience"],
    claims,
    expires: DateTime.Now.AddMinutes(15),
    signingCredentials: credentials
    );
    
   ```
 - **JwtSecurityToken Constructor Parameters**:
    - **Issuer**: `configuration["Jwt:Issuer"]` — The issuer of the token, typically the URL or name of your server or organization.
    - **Audience**: `configuration["Jwt:Audience"]` — The audience for whom the token is intended. Usually, this would be your API or another service that consumes the JWT.
    - **Claims**: The list of claims we created earlier (user's email and roles).
    - **Expires**: `DateTime.Now.AddMinutes(15)` — The expiration time for the token. This token will expire 15 minutes from the time it is generated.
    - **SigningCredentials**: The credentials used to sign the token, which include the signing algorithm and the key.
  
  - ## 6.Return the JWT as a String
    ```
    return new JwtSecurityTokenHandler().WriteToken(token);
       ```
    - **JwtSecurityTokenHandler**: This is a helper class provided by .NET to handle JWTs. `WriteToken()` takes the `JwtSecurityToken` object and serializes it into a compact, URL-safe JWT string format, which can be sent to the client.
   
      ## Summary
      This method generates a JWT that includes the following:
      - ### **1.Claims**:
          - The user's email.
          - The user's roles (from the `roles` list).
      - ### **2.Security**:
          - The token is signed using a secret key (from the application's configuration) with the HMAC-SHA256 algorithm.
      - ### **3.Token Properties**:
          - The token will expire in 15 minutes.
          - The token has an issuer (`Issuer`) and an audience (`Audience`), both configured from settings.
    - Finally, the JWT is returned as a string, ready to be sent back to the client for authentication.

## **Example Output**
- If this method were called for a user with the email user@example.com and roles ["Admin", "User"], the JWT returned might look like this (in a compact format):
  ```
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJyb2xlcyI6WyJBZG1pbiIsIlVzZXIiXSwiaXNzIjoiY29tcGFueS5jb20iLCJhdWQiOiJhcGkuY29tcGFueS5jb20iLCJleHBpcmVkIjoxNjYwMDMzNzYwfQ.S7_D9y6x8gNxfWgfkF5lHnTj_K0Gjr9b6Sryj0qf-V8
    ```



- # ToChck Authentication and Authorization in swagger:
   - just add these lines into your program.cs file
   - at the top in program.cs file add this --> `using Microsoft.OpenApi.Models`
     ```
     builder.Services.AddSwaggerGen(options =>
     {
         options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Walks API", Version = "v1" });
         options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
     {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
     });

     options.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
         }
      });
     });
     ```
  
 



     

         

          


      





    

   



