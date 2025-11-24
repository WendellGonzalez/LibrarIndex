# -----------------------------
# Etapa 1: Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar todos los proyectos para restaurar correctamente
COPY ./Backend/Core/Core.Domain/Core.Domain.csproj ./Core/Domain/
COPY ./Backend/Core/Core.Application/Core.Application.csproj ./Core/Application/
COPY ./Backend/Infraestructure/Infraestructure.csproj ./Infraestructure/
COPY ./Backend/Presentation/ApiSistemaLibreria/ApiSistemaLibreria.csproj ./Presentation/ApiSistemaLibreria/

# Restaurar dependencias
RUN dotnet restore ./Presentation/ApiSistemaLibreria/ApiSistemaLibreria.csproj

# Copiar todo el código
COPY ./Backend ./ 

# Publicar en modo Release
RUN dotnet publish ./Presentation/ApiSistemaLibreria/ApiSistemaLibreria.csproj -c Release -o out

# -----------------------------
# Etapa 2: Runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar los archivos publicados desde la build
COPY --from=build /app/out ./

# Exponer el puerto de la API
EXPOSE 8080

# Comando de inicio
ENTRYPOINT ["dotnet", "ApiSistemaLibreria.dll"]
