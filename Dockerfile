#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Dogstray/Dogstray.csproj", "Dogstray/"]
RUN dotnet restore "Dogstray/Dogstray.csproj"
COPY . .
WORKDIR "/src/Dogstray"
RUN dotnet build "Dogstray.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dogstray.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dogstray.dll"]