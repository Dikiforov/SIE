CREATE TABLE PERSONA
(
	ID INT NOT NULL IDENTITY (1,1),
	NOMBRE VARCHAR(50),
	APELLIDO VARCHAR(50),
	PRIMARY KEY (ID),
)

CREATE TABLE COCHES
(
	ID INT NOT NULL IDENTITY (1,1),
	MARCA VARCHAR(50),
	MODELO VARCHAR(50),
	VIN VARCHAR(50),
	PRIMARY KEY (ID)
)

CREATE TABLE PERSONA_COCHE
(
	ID INT NOT NULL IDENTITY (1,1),
	ID_PERSONA INT NOT NULL,
	ID_COCHE INT NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (ID_PERSONA) REFERENCES PERSONA(ID),
	FOREIGN KEY (ID_COCHE) REFERENCES COCHES(ID)
)

GO