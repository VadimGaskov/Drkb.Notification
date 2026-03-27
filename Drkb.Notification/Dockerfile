FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Drkb.Notification/Drkb.Notification.csproj", "Drkb.Notification/"]
COPY ["Drkb.Notification.Infrastructure/Drkb.Notification.Infrastructure.csproj", "Drkb.Notification.Infrastructure/"]
COPY ["Drkb.Notification.Application/Drkb.Notification.Application.csproj", "Drkb.Notification.Application/"]
COPY ["Drkb.Notification.Domain/Drkb.Notification.Domain.csproj", "Drkb.Notification.Domain/"]
COPY ["Drkb.Notification.Contract/Drkb.Notification.Contract.csproj", "Drkb.Notification.Contract/"]
COPY ["Drkb.Notification.Integration/Drkb.Notification.Integration.csproj", "Drkb.Notification.Integration/"]
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
