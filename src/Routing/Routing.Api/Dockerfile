FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Routing.Api/Routing.Api.csproj", "Routing.Api/"]
RUN dotnet restore "Routing.Api/Routing.Api.csproj"
COPY . .
WORKDIR "/src/Routing.Api"
RUN dotnet build "Routing.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Routing.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Routing.Api.dll"]