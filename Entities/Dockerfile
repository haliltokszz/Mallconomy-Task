FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Entities/Entities.csproj", "Entities/"]
RUN dotnet restore "Entities/Entities.csproj"
COPY . .
WORKDIR "/src/Entities"
RUN dotnet build "Entities.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Entities.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Entities.dll"]
