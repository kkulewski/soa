#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Retail.Shipping.Host.csproj", "Retail.Shipping.Host/"]
RUN dotnet restore "Retail.Shipping.Host/Retail.Shipping.Host.csproj"
COPY . .
WORKDIR "/src/Retail.Shipping.Host"
RUN dotnet build "Retail.Shipping.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Retail.Shipping.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retail.Shipping.Host.dll"]
