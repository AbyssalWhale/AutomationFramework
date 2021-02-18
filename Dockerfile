FROM mcr.microsoft.com/dotnet/sdk:3.1

WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet build AutomationFramework.sln

WORKDIR /app/RegressionApiTests/bin/Debug/netcoreapp3.1
 

WORKDIR /App
ENTRYPOINT ["dotnet", "test", "--settings", "/app/TestConfigurator/RunSettings/InstanceSettings.runsettings", "/app/RegressionApiTests/bin/Debug/netcoreapp3.1/RegressionApiTests.dll", "/InIsolation", "/Logger:trx"]