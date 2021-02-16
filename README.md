# TiendaServicios

## Bases de datos

- PostgreSQL : tiendalibroautor
- SQL Server : tiendalibro
- MySQL : carritocompra

## Comandos de Migraciones Iniciales

```powershell
dotnet tool install --global dotnet-ef --version 5.0.3
dotnet-ef migrations add MigracionPostgresInicial --project TiendaServicios.Api.Autor
dotnet-ef database update --project TiendaServicios.Api.Autor
dotnet-ef migrations add MigracionSqlServerInicial --project TiendaServicios.Api.Libro
dotnet-ef database update --project TiendaServicios.Api.Libro
dotnet-ef migrations add MigracionMySqlInicial --project TiendaServicios.Api.CarritoCompra
dotnet-ef database update --project TiendaServicios.Api.CarritoCompra
```
