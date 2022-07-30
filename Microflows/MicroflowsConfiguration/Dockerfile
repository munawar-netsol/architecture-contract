FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5002
COPY . .
ENTRYPOINT ["dotnet", "MicroflowsConfiguration.dll"]