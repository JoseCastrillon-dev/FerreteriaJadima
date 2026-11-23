CREATE DATABASE Ferreteria_Jadima;
GO
USE Ferreteria_Jadima;
GO
CREATE TABLE [Personas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[cedula] NVARCHAR(15) NOT NULL,
	[nombre] NVARCHAR(80) NOT NULL, 
	[apellido] NVARCHAR(80) NOT NULL, 
	[numeroTelefono] NVARCHAR(15) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [EstadoCreditos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[estado] NVARCHAR(50) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);

CREATE TABLE [Cargos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[nombre] NVARCHAR(100) UNIQUE NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);

CREATE TABLE [TiposDePagos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[metodo] NVARCHAR(50) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Tipos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[nombre] NVARCHAR(50) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Vehiculos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[matricula] NVARCHAR(10) NOT NULL UNIQUE, 
	[marca] NVARCHAR(50) NOT NULL,
	[numeroDeChasis] NVARCHAR(50) NOT NULL UNIQUE, 
	[numeroMotor] NVARCHAR(50) NOT NULL UNIQUE,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Proveedores](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[nombre] NVARCHAR(100) NOT NULL,
	[numeroDeTelefono] NVARCHAR(20) NOT NULL,
	[direccion] NVARCHAR(150) NOT NULL,
	[email] NVARCHAR(100) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Clientes](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_persona] INT REFERENCES [Personas]([id]),
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Salarios](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_cargo] INT REFERENCES [Cargos]([id]),
	[id_tiposDePago] INT REFERENCES [TiposDePagos]([id]),
	[valorHora] DECIMAL(18,2) NOT NULL, 
	[horasTrabajadas] INT NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Productos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_tipo] INT REFERENCES [Tipos]([id]),
	[codigo] NVARCHAR(30) NOT NULL, 
	[cantidad] INT NOT NULL, 
	[nombre] NVARCHAR(100) UNIQUE NOT NULL,
	[valor] DECIMAL(18,2) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [DetalleVentas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_producto] INT REFERENCES [Productos]([id]),
	[cantidad] INT NOT NULL,
	[iva] DECIMAL(18,2) NOT NULL,
	[total] DECIMAL(18,2) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Empleados](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_persona] INT REFERENCES [Personas]([id]),
	[id_salario] INT REFERENCES [Salarios]([id]),
	[carnet] NVARCHAR(20) NOT NULL,
	[genero] NVARCHAR(20) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Estanterias](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[id_producto] INT REFERENCES [Productos]([id]),
	[codigo] NVARCHAR(20) NOT NULL, 
	[stock] INT NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Compras](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_proveedor] INT REFERENCES [Proveedores]([id]),
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[fechaCompra] DATETIME NULL, 
	[total] DECIMAL(18,2) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [DetalleCompras](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_compra] INT REFERENCES [Compras]([id]),
	[id_producto] INT REFERENCES [Productos]([id]),
	[cantidad] INT NOT NULL,
	[preciosCompra] DECIMAL(18,2) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Ventas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_detalleVenta] INT REFERENCES [DetalleVentas]([id]),
	[id_tipoDePago] INT REFERENCES [TiposDePagos]([id]),
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[id_cliente] INT REFERENCES [Clientes]([id]),
	[iva] DECIMAL(18,2) NOT NULL,
	[total] DECIMAL(18,2) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Creditos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[idVenta] INT REFERENCES [Ventas]([id]),
	[id_estadoCredito] INT REFERENCES [EstadoCreditos]([id]), 
	[fechaPrestamo] SMALLDATETIME NULL,
	[valorPrestamo] DECIMAL(18,2) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Cuotas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_credito] INT REFERENCES [Creditos]([id]),
	[id_tipoDePago] INT REFERENCES [TiposDePagos]([id]),
	[fechaDePago] DATETIME NULL, 
	[valorDeCuota] DECIMAL(18,2) NOT NULL, 
	[numeroDeCuota] INT NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);

CREATE TABLE [Domicilios](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_venta] INT REFERENCES [Ventas]([id]),
	[id_vehiculo] INT REFERENCES [Vehiculos]([id]),
	[numeroDeTelefono] NVARCHAR(20) NOT NULL,
	[direccion] NVARCHAR(150) NOT NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Entregas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_domicilio] INT REFERENCES [Domicilios]([id]),
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[fechaSalida] DATETIME NULL, 
	[fechaLlegada] DATETIME NULL,
	[imagen] VARCHAR(MAX) NULL
);
CREATE TABLE [Auditoria] (
    [id]     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Desc]	           NVARCHAR(60) NOT NULL,
    [fecha]            DATETIME        NOT NULL,
    [tablaAfectada]    NVARCHAR(100)             NOT NULL
);

CREATE TABLE [Usuarios](
	[id]     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Usuario]	     NVARCHAR(50) NOT NULL,
	[Contraseña]	 NVARCHAR(50) NOT NULL,
	[rol] INT NOT NULL DEFAULT 2 REFERENCES Roles(id);
);

CREATE TABLE [Roles] (
    [id]     INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [Permisos] (
    [id]      INT IDENTITY(1,1) PRIMARY KEY,
    [id_rol]  INT NOT NULL UNIQUE REFERENCES Roles(id),
    [ver]     BIT NOT NULL DEFAULT 0,   -- consultar y buscar
    [guardar] BIT NOT NULL DEFAULT 0,   -- agregar, editar, actualizar
    [borrar]  BIT NOT NULL DEFAULT 0
);

INSERT INTO Roles (nombre) VALUES ('Administrador'), ('Usuario');

INSERT INTO Permisos (id_rol, ver, guardar, borrar) VALUES
 (1, 1, 1, 1),   
 (2, 1, 0, 0);  
