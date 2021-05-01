# AdventureWorks 2019 ASP.NET Core MVC

This application uses Entity Framework Core to scaffold an existing AdventureWorks 2019 database. 


## EF Core scaffolding

Use the [.NET Core CLI to scaffold the existing database](https://docs.microsoft.com/en-us/ef/core/cli/dotnet#dotnet-ef-dbcontext-scaffold), placing the generated models in the Models folder:

```dotnetcli
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:AdventureWorks2019 "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2019"
dotnet ef dbcontext scaffold Name=ConnectionStrings:AdventureWorks2019 Microsoft.EntityFrameworkCore.SqlServer -o Models
```
Once the DbContext is created, you can now [generate pages using aspnet-codegenerator tooling](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-5.0&tabs=visual-studio-code#scaffold-movie-pages), placing the generated Controllers in the Controllers folder:

```dotnet
dotnet aspnet-codegenerator controller -name ProductsController -m Products -dc AdventureWorks2019Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```