-- Crear la base de datos
CREATE DATABASE InsumosAPI;
GO

-- Usar la base de datos creada
USE InsumosAPI;
GO

-- Crear tabla Usuarios
CREATE TABLE Usuarios (
    id_usuario INT PRIMARY KEY IDENTITY(1,1),
    nombres NVARCHAR(MAX) NOT NULL,
	apellidos NVARCHAR(MAX) NOT NULL,
    username NVARCHAR(MAX) UNIQUE NOT NULL,
    contraseña NVARCHAR(255) NOT NULL,
    rol INT NOT NULL,
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
);
GO

-- Crear tabla Medicamentos
CREATE TABLE Medicamentos (
    id_medicamento INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
	id_laboratorio int,
    descripcion NVARCHAR(MAX),
    precio DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL CHECK (stock >= 0),
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
);
GO

-- Crear tabla Proveedores
CREATE TABLE Proveedores (
    id_proveedor INT PRIMARY KEY IDENTITY(1,1),
	identificacion NVARCHAR(13) NOT NULL, 
    nombres NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20),
    direccion NVARCHAR(255),
	razon_social NVARCHAR(20),
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
);
GO

-- Crear tabla Compras
CREATE TABLE Compras (
    id_compra INT PRIMARY KEY IDENTITY(1,1),
    id_proveedor INT NOT NULL,
    fecha_emision DATE NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
    FOREIGN KEY (id_proveedor) REFERENCES Proveedores(id_proveedor)
);
GO

-- Crear tabla Detalle_Compras
CREATE TABLE Detalle_Compras (
    id_detalle_compra INT PRIMARY KEY IDENTITY(1,1),
    id_compra INT NOT NULL,
    id_medicamento INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10, 2) NOT NULL,
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
    FOREIGN KEY (id_compra) REFERENCES Compras(id_compra),
    FOREIGN KEY (id_medicamento) REFERENCES Medicamentos(id_medicamento)
);
GO

-- Crear tabla Ventas
CREATE TABLE Ventas (
    id_venta INT PRIMARY KEY IDENTITY(1,1),
    id_vendedor INT NOT NULL,
    fecha_emision DATE NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
	iva DECIMAL(10, 2) NOT NULL,
	total DECIMAL(10, 2) NOT NULL,
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
    FOREIGN KEY (id_vendedor) REFERENCES Usuarios(id_usuario)
);
GO

-- Crear tabla Detalle_Ventas
CREATE TABLE Detalle_Ventas (
    id_detalle_venta INT PRIMARY KEY IDENTITY(1,1),
    id_venta INT NOT NULL,
    id_medicamento INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10, 2) NOT NULL,
	precio_total DECIMAL(10, 2) NOT NULL,
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
    FOREIGN KEY (id_venta) REFERENCES Ventas(id_venta),
    FOREIGN KEY (id_medicamento) REFERENCES Medicamentos(id_medicamento)
);
GO

-- Crear tabla Clientes
CREATE TABLE Clientes (
    id_cliente INT PRIMARY KEY IDENTITY(1,1),
	identificacion NVARCHAR(13) NOT NULL,
    nombre_completo NVARCHAR(MAX) NOT NULL,
    telefono NVARCHAR(20),
    direccion NVARCHAR(MAX),
	correo_electronico NVARCHAR(MAX),
	estado NVARCHAR(1) DEFAULT 'A',
	usuario_creacion NVARCHAR(MAX) NOT NULL, 
	fecha_creacion DATETIME NOT NULL, 
	usuario_modificacion NVARCHAR(MAX), 
	fecha_modificacion DATETIME, 
);
GO
