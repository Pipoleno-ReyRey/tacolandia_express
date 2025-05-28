# tacolandia_express

Este proyecto es una implementación personal de un sistema de gestión de pedidos y administración de un restaurante de tacos, desarrollado en C# y .NET. Está estructurado en varios servicios que gestionan diferentes aspectos de la aplicación, como pedidos, autenticación y administración de productos.

## Estructura del Proyecto

El repositorio se organiza en los siguientes servicios:

- Pedidos_service: Maneja la creación, edición y eliminación de pedidos.
- Login_service: Gestiona la autenticación y autorización de los usuarios mediante JWT.
- Products_service: Administra la gestión de productos y configuración del menú.

## Tecnologías Utilizadas

- Lenguaje de Programación: C# 12 y .NET/ASP.NET
- Base de Datos: MySQL
- Autenticación: JWT
- Contenedorización: Docker y Kubernetes
- Entorno de Desarrollo: Visual Studio con configuraciones optimizadas

## API Endpoints

### Orders Service
- GET pedidos/getDishes Obtener todos platos que fueron pedidos
- GET pedidos/getDish/{id} Obtener un pedido en especifico
- POST pedidos/postDish Ingresar un pedido
- DELETE pedidos/deleteDish/{id} Borrar un pedido

### Login Service
- POST /login Registro de usuario 
- GET sign_up/ Obtener un usuario

### Storage Service
- GET storage/items Obtener todos los items del inventario
- POST storage/postItem Ingresar un item al inventario
- PUT storage/updateItem Actualizar un item del inventario
