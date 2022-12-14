FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HavillahWebUI_WebApp/HavillahWebUI_WebApp.csproj", "HavillahWebUI_WebApp/"]
RUN dotnet restore "HavillahWebUI_WebApp/HavillahWebUI_WebApp.csproj"
COPY . .
WORKDIR "/src/HavillahWebUI_WebApp"
RUN dotnet build "HavillahWebUI_WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HavillahWebUI_WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HavillahWebUI_WebApp.dll"]
