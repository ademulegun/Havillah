#Stage 1 Define base image that will be used for production
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Stage 2 Build and publish the code
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HavillahWebUI_Server/HavillahWebUI_Server.csproj", "HavillahWebUI_Server/"]
COPY ["Havillah.ApplicationServices/Havillah.ApplicationServices.csproj", "Havillah.ApplicationServices/"]
COPY ["Havillah.Core/Havillah.Core.csproj", "Havillah.Core/"]
COPY ["Havillah.Shared/Havillah.Shared.csproj", "Havillah.Shared/"]
RUN dotnet restore "HavillahWebUI_Server/HavillahWebUI_Server.csproj"
COPY . .
WORKDIR "/src/HavillahWebUI_Server"
RUN dotnet build "HavillahWebUI_Server.csproj" -c Release -o /app/build

#Stage 3: Build and publish the code
FROM build AS publish
RUN dotnet publish "HavillahWebUI_Server.csproj" -c Release -o /app/publish

#Stage 4: Build and publish the code
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HavillahWebUI_Server.dll"]
