#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["DesafioTecnico.WebAPI/DesafioTecnico.WebAPI.csproj", "DesafioTecnico.WebAPI/"]
RUN dotnet restore "./DesafioTecnico.WebAPI/./DesafioTecnico.WebAPI.csproj"
COPY . .
WORKDIR "/src/DesafioTecnico.WebAPI"
RUN dotnet build "./DesafioTecnico.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./DesafioTecnico.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioTecnico.WebAPI.dll"]
