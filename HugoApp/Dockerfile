FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY HugoApp/HugoApp.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y compilar
COPY . ./
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados al contenedor
COPY --from=build /app/publish ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV PORT=5000

ENTRYPOINT ["dotnet", "HugoApp.dll"]


