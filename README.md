# asumet-doc

## Dev Setup
* Set DB password to ASUMETDOCSECRETS__ASUMETDOCDBPASSWORD Env Variable. Windows Example:
```bash
[Environment]::SetEnvironmentVariable("ASUMETDOCSECRETS__ASUMETDOCDBPASSWORD", "MyPass", [System.EnvironmentVariableTarget]::User)
```

* Update Database with Migration scripts (execute from the solution root folder):
```bash
$vsProject = "Asumet.Doc.Repo";
$vsStartupProject = "Asumet.Doc.Api";
#dotnet ef migrations add InitialCreate --project $vsProject --startup-project $vsStartupProject;
dotnet ef database update --project $vsProject --startup-project $vsStartupProject;
```
