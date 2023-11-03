#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN apt-get update && apt-get install -y procps && rm -rf /var/lib/apt/lists/*
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Asumet.Doc.Api/Asumet.Doc.Api.csproj", "Asumet.Doc.Api/"]
RUN dotnet restore "Asumet.Doc.Api/Asumet.Doc.Api.csproj"
COPY . .
WORKDIR "/src/Asumet.Doc.Api"
RUN dotnet build "Asumet.Doc.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Asumet.Doc.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asumet.Doc.Api.dll"]