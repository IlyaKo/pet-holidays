# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LosTomates.PetHolidays.WebApi/LosTomates.PetHolidays.WebApi.csproj", "LosTomates.PetHolidays.WebApi/"]
COPY ["LosTomates.PetHolidays.Application/LosTomates.PetHolidays.Application.csproj", "LosTomates.PetHolidays.Application/"]
COPY ["LosTomates.PetHolidays.Core/LosTomates.PetHolidays.Core.csproj", "LosTomates.PetHolidays.Core/"]
COPY ["LosTomates.PetHolidays.DataAccess/LosTomates.PetHolidays.DataAccess.csproj", "LosTomates.PetHolidays.DataAccess/"]
RUN dotnet restore "./LosTomates.PetHolidays.WebApi/LosTomates.PetHolidays.WebApi.csproj"
COPY . .
WORKDIR "/src/LosTomates.PetHolidays.WebApi"
RUN dotnet build "./LosTomates.PetHolidays.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LosTomates.PetHolidays.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LosTomates.PetHolidays.WebApi.dll"]