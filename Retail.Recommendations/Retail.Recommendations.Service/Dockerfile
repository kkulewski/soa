#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Retail.Recommendations.Service.csproj", "Retail.Recommendations.Service/"]
RUN dotnet restore "Retail.Recommendations.Service/Retail.Recommendations.Service.csproj"
COPY . .
WORKDIR "/src/Retail.Recommendations.Service"
RUN dotnet build "Retail.Recommendations.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Retail.Recommendations.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retail.Recommendations.Service.dll"]
