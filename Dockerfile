FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

ARG NUGET_TOKEN

RUN dotnet nuget remove source drkb-private || true
RUN dotnet nuget locals all --clear

# Добавляем приватный источник

RUN dotnet nuget add source https://baget.drkb-portal.ru/v3/index.json \
    --name drkb-private \
    --username drkb \
    --password $NUGET_TOKEN \
    --store-password-in-clear-text

COPY ["Drkb.Notification/Drkb.Notification.csproj", "Drkb.Notification/"]
COPY ["Drkb.Notification.Domain/Drkb.Notification.Domain.csproj", "Drkb.Notification.Domain/"]
COPY ["Drkb.Notification.Application/Drkb.Notification.Application.csproj", "Drkb.Notification.Application/"]
COPY ["Drkb.Notification.Infrastructure/Drkb.Notification.Infrastructure.csproj", "Drkb.Notification.Infrastructure/"]
RUN dotnet restore "Drkb.Notification/Drkb.Notification.csproj"
COPY . .
WORKDIR "/src/Drkb.Notification"
RUN dotnet build "Drkb.Notification.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Drkb.Notification.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Drkb.Notification.dll"]
