# Define la imagen base de ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Define la imagen base del SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia el archivo .csproj correcto
COPY DoubleV.csproj ./

# Restaura las dependencias
RUN dotnet restore

# Copia el resto de los archivos
COPY . .

# Cambia al directorio del proyecto
WORKDIR "/src"  # Cambia aquí para estar en /src donde está el .csproj

# Construye la aplicación
RUN dotnet build "DoubleV.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "DoubleV.csproj" -c Release -o /app/publish

# Usa la imagen base ASP.NET Core Runtime para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DoubleV.dll"]
