Create appsettings.json.user file and set it to copy to output directory on build.
```json
{
  "connectionStrings": {
   "EFCoreContext": "Server=tcp:techorama.database.windows.net,1433;Initial Catalog=Techorama_Challenge;Persist Security Info=False;User ID=...TechoReadOnly;Password=...;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```