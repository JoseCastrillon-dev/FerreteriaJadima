CREATE DATABASE Ferreteria_Jadima;
GO
USE Ferreteria_Jadima;
GO

CREATE TABLE [Personas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[cedula] NVARCHAR(15) NOT NULL,
	[nombre] NVARCHAR(80) NOT NULL, 
	[apellido] NVARCHAR(80) NOT NULL, 
	[numeroTelefono] NVARCHAR(15) NOT NULL
);

CREATE TABLE [EstadoCreditos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[estado] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Cargos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[nombre] NVARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE [TiposDePagos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[metodo] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Tipos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Vehiculos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[matricula] NVARCHAR(10) NOT NULL UNIQUE, 
	[marca] NVARCHAR(50) NOT NULL,
	[numeroDeChasis] NVARCHAR(50) NOT NULL UNIQUE, 
	[numeroMotor] NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [Proveedores](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[nombre] NVARCHAR(100) NOT NULL,
	[numeroDeTelefono] NVARCHAR(20) NOT NULL,
	[direccion] NVARCHAR(150) NOT NULL,
	[email] NVARCHAR(100) NOT NULL
);

CREATE TABLE [Clientes](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_persona] INT REFERENCES [Personas]([id])
);

CREATE TABLE [Salarios](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_cargo] INT REFERENCES [Cargos]([id]),
	[id_tiposDePago] INT REFERENCES [TiposDePagos]([id]),
	[valorHora] DECIMAL(18,2) NOT NULL, 
	[horasTrabajadas] INT NOT NULL
);

CREATE TABLE [Productos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_tipo] INT REFERENCES [Tipos]([id]),
	[codigo] NVARCHAR(30) NOT NULL, 
	[cantidad] INT NOT NULL, 
	[nombre] NVARCHAR(100) UNIQUE NOT NULL,
	[valor] DECIMAL(18,2) NOT NULL
);

CREATE TABLE [DetalleVentas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_producto] INT REFERENCES [Productos]([id]),
	[cantidad] INT NOT NULL,
	[iva] DECIMAL(18,2) NOT NULL,
	[total] DECIMAL(18,2) NOT NULL
);

CREATE TABLE [Empleados](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_persona] INT REFERENCES [Personas]([id]),
	[id_salario] INT REFERENCES [Salarios]([id]),
	[carnet] NVARCHAR(20) NOT NULL,
	[genero] NVARCHAR(20) NOT NULL
);

CREATE TABLE [Estanterias](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[id_producto] INT REFERENCES [Productos]([id]),
	[codigo] NVARCHAR(20) NOT NULL, 
	[stock] INT NOT NULL
);

CREATE TABLE [Compras](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_proveedor] INT REFERENCES [Proveedores]([id]),
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[fechaCompra] DATETIME NULL, 
	[total] DECIMAL(18,2) NOT NULL
);

CREATE TABLE [DetalleCompras](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_compra] INT REFERENCES [Compras]([id]),
	[id_producto] INT REFERENCES [Productos]([id]),
	[cantidad] INT NOT NULL,
	[preciosCompra] DECIMAL(18,2) NOT NULL
);

CREATE TABLE [Ventas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_detalleVenta] INT REFERENCES [DetalleVentas]([id]),
	[id_tipoDePago] INT REFERENCES [TiposDePagos]([id]),
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[id_cliente] INT REFERENCES [Clientes]([id]),
	[iva] DECIMAL(18,2) NOT NULL,
	[total] DECIMAL(18,2) NOT NULL
);

CREATE TABLE [Creditos](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[idVenta] INT REFERENCES [Ventas]([id]),
	[id_estadoCredito] INT REFERENCES [EstadoCreditos]([id]), 
	[fechaPrestamo] SMALLDATETIME NULL,
	[valorPrestamo] DECIMAL(18,2) NOT NULL
);

CREATE TABLE [Cuotas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_credito] INT REFERENCES [Creditos]([id]),
	[id_tipoDePago] INT REFERENCES [TiposDePagos]([id]),
	[fechaDePago] DATETIME NULL, 
	[valorDeCuota] DECIMAL(18,2) NOT NULL, 
	[numeroDeCuota] INT NOT NULL
);


CREATE TABLE [Domicilios](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_venta] INT REFERENCES [Ventas]([id]),
	[id_vehiculo] INT REFERENCES [Vehiculos]([id]),
	[numeroDeTelefono] NVARCHAR(20) NOT NULL,
	[direccion] NVARCHAR(150) NOT NULL
);

CREATE TABLE [Entregas](
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[id_domicilio] INT REFERENCES [Domicilios]([id]),
	[id_empleado] INT REFERENCES [Empleados]([id]),
	[fechaSalida] DATETIME NULL, 
	[fechaLlegada] DATETIME NULL
);

CREATE TABLE [Auditoria] (
    [idAuditoria]      BIGINT IDENTITY(1,1) NOT NULL,
    [fecha]            DATETIME2(0)         NOT NULL
		CONSTRAINT DF_Auditoria_fecha DEFAULT (SYSDATETIME()),
    [usuario]          SYSNAME              NOT NULL
        CONSTRAINT DF_Auditoria_usuario DEFAULT (SUSER_SNAME()),
    [tablaAfectada]    SYSNAME              NOT NULL,
    [operacion]        CHAR(1)              NOT NULL,
    [claveRegistro]    NVARCHAR(100)        NOT NULL,
    [datosAntes]       NVARCHAR(MAX)        NULL,
    [datosDespues]     NVARCHAR(MAX)        NULL
    CONSTRAINT PK_Auditoria
        PRIMARY KEY (idAuditoria),
    CONSTRAINT CK_Auditoria_operacion
        CHECK (operacion IN ('I', 'U', 'D'))
);
GO

CREATE TRIGGER TR_Personas_Auditoria ON Personas
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (tablaAfectada,operacion,claveRegistro,datosAntes,datosDespues)
    SELECT
        'Personas',

        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,

        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE
            WHEN d.id IS NOT NULL THEN
                CONCAT(
                    'id: ', d.id,
                    ' | cedula: ', d.cedula,
                    ' | nombre: ', d.nombre,
                    ' | apellido: ', d.apellido,
                    ' | numTelefono: ', d.numeroTelefono
                )
        END,

        CASE
            WHEN i.id IS NOT NULL THEN
                CONCAT(
                    'id: ', i.id,
                    ' | cedula: ', i.cedula,
                    ' | nombre: ', i.nombre,
                    ' | apellido: ', i.apellido,
                    ' | numTelefono: ', i.numeroTelefono
                )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d	
        ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_EstadoCreditos_Auditoria
ON EstadoCreditos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'EstadoCreditos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT('id: ', d.id,
                   ' | estado: ', d.estado)
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT('id: ', i.id,
                   ' | estado: ', i.estado)
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

 CREATE TRIGGER TR_Cargos_Auditoria
ON Cargos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Cargos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT('id: ', d.id,
                   ' | nombre: ', d.nombre)
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT('id: ', i.id,
                   ' | nombre: ', i.nombre)
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_TiposDePagos_Auditoria
ON TiposDePagos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'TiposDePagos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT('id: ', d.id,
                   ' | metodo: ', d.metodo)
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT('id: ', i.id,
                   ' | metodo: ', i.metodo)
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

DELETE FROM TiposDePagos WHERE id=1
GO

CREATE TRIGGER TR_Tipos_Auditoria
ON Tipos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Tipos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT('id: ', d.id,
                   ' | nombre: ', d.nombre)
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT('id: ', i.id,
                   ' | nombre: ', i.nombre)
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

DELETE FROM Tipos WHERE id=2
GO

CREATE TRIGGER TR_Vehiculos_Auditoria
ON Vehiculos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Vehiculos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | matricula: ', d.matricula,
                ' | marca: ', d.marca,
                ' | numeroDeChasis: ', d.numeroDeChasis,
                ' | numeroMotor: ', d.numeroMotor
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | matricula: ', i.matricula,
                ' | marca: ', i.marca,
                ' | numeroDeChasis: ', i.numeroDeChasis,
                ' | numeroMotor: ', i.numeroMotor
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Proveedores_Auditoria
ON Proveedores
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Proveedores',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | nombre: ', d.nombre,
                ' | numeroDeTelefono: ', d.numeroDeTelefono,
                ' | direccion: ', d.direccion,
                ' | email: ', d.email
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | nombre: ', i.nombre,
                ' | numeroDeTelefono: ', i.numeroDeTelefono,
                ' | direccion: ', i.direccion,
                ' | email: ', i.email
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Clientes_Auditoria
ON Clientes
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Clientes',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_persona: ', d.id_persona
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_persona: ', i.id_persona
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Salarios_Auditoria
ON Salarios
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Salarios',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_cargo: ', d.id_cargo,
                ' | id_tiposDePago: ', d.id_tiposDePago,
                ' | valorHora: ', d.valorHora,
                ' | horasTrabajadas: ', d.horasTrabajadas
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_cargo: ', i.id_cargo,
                ' | id_tiposDePago: ', i.id_tiposDePago,
                ' | valorHora: ', i.valorHora,
                ' | horasTrabajadas: ', i.horasTrabajadas
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Productos_Auditoria
ON Productos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Productos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_tipo: ', d.id_tipo,
                ' | codigo: ', d.codigo,
                ' | cantidad: ', d.cantidad,
                ' | nombre: ', d.nombre,
                ' | valor: ', d.valor
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_tipo: ', i.id_tipo,
                ' | codigo: ', i.codigo,
                ' | cantidad: ', i.cantidad,
                ' | nombre: ', i.nombre,
                ' | valor: ', i.valor
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_DetalleVentas_Auditoria
ON DetalleVentas
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'DetalleVentas',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_producto: ', d.id_producto,
                ' | cantidad: ', d.cantidad,
                ' | iva: ', d.iva,
                ' | total: ', d.total
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_producto: ', i.id_producto,
                ' | cantidad: ', i.cantidad,
                ' | iva: ', i.iva,
                ' | total: ', i.total
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Empleados_Auditoria
ON Empleados
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Empleados',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_persona: ', d.id_persona,
                ' | id_salario: ', d.id_salario,
                ' | carnet: ', d.carnet,
                ' | genero: ', d.genero
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_persona: ', i.id_persona,
                ' | id_salario: ', i.id_salario,
                ' | carnet: ', i.carnet,
                ' | genero: ', i.genero
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Estanterias_Auditoria
ON Estanterias
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Estanterias',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_empleado: ', d.id_empleado,
                ' | id_producto: ', d.id_producto,
                ' | codigo: ', d.codigo,
                ' | stock: ', d.stock
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_empleado: ', i.id_empleado,
                ' | id_producto: ', i.id_producto,
                ' | codigo: ', i.codigo,
                ' | stock: ', i.stock
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Compras_Auditoria
ON Compras
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Compras',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_proveedor: ', d.id_proveedor,
                ' | id_empleado: ', d.id_empleado,
                ' | fechaCompra: ', d.fechaCompra,
                ' | total: ', d.total
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_proveedor: ', i.id_proveedor,
                ' | id_empleado: ', i.id_empleado,
                ' | fechaCompra: ', i.fechaCompra,
                ' | total: ', i.total
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_DetalleCompras_Auditoria
ON DetalleCompras
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'DetalleCompras',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_compra: ', d.id_compra,
                ' | id_producto: ', d.id_producto,
                ' | cantidad: ', d.cantidad,
                ' | preciosCompra: ', d.preciosCompra
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_compra: ', i.id_compra,
                ' | id_producto: ', i.id_producto,
                ' | cantidad: ', i.cantidad,
                ' | preciosCompra: ', i.preciosCompra
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Ventas_Auditoria
ON Ventas
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Ventas',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_detalleVenta: ', d.id_detalleVenta,
                ' | id_tipoDePago: ', d.id_tipoDePago,
                ' | id_empleado: ', d.id_empleado,
                ' | id_cliente: ', d.id_cliente,
                ' | iva: ', d.iva,
                ' | total: ', d.total
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_detalleVenta: ', i.id_detalleVenta,
                ' | id_tipoDePago: ', i.id_tipoDePago,
                ' | id_empleado: ', i.id_empleado,
                ' | id_cliente: ', i.id_cliente,
                ' | iva: ', i.iva,
                ' | total: ', i.total
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Creditos_Auditoria
ON Creditos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Creditos',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | idVenta: ', d.idVenta,
                ' | id_estadoCredito: ', d.id_estadoCredito,
                ' | fechaPrestamo: ', d.fechaPrestamo,
                ' | valorPrestamo: ', d.valorPrestamo
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | idVenta: ', i.idVenta,
                ' | id_estadoCredito: ', i.id_estadoCredito,
                ' | fechaPrestamo: ', i.fechaPrestamo,
                ' | valorPrestamo: ', i.valorPrestamo
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Cuotas_Auditoria
ON Cuotas
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Cuotas',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_credito: ', d.id_credito,
                ' | id_tipoDePago: ', d.id_tipoDePago,
                ' | fechaDePago: ', d.fechaDePago,
                ' | valorDeCuota: ', d.valorDeCuota,
                ' | numeroDeCuota: ', d.numeroDeCuota
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_credito: ', i.id_credito,
                ' | id_tipoDePago: ', i.id_tipoDePago,
                ' | fechaDePago: ', i.fechaDePago,
                ' | valorDeCuota: ', i.valorDeCuota,
                ' | numeroDeCuota: ', i.numeroDeCuota
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Domicilios_Auditoria
ON Domicilios
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Domicilios',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_venta: ', d.id_venta,
                ' | id_vehiculo: ', d.id_vehiculo,
                ' | numeroDeTelefono: ', d.numeroDeTelefono,
                ' | direccion: ', d.direccion
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_venta: ', i.id_venta,
                ' | id_vehiculo: ', i.id_vehiculo,
                ' | numeroDeTelefono: ', i.numeroDeTelefono,
                ' | direccion: ', i.direccion
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

CREATE TRIGGER TR_Entregas_Auditoria
ON Entregas
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        tablaAfectada, operacion, claveRegistro,
        datosAntes, datosDespues
    )
    SELECT
        'Entregas',
        CASE
            WHEN d.id IS NULL THEN 'I'
            WHEN i.id IS NULL THEN 'D'
            ELSE 'U'
        END,
        CONVERT(NVARCHAR(100), COALESCE(i.id, d.id)),

        CASE WHEN d.id IS NOT NULL THEN
            CONCAT(
                'id: ', d.id,
                ' | id_domicilio: ', d.id_domicilio,
                ' | id_empleado: ', d.id_empleado,
                ' | fechaSalida: ', d.fechaSalida,
                ' | fechaLlegada: ', d.fechaLlegada
            )
        END,

        CASE WHEN i.id IS NOT NULL THEN
            CONCAT(
                'id: ', i.id,
                ' | id_domicilio: ', i.id_domicilio,
                ' | id_empleado: ', i.id_empleado,
                ' | fechaSalida: ', i.fechaSalida,
                ' | fechaLlegada: ', i.fechaLlegada
            )
        END

    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO

SELECT *
FROM Auditoria
WHERE operacion = 'D'
ORDER BY fecha DESC;

SELECT *
FROM Auditoria
WHERE operacion = 'I'
ORDER BY fecha DESC;

SELECT *
FROM Auditoria
WHERE operacion = 'U'
ORDER BY fecha DESC;