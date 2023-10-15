# family-budget

## Simple Demo application Family Budget

Please make sure you have installed .NET CLI and docker

In order to build app you can launch it from Visual Studo.
Docker-compose is set as startup project, and will download (if needed)
build and run conteners.

DB (SQL Server) is in separate container. In current configuration it has volume attached,
to have some level of data persistency.


Simple app provides a Swagger UI (that will show up once you build&run solution).
There is an option to add users, budgets, and budgets items (expenses or incomes).


## Testing
In the solution are two test projects.
First contains some example of unit test covers FamilyBudget.Core project.
Second contains integration test. Higher level tests, that performs calls to
in-memory hosted API. (In this case EF Core is configured to also work in
in-memory mode).

You can either run test directly from VS, or alternativly use the .NET CLI command:
```
dotnet test ./test/FamilyBudget.Web.Tests/FamilyBudget.Web.Tests.csproj
```
or
```
 dotnet test ./test/FamilyBudget.Code.Tests/FamilyBudget.Code.Tests.csproj
```

## migrations
In order to generate new migration script run following command from console:


```
dotnet ef migrations add InitialCreate --project ./src/FamilyBudget.Infra/FamilyBudget.Infra.csproj  --startup-project ./src/FamilyBudget.Web/FamilyBudget.Web.csproj
```

To apply migration run following:
```
 dotnet ef database update --project ./src/FamilyBudget.Infra/FamilyBudget.Infra.csproj  --startup-project ./src/FamilyBudget.Web/FamilyBudget.Web.csproj
```

## Auth - there is a Keycloak instance configured
In order to log in as admin please admin/admin.
We need to add a reaml and  configure a client.
This is not integrated yet.
