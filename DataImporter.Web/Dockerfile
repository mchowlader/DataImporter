#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["DataImporter.Web/DataImporter.Web.csproj", "DataImporter.Web/"]
COPY ["DataImporter.Transfer/DataImporter.Transfer.csproj", "DataImporter.Transfer/"]
COPY ["DataImporter.User/DataImporter.User.csproj", "DataImporter.User/"]
COPY ["DataImporter.ExcelFileReader/DataImporter.ExcelFileReader.csproj", "DataImporter.ExcelFileReader/"]
COPY ["DataImporter.Common/DataImporter.Common.csproj", "DataImporter.Common/"]
COPY ["DataImporter.Data/DataImporter.Data.csproj", "DataImporter.Data/"]
COPY ["DataImporter.ExcelFileCreate/DataImporter.ExcelFileCreate.csproj", "DataImporter.ExcelFileCreate/"]
RUN dotnet restore "DataImporter.Web/DataImporter.Web.csproj"
COPY . .
WORKDIR "/src/DataImporter.Web"
RUN dotnet build "DataImporter.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataImporter.Web.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataImporter.Web.dll"]