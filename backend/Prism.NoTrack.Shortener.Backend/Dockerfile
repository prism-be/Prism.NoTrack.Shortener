FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Prism.NoTrack.Shortener.Backend/Prism.NoTrack.Shortener.Backend.csproj", "Prism.NoTrack.Shortener.Backend/"]
RUN dotnet restore "Prism.NoTrack.Shortener.Backend/Prism.NoTrack.Shortener.Backend.csproj"
COPY . .
WORKDIR "/src/Prism.NoTrack.Shortener.Backend"
RUN dotnet build "Prism.NoTrack.Shortener.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Prism.NoTrack.Shortener.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prism.NoTrack.Shortener.Backend.dll"]
