-- Script para crear las tablas necesarias para los módulos de Inventario y Reportes
-- Ejecutar en la base de datos [Clinipet-JoaquinSM]

USE [Clinipet-JoaquinSM];
GO

-- 1. Tabla de Proveedores
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proveedores]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Proveedores](
        [ProveedorID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Nombre] [nvarchar](100) NOT NULL,
        [Contacto] [nvarchar](100) NULL,
        [Telefono] [nvarchar](20) NULL,
        [Email] [nvarchar](100) NULL,
        [Direccion] [nvarchar](255) NULL
    );
END
GO

-- 2. Tabla de Categorías de Productos
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categorias]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Categorias](
        [CategoriaID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Nombre] [nvarchar](50) NOT NULL
    );
    -- Insertar categorías básicas
    INSERT INTO [dbo].[Categorias] (Nombre) VALUES ('Medicamentos'), ('Alimentos'), ('Accesorios'), ('Higiene'), ('Otros');
END
GO

-- 3. Tabla de Productos (Inventario)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Productos]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Productos](
        [ProductoID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](max) NULL,
        [CategoriaID] [int] NULL FOREIGN KEY REFERENCES [dbo].[Categorias]([CategoriaID]),
        [ProveedorID] [int] NULL FOREIGN KEY REFERENCES [dbo].[Proveedores]([ProveedorID]),
        [PrecioCompra] [decimal](18, 2) NOT NULL DEFAULT 0,
        [PrecioVenta] [decimal](18, 2) NOT NULL DEFAULT 0,
        [StockActual] [int] NOT NULL DEFAULT 0,
        [StockMinimo] [int] NOT NULL DEFAULT 5,
        [FechaCaducidad] [date] NULL,
        [Lote] [nvarchar](50) NULL
    );
END
GO

-- 4. Tabla de Movimientos de Inventario
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovimientosInventario]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[MovimientosInventario](
        [MovimientoID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ProductoID] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Productos]([ProductoID]),
        [TipoMovimiento] [nvarchar](20) NOT NULL, -- 'Entrada', 'Salida', 'Ajuste'
        [Cantidad] [int] NOT NULL,
        [FechaMovimiento] [datetime] NOT NULL DEFAULT GETDATE(),
        [Motivo] [nvarchar](255) NULL
    );
END
GO
