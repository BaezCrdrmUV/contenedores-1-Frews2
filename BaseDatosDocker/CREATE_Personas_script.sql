SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

CREATE DATABASE IF NOT EXISTS Personas;
USE Personas;
CREATE TABLE IF NOT EXISTS Personas (
	idPersona INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nombres VARCHAR(255) NOT NULL,
	apellidos VARCHAR(255) NOT NULL,
	genero VARCHAR(255) NOT NULL,
	edad INT NOT NULL)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS Direcciones(
	idDireccion INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	numero_Direccion INT NOT NULL,
	nombre_Direccion VARCHAR(255) NOT NULL,
	ciudad VARCHAR(255) NOT NULL,
	estado VARCHAR(255) NOT NULL,
	pais VARCHAR(255) NOT NULL,
	idHabitante int NOT NULL,
	CONSTRAINT fk_DireccionPersona
	FOREIGN KEY (idHabitante)
	REFERENCES Personas(idPersona)
	ON DELETE CASCADE
    	ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS Telefonos(
	idTelefono int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	numero_Telefono VARCHAR(10) NOT NULL,
	idDuenio INT NOT NULL,
	CONSTRAINT fk_TelefonoPersona
	FOREIGN KEY (idDuenio)
	REFERENCES Personas(idPersona)
	ON DELETE CASCADE
    	ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE USER 'usuario' IDENTIFIED BY 'PASS333';
GRANT ALL PRIVILEGES ON *.* TO 'usuario'@'%' WITH GRANT OPTION;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

START TRANSACTION;
USE Personas;
INSERT INTO Personas (nombres, apellidos, genero, edad) VALUES ('Ricardo','Moguel Sanchez','Masculino',22);
INSERT INTO Personas (nombres, apellidos, genero, edad) VALUES ('Juan Gabriel','Torres Reyes','Masculino',19);
INSERT INTO Personas (nombres, apellidos, genero, edad) VALUES ('Sophia Anna','Martinez Mendoza','Masculino',22);
COMMIT;


START TRANSACTION;
USE Personas;
INSERT INTO Direcciones(numero_Direccion, nombre_Direccion, ciudad, estado, pais, idHabitante) VALUES (11,'Avenida 7','Cordoba','Veracruz','Mexico',1);
INSERT INTO Direcciones(numero_Direccion, nombre_Direccion, ciudad, estado, pais, idHabitante) VALUES (123,'Maple Street','Austin','Texas','USA',2);
INSERT INTO Direcciones(numero_Direccion, nombre_Direccion, ciudad, estado, pais, idHabitante) VALUES (24,'Avenida Insurgentes','Chetumal','Quintana Roo','Mexico',3);
COMMIT;

START TRANSACTION;
USE Personas;
INSERT INTO Telefonos (numero_Telefono, idDuenio) VALUES ('2284758489',1);
INSERT INTO Telefonos (numero_Telefono, idDuenio) VALUES ('1004585254',2);
INSERT INTO Telefonos (numero_Telefono, idDuenio) VALUES ('2107721225',3);
COMMIT;
