#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StarSound/StarSound.csproj", "StarSound/"]
RUN dotnet restore "./StarSound/./StarSound.csproj"
COPY . .
WORKDIR "/src/StarSound"
RUN dotnet build "./StarSound.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StarSound.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarSound.dll"]


FROM mcr.microsoft.com/mssql/server:latest


RUN apt-get update -y

RUN apt install libgl1-mesa-glx -y

# Install the Kerberos libraries
#RUN apt-get update && \
    #apt-get install -y krb5-user && \
    #apt-get clean
#