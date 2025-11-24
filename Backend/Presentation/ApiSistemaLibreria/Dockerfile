# Imagen base para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar archivos del proyecto
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto del código
COPY . ./

# Publicar la aplicación en modo release
RUN dotnet publish -c Release -o out

# Imagen base para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar publicación desde la build
COPY --from=build /app/out ./

# Exponer el puerto donde corre tu API
EXPOSE 8080

# Comando de inicio
ENTRYPOINT ["dotnet", "ApiSistemaLibreria.dll"]
