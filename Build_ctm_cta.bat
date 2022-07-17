cd ContractActivationService
cd ContractActivationService
dotnet publish "ContractActivationService.csproj" -c Release -o app/publish
cd..
cd..

cd ContractModificationService
dotnet publish "ContractModificationService.csproj" -c Release -o app/publish
cd..
