#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Autor/TiendaServicios.Api.Autor.csproj", "TiendaServicios.Api.Autor/"]
COPY ["TiendaServicios.Mensajeria.Email/TiendaServicios.Mensajeria.Email.csproj", "TiendaServicios.Mensajeria.Email/"]
COPY ["TiendaServicios.RabbitMQ.Bus/TiendaServicios.RabbitMQ.Bus.csproj", "TiendaServicios.RabbitMQ.Bus/"]
RUN dotnet restore "TiendaServicios.Api.Autor/TiendaServicios.Api.Autor.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Autor"
RUN dotnet build "TiendaServicios.Api.Autor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Autor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Autor.dll"]