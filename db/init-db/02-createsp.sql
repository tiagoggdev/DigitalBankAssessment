USE DigitalBankDb;
GO
-- =============================================
-- Author:		Santiago Garcia
-- Create date: 03/10/2025
-- Description:	Sp para CRUD de usuarios
-- =============================================

CREATE PROCEDURE sp_CreateUsuario
    @Nombre NVARCHAR(100),
    @FechaNacimiento DATE,
    @Sexo CHAR(1)
AS
BEGIN
    INSERT INTO Usuarios (Nombre, FechaNacimiento, Sexo, Activo)
    VALUES (@Nombre, @FechaNacimiento, @Sexo, 1);

    SELECT SCOPE_IDENTITY() AS NewId;
END
GO

CREATE PROCEDURE sp_DeleteUsuario
	@Id INT
AS
BEGIN
	UPDATE Usuarios
	SET Activo = 0
	WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_EditUsuario
	@Id INT,
    @Nombre NVARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL,
    @Sexo CHAR(1) = NULL
AS
BEGIN
    UPDATE Usuarios
	SET Nombre = ISNULL(@Nombre, Nombre),
		FechaNacimiento = ISNULL(@FechaNacimiento, FechaNacimiento), 
		Sexo = ISNULL(@Sexo, Sexo)
	WHERE Id = @Id;

END
GO

CREATE PROCEDURE sp_GetAllUsuarios
AS
BEGIN
	SELECT * FROM Usuarios WHERE Activo = 1;
END
GO