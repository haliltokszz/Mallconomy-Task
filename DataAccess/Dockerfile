﻿FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "DataAccess/DataAccess.csproj"
COPY . .
WORKDIR "/src/DataAccess"
RUN dotnet build "DataAccess.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataAccess.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataAccess.dll"]
