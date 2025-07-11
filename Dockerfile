# Базовый образ для runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем только файлы проектов для восстановления
COPY ["Tik-Tac-Toe.API.sln", "./"]
COPY ["Tik-Tac-Toe.API/Tik-Tac-Toe.API.csproj", "Tik-Tac-Toe.API/"]
COPY ["Tik-Tac-Toe.Buisnes/Tik-Tac-Toe.Buisnes.csproj", "Tik-Tac-Toe.Buisnes/"]
COPY ["Tik-Tac-Toe.Core/Tik-Tac-Toe.Core.csproj", "Tik-Tac-Toe.Core/"]
COPY ["Tik-Tac-Toe.DataAccess/Tik-Tac-Toe.DataAccess.csproj", "Tik-Tac-Toe.DataAccess/"]
RUN dotnet restore "Tik-Tac-Toe.API.sln"

# Копируем весь исходный код
COPY . .

# Собираем проект
WORKDIR "/src/Tik-Tac-Toe.API"
RUN dotnet build "Tik-Tac-Toe.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tik-Tac-Toe.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Явно копируем appsettings.json из правильной директории
COPY ["Tik-Tac-Toe.API/appsettings.json", "./"]

ENTRYPOINT ["dotnet", "Tik-Tac-Toe.API.dll"]