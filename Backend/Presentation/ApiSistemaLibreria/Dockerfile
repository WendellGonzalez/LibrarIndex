# -----------------------------
# Etapa 1: Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar solo los .csproj de todos los proyectos para restaurar dependencias
COPY ./Backend/Core/Domain/Domain.csproj ./Core/Domain/
COPY ./Backend/Core/Application/Application.csproj ./Core/Application/
COPY ./Backend/Infraestructure/Infraestructure.csproj ./Infraestructure/
COPY ./Backend/Presentation/ApiSistemaLibreria/ApiSistemaLibreria.csproj ./Presentation/ApiSistemaLibreria/

# Restaurar dependencias
RUN dotnet restore ./Presentation/ApiSistemaLibreria/ApiSistemaLibreria.csproj

# Copiar todo el código
COPY ./Backend ./ 

# Limpiar obj/bin para evitar duplicados de AssemblyInfo
RUN find . -type d -name obj -exec rm -rf {} + \
    && find . -type d -name bin -exec rm -rf {} +

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
