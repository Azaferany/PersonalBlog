dotnet tool update --global dotnet-ef --version 5.0.0
dotnet build
dotnet ef --startup-project ../PersonalBlog.Web/ database update --context ProjectDbContext
pause