-- Script de base de datos para el Sistema de Ventas (TechStore)
-- Ejecutar este script completo en MySQL (Workbench, consola, etc.)
-- antes de correr la aplicacion.

CREATE DATABASE IF NOT EXISTS pos_techstore;
USE pos_techstore;

CREATE TABLE IF NOT EXISTS clientes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS productos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    codigo VARCHAR(20) NOT NULL UNIQUE,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS venta_cabecera (
    id INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATETIME NOT NULL,
    cliente_id INT NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE TABLE IF NOT EXISTS venta_detalle (
    id INT AUTO_INCREMENT PRIMARY KEY,
    venta_id INT NOT NULL,
    producto_id INT NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (venta_id) REFERENCES venta_cabecera(id),
    FOREIGN KEY (producto_id) REFERENCES productos(id)
);

-- Datos de ejemplo
INSERT INTO clientes (nombre, telefono, correo) VALUES
('Cliente Mostrador', '0000-0000', 'mostrador@correo.com');

INSERT INTO productos (codigo, nombre, descripcion, precio, stock) VALUES
('T001', 'Laptop Core i5', 'Laptop 15.6" Core i5 8GB RAM', 650.00, 15),
('T002', 'Mouse Inalambrico', 'Mouse inalambrico optico', 12.50, 40),
('T003', 'Teclado Mecanico', 'Teclado mecanico RGB', 35.00, 25),
('T004', 'Celular Galaxy A15', 'Smartphone 128GB 4G', 180.00, 8),
('T005', 'Monitor 24 pulgadas', 'Monitor LED Full HD', 110.00, 5);
