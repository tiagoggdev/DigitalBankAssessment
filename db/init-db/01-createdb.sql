CREATE DATABASE DigitalBankDb;
GO

USE DigitalBankDb;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    FechaNacimiento DATETIME NOT NULL,
    Sexo CHAR(1) NOT NULL,
    Activo BIT NOT NULL
);
GO
