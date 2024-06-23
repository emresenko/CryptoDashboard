FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CryptoDashboard.Client.csproj", "."]
RUN dotnet restore "./CryptoDashboard.Client.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "CryptoDashboard.Client.csproj" -c Release -o /app/build
RUN dotnet publish "CryptoDashboard.Client.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CryptoDashboard.Client.dll"]
