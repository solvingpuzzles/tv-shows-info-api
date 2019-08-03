FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /src
# COPY ./src/Host/Host.csproj /src/Host/Host.csproj
COPY ./src/Host /src/Host
COPY ./src/Data /src/Data
COPY ./src/Services /src/Services

RUN dotnet restore /src/Host/Host.csproj

# COPY ./src/Host /src/Host
# COPY ./src/Data /src/Data
# COPY ./src/Services /src/Services
RUN dotnet publish /src/Host/Host.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 80

ENTRYPOINT ["dotnet", "Host.dll"]
