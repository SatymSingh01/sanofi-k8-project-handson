# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/Lib-AppBase/Lib-AppBase.csproj Lib-AppBase/
COPY src/AppOne/AppOne.csproj AppOne/
RUN dotnet restore AppOne/AppOne.csproj

COPY src/Lib-AppBase/ Lib-AppBase/
COPY src/AppOne/ AppOne/
WORKDIR /src/AppOne
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "AppOne.dll"]
