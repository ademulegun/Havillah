#Stage 1 Define base image that will be used for production
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Stage 2 Build and publish the code
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["Havillah.ApplicationServices/Havillah.ApplicationServices.csproj", "Havillah.ApplicationServices/"]
COPY ["Havillah.Core/Havillah.Core.csproj", "Havillah.Core/"]
COPY ["Havillah.Shared/Havillah.Shared.csproj", "Havillah.Shared/"]
COPY ["Havillah.Infrasructure/Havillah.Infrasructure.csproj", "Havillah.Infrasructure/"]
COPY ["Havillah.Persistence/Havillah.Persistence.csproj", "Havillah.Persistence/"]
RUN dotnet restore "API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

#Stage 3: Build and publish the code
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

#Stage 4: Build and publish the code
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
