# TiendaServicios

## Bases de datos

- PostgreSQL : tiendalibroautor
- SQL Server : tiendalibro
- MySQL : carritocompra

## Comandos de migraciones iniciales (deben de tener las bases de datos iniciadas)

Cambiar las cadenas de conexión de las configuraciones a localhost para que funcionen los comandos

```powershell
dotnet tool install --global dotnet-ef --version 5.0.3
dotnet-ef migrations add MigracionPostgresInicial --project TiendaServicios.Api.Autor
dotnet-ef database update --project TiendaServicios.Api.Autor
dotnet-ef migrations add MigracionSqlServerInicial --project TiendaServicios.Api.Libro
dotnet-ef database update --project TiendaServicios.Api.Libro
dotnet-ef migrations add MigracionMySqlInicial --project TiendaServicios.Api.CarritoCompra
dotnet-ef database update --project TiendaServicios.Api.CarritoCompra
```

Desde el momento que se use docker compose, ya no se va a poder usar el comando de migraciones de manera global. 
Se va a tener que ingresar a cada proyecto en la consola y ejecutar el comando sin el --project correspondiente

## Comandos de despliegue en Docker

### PostgreSQL (a la versión 13)

```powershell
docker pull postgres:latest
docker run --name postgres-container -e POSTGRES_PASSWORD=123456 -d -p 5432:5432 postgres:latest
docker exec -it postgres-container bash
psql -U postgres
create database tiendalibroautor;
```

### Configuración del network para la compatibilidad del docker-compose de TiensaServicios.Api.Autor

```powershell
docker network create microservicenet
docker network connect microservicenet postgres-container
docker network inspect microservicenet
```

### SQL Server (a la versión 13)

```powershell
docker pull mcr.microsoft.com/mssql/server
docker run --name mssql-container -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=123456$Aa-' -p 1433:1433 -d mcr.microsoft.com/mssql/server
docker network connect microservicenet mssql-container
```

* Crear la base de datos en Management Studio

### MySQL (a la versión 8)

```powershell
docker pull mysql
docker run --name mysql-container -d -p 3306:3306 -e "MYSQL_ROOT_PASSWORD=123456" mysql
docker network connect microservicenet mysql-container
```

* Crear la base de datos en HeidiSQL

### RabbitMQ (a la versión 3)

#### Versión sin gestor web

```powershell 
docker run -d --hostname mi-rabbit-server --name rabbitmq-container rabbitmq:3 
docker network connect microservicenet rabbitmq-container
```

#### Versión con gestor web
```powershell 
docker run -d --hostname mi-rabbit-server --name rabbitmq-web-container -p 15672:15672 rabbitmq:3-management
docker network connect microservicenet rabbitmq-web-container
```
