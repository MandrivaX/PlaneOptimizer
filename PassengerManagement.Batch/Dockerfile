FROM mcr.microsoft.com/dotnet/runtime:5.0 
COPY bin/Release/net5.0/publish .
COPY /Passengers/Passengers.txt /Passengers/Passengers.txt .
ENTRYPOINT ["dotnet", "PassengerManagement.Batch.dll"]