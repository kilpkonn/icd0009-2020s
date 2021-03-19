# Car App

## How to run locally

### Start development database
```bash
# Use -d for detatched
docker-compose up [-d]
```
### Run your app
Running from IDE works just fine or `./path/to/executable`

## How to develop

### Create migrations
```bash
dotnet ef migrations add InitialMigration --project DAL.App.EF --startup-project WebApplication
```

### Create MVC Controllers
```bash
dotnet aspnet-codegenerator controller -name CarController        -actions -m  Car        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
```

### Create ApiControllers
```bash
dotnet aspnet-codegenerator controller -name CarController     -m Car     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
```