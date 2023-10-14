## Simple Demo application Family Budget


## Auth - there is a Keycloak instance configured
In order to log in as admin please admin/admin.
We need to add a reaml and  configure a client .

## migrations
In order to generate new migration script run following command from console:


```
dotnet ef migrations add InitialCreate --project ./src/FamilyBudget.Infra/FamilyBudget.Infra.csproj  --startup-project ./src/FamilyBudget.Web/FamilyBudget.Web.csproj
```