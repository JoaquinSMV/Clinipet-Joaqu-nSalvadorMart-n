-- ============================================================================
-- SCRIPT DE BASE DE DATOS PARA MEJORAS DE CLINIPET (CORREGIDO)
-- Rama: mejoras-clinipet
-- Fecha: 2026-05-06
-- ============================================================================

-- Usar la base de datos de Clinipet
USE [Clinipet-JoaquinSM];
GO

-- ============================================================================
-- TABLA: servicios
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[servicios]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[servicios] (
        [id_servicio] INT PRIMARY KEY IDENTITY(1,1),
        [nombre_servicio] NVARCHAR(100) NOT NULL,
        [descripcion] NVARCHAR(MAX),
        [precio] DECIMAL(10,2) NOT NULL,
        [categoria] NVARCHAR(50),
        [activo] BIT DEFAULT 1,
        [fecha_creacion] DATETIME DEFAULT GETDATE(),
        [fecha_actualizacion] DATETIME
    );
    
    CREATE INDEX IX_servicios_categoria ON [dbo].[servicios]([categoria]);
    CREATE INDEX IX_servicios_activo ON [dbo].[servicios]([activo]);
    
    PRINT 'Tabla servicios creada exitosamente';
END
GO

-- ============================================================================
-- TABLA: medicamentos
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[medicamentos]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[medicamentos] (
        [id_medicamento] INT PRIMARY KEY IDENTITY(1,1),
        [nombre_medicamento] NVARCHAR(100) NOT NULL,
        [descripcion] NVARCHAR(MAX),
        [cantidad_stock] INT DEFAULT 0,
        [cantidad_minima] INT DEFAULT 5,
        [precio_unitario] DECIMAL(10,2),
        [fecha_vencimiento] DATETIME,
        [proveedor] NVARCHAR(100),
        [activo] BIT DEFAULT 1,
        [fecha_creacion] DATETIME DEFAULT GETDATE(),
        [fecha_actualizacion] DATETIME
    );
    
    CREATE INDEX IX_medicamentos_activo ON [dbo].[medicamentos]([activo]);
    CREATE INDEX IX_medicamentos_fecha_vencimiento ON [dbo].[medicamentos]([fecha_vencimiento]);
    
    PRINT 'Tabla medicamentos creada exitosamente';
END
GO

-- ============================================================================
-- TABLA: movimientos_inventario
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[movimientos_inventario]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[movimientos_inventario] (
        [id_movimiento] INT PRIMARY KEY IDENTITY(1,1),
        [id_medicamento] INT NOT NULL,
        [cantidad] INT NOT NULL,
        [motivo] NVARCHAR(100),
        [fecha_movimiento] DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_movimientos_medicamentos FOREIGN KEY ([id_medicamento]) 
            REFERENCES [dbo].[medicamentos]([id_medicamento])
    );
    
    PRINT 'Tabla movimientos_inventario creada exitosamente';
END
GO

-- ============================================================================
-- ALTERACIONES A TABLA: citas
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[citas]') AND name = 'diagnostico')
    ALTER TABLE [dbo].[citas] ADD [diagnostico] NVARCHAR(MAX);
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[citas]') AND name = 'tratamiento')
    ALTER TABLE [dbo].[citas] ADD [tratamiento] NVARCHAR(MAX);
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[citas]') AND name = 'medicamentos')
    ALTER TABLE [dbo].[citas] ADD [medicamentos] NVARCHAR(MAX);
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[citas]') AND name = 'observaciones')
    ALTER TABLE [dbo].[citas] ADD [observaciones] NVARCHAR(MAX);
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[citas]') AND name = 'id_servicio')
BEGIN
    ALTER TABLE [dbo].[citas] ADD [id_servicio] INT;
    ALTER TABLE [dbo].[citas] ADD CONSTRAINT FK_citas_servicios 
        FOREIGN KEY ([id_servicio]) REFERENCES [dbo].[servicios]([id_servicio]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[citas]') AND name = 'fecha_actualizacion')
    ALTER TABLE [dbo].[citas] ADD [fecha_actualizacion] DATETIME;
GO

-- ============================================================================
-- VISTAS (USANDO SQL DINÁMICO PARA EVITAR ERRORES DE LOTE)
-- ============================================================================

-- Vista: Medicamentos con stock bajo
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_MedicamentosStockBajo]'))
BEGIN
    EXEC('CREATE VIEW [dbo].[vw_MedicamentosStockBajo] AS
    SELECT 
        [id_medicamento],
        [nombre_medicamento],
        [cantidad_stock],
        [cantidad_minima],
        [proveedor],
        ([cantidad_minima] - [cantidad_stock]) AS [cantidad_faltante]
    FROM [dbo].[medicamentos]
    WHERE [activo] = 1 AND [cantidad_stock] <= [cantidad_minima]')
    PRINT 'Vista vw_MedicamentosStockBajo creada';
END
GO

-- Vista: Medicamentos próximos a vencer
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_MedicamentosProximosAVencer]'))
BEGIN
    EXEC('CREATE VIEW [dbo].[vw_MedicamentosProximosAVencer] AS
    SELECT 
        [id_medicamento],
        [nombre_medicamento],
        [cantidad_stock],
        [fecha_vencimiento],
        DATEDIFF(DAY, GETDATE(), [fecha_vencimiento]) AS [dias_para_vencer],
        [proveedor]
    FROM [dbo].[medicamentos]
    WHERE [activo] = 1 AND [fecha_vencimiento] IS NOT NULL
        AND [fecha_vencimiento] > GETDATE()')
    PRINT 'Vista vw_MedicamentosProximosAVencer creada';
END
GO

-- ============================================================================
-- DATOS DE EJEMPLO
-- ============================================================================

-- Servicios
IF NOT EXISTS (SELECT * FROM [dbo].[servicios] WHERE [nombre_servicio] = 'Consulta General')
BEGIN
    INSERT INTO [dbo].[servicios] ([nombre_servicio], [descripcion], [precio], [categoria])
    VALUES 
        ('Consulta General', 'Revisión y diagnóstico general', 50.00, 'Consulta'),
        ('Consulta de Seguimiento', 'Revisión de mascota en tratamiento', 35.00, 'Consulta'),
        ('Vacunación', 'Aplicación de vacunas', 40.00, 'Vacunación'),
        ('Desparasitación', 'Tratamiento antiparasitario', 30.00, 'Tratamiento'),
        ('Limpieza Dental', 'Limpieza y pulido de dientes', 150.00, 'Odontología'),
        ('Extracción Dental', 'Extracción de pieza dental', 200.00, 'Odontología'),
        ('Cirugía Menor', 'Procedimiento quirúrgico menor', 300.00, 'Cirugía'),
        ('Cirugía Mayor', 'Procedimiento quirúrgico mayor', 600.00, 'Cirugía'),
        ('Baño y Corte', 'Aseo y corte de pelo', 80.00, 'Aseo'),
        ('Análisis de Sangre', 'Examen de laboratorio', 120.00, 'Laboratorio'),
        ('Radiografía', 'Estudio radiológico', 180.00, 'Laboratorio'),
        ('Ecografía', 'Estudio ecográfico', 220.00, 'Laboratorio');
END
GO

-- Medicamentos
IF NOT EXISTS (SELECT * FROM [dbo].[medicamentos] WHERE [nombre_medicamento] = 'Amoxicilina 500mg')
BEGIN
    INSERT INTO [dbo].[medicamentos] 
    ([nombre_medicamento], [descripcion], [cantidad_stock], [cantidad_minima], [precio_unitario], 
     [fecha_vencimiento], [proveedor])
    VALUES 
        ('Amoxicilina 500mg', 'Antibiótico de amplio espectro', 100, 20, 2.50, '2026-12-31', 'Laboratorio XYZ'),
        ('Metronidazol 250mg', 'Antiparasitario y antibacteriano', 80, 15, 1.80, '2026-11-30', 'Laboratorio ABC'),
        ('Suero Fisiológico', 'Solución salina estéril', 50, 10, 0.50, '2026-08-15', 'Farmacéutica DEF'),
        ('Anestésico Local', 'Lidocaína 2%', 30, 5, 5.00, '2026-10-20', 'Laboratorio GHI'),
        ('Vitaminas Complejas', 'Complejo vitamínico inyectable', 40, 8, 3.50, '2026-09-30', 'Laboratorio JKL'),
        ('Antiinflamatorio', 'Meloxicam 5mg/ml', 25, 5, 4.20, '2026-07-15', 'Farmacéutica MNO');
END
GO

PRINT '=== SCRIPT CORREGIDO Y EJECUTADO CON ÉXITO ===';
