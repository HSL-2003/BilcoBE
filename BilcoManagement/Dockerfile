# ================================
# 🚀 Base Runtime
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Render / Fly.io chạy trên port 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy file csproj
COPY ["BilcoManagement.csproj", "./"]


RUN dotnet restore "BilcoManagement.csproj"


COPY . .


RUN dotnet build "BilcoManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BilcoManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BilcoManagement.dll"]
