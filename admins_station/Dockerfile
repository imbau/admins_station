FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50700
EXPOSE 44301

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["admins_station/admins_station.csproj", "admins_station/"]
RUN dotnet restore "admins_station/admins_station.csproj"
COPY . .
WORKDIR "/src/admins_station"
RUN dotnet build "admins_station.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "admins_station.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "admins_station.dll"]