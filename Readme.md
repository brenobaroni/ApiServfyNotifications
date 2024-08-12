

```bash

##to Migrate

=> change schema
=> npm db:push

=> dotnet ef dbcontext scaffold "Host=localhost:5432;Database=postgres;Username=postgres;Password=pass!" Npgsql.EntityFrameworkCore.PostgreSQL --context-dir DbContext --context Context/ServfyBaseContext --output-dir Entities --project Api.Servfy.Base.Domain