FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Playground.API/Playground.API.csproj", "src/Playground.API/"]
COPY ["src/Playground.Application/Playground.Application.csproj", "src/Playground.Application/"]
COPY ["src/Playground.Core/Playground.Core.csproj", "src/Playground.Core/"]
COPY ["src/Playground.Infrastructure/Playground.Infrastructure.csproj", "src/Playground.Infrastructure/"]
RUN dotnet restore "src/Playground.API/Playground.API.csproj"
COPY . .
WORKDIR "/src/src/Playground.API"
RUN dotnet build "Playground.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Playground.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Playground.API.dll"]
