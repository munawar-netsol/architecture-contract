cd ContractActivationService
cd ContractActivationService
rmdir app /s /Q
dotnet publish "ContractActivationService/ContractActivationService.csproj" -c Release -o app/publish
cd..
cd..

cd ContractModificationService
rmdir app /s /Q
dotnet publish "ContractModificationService.csproj" -c Release -o app/publish
cd..

cd PublicApiGateway
rmdir app /s /Q
dotnet publish "PublicApiGateway.csproj" -c Release -o app/publish
cd..

cd Microflows
cd MicroflowsConfiguration
rmdir app /s /Q
dotnet publish "MicroflowsConfiguration.csproj" -c Release -o app/publish
cd..
cd..

docker compose --env-file env-secret-configurations.env up --build -d

