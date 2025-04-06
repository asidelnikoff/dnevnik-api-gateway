FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Dnevnik.ApiGateway
COPY ["/src/Dnevnik.ApiGateway/Dnevnik.ApiGateway.csproj", "."]
RUN dotnet restore "Dnevnik.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Dnevnik.ApiGateway"
RUN dotnet build "Dnevnik.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Dnevnik.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dnevnik.ApiGateway.dll"]
