FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Копирование и восстановление зависимостей
COPY *.csproj .
RUN dotnet restore

# Копирование и сборка приложения
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
COPY Certificates /app/Certificates
ENTRYPOINT ["dotnet", "TrashGrounds.Gateway.dll"]