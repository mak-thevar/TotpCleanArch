var builder = DistributedApplication.CreateBuilder(args);


var username = builder.AddParameter("pg-user", secret: true);
var password = builder.AddParameter("pg-password", secret: true);

var postgres = builder.AddPostgres("postgres", username, password)
               .WithDataVolume("postgres-data")
               .WithPgAdmin()
               .AddDatabase("totp-db");

builder.AddProject<Projects.TotpCleanArch_WebApi>("totpcleanarch").WithReference(postgres);

builder.Build().Run();
