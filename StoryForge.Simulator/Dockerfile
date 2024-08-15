#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StoryForge.Simulator/StoryForge.Simulator.csproj", "StoryForge.Simulator/"]
COPY ["StoryForge.Application/StoryForge.Application.csproj", "StoryForge.Application/"]
COPY ["StoryForge.Core/StoryForge.Core.csproj", "StoryForge.Core/"]
COPY ["StoryForge.Infrastructure/StoryForge.Infrastructure.csproj", "StoryForge.Infrastructure/"]
RUN dotnet restore "./StoryForge.Simulator/StoryForge.Simulator.csproj"
COPY . .
WORKDIR "/src/StoryForge.Simulator"
RUN dotnet build "./StoryForge.Simulator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StoryForge.Simulator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoryForge.Simulator.dll"]