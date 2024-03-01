FROM mcr.microsoft.com/dotnet/sdk:8.0 as base
RUN apt-get update && apt-get install -y wget

WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /src
COPY ["WebApp.csproj", "."]
RUN dotnet restore "WebApp.csproj"

COPY . .
WORKDIR /src
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build as publish 
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

from base as final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "WebApp.dll"]