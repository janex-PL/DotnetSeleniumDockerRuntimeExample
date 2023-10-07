FROM runtime-chrome:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SeleniumChromeDocker.csproj", "./"]
RUN dotnet restore "SeleniumChromeDocker.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "SeleniumChromeDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SeleniumChromeDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SeleniumChromeDocker.dll"]
