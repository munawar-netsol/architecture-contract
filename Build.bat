cd ContractActivationService
cd ContractActivationService
rmdir app /s /Q
dotnet publish "ContractActivationService.csproj" -c Release -o app/publish
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

docker-compose up --build -d
