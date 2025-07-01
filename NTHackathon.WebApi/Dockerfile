FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NTHackathon.WebApi/NTHackathon.WebApi.csproj", "NTHackathon.WebApi/"]
RUN dotnet restore "NTHackathon.WebApi/NTHackathon.WebApi.csproj"
COPY . .
WORKDIR "/src/NTHackathon.WebApi"
RUN dotnet build "./NTHackathon.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NTHackathon.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NTHackathon.WebApi.dll"]
