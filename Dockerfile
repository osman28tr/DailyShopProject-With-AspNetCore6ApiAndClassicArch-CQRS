from mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY ./DailyShop.Entities/*.csproj ./DailyShop.Entities/
COPY ./DailyShop.DataAccess/*.csproj ./DailyShop.DataAccess/
COPY ./DailyShop.Business/*.csproj ./DailyShop.Business/
COPY ./DailyShop.API/*.csproj ./DailyShop.API/
COPY ./corePackages/Core.Application/*.csproj ./corePackages/Core.Application/
COPY ./corePackages/Core.CrossCuttingConcers/*.csproj ./corePackages/Core.CrossCuttingConcers/
COPY ./corePackages/Core.Persistence/*.csproj ./corePackages/Core.Persistence/
COPY ./corePackages/Core.Security/*.csproj ./corePackages/Core.Security/
COPY ./corePackages/Core.ElasticSearch/*.csproj ./corePackages/Core.ElasticSearch/
COPY ./corePackages/Core.Mailing/*.csproj ./corePackages/Core.Mailing/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./DailyShop.API/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5025"
ENTRYPOINT [ "dotnet", "DailyShop.API.dll"]