FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CryptoDashboard.Api.csproj", "."]
RUN dotnet restore "./CryptoDashboard.Api.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "CryptoDashboard.Api.csproj" -c Release -o /app/build
RUN dotnet publish "CryptoDashboard.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CryptoDashboard.Api.dll"]
