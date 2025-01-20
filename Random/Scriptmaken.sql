CREATE TABLE `SOORT` (
    Sid INT AUTO_INCREMENT PRIMARY KEY,
    Soort VARCHAR(255),
    Voorkomen VARCHAR(255)
);

CREATE TABLE `WETENSCHAPPELIJKENAAM` (
    WNid INT AUTO_INCREMENT PRIMARY KEY,
    Naam VARCHAR(255),
    WetenschappelijkeNaam VARCHAR(255)
);

CREATE TABLE `LOCATIE` (
    Lid INT AUTO_INCREMENT PRIMARY KEY,
    Locatienaam VARCHAR(255),
    provincie VARCHAR(255),
    breedtegraad DOUBLE,
    lengtegraad DOUBLE
);

CREATE TABLE `GEBRUIKER` (
    Weergavenaam VARCHAR(255),
    naam VARCHAR(255),
    email VARCHAR(255) PRIMARY KEY,
    biografie TEXT,
    taal VARCHAR(50),
    geslacht VARCHAR(50),
    geboortejaar INT,
    telefoonnummer VARCHAR(50),
    land VARCHAR(100)
);

CREATE TABLE `WAARNEMING` (
    Wid INT AUTO_INCREMENT PRIMARY KEY,
    Omschrijving TEXT,
    Sid INT,
    Datum DATE,
    Tijd TIME,
    WNid INT,
    Lid INT,
    toelichting TEXT,
    aantal INT,
    geslacht VARCHAR(50),
    gebruiker VARCHAR(255),
    zekerheid VARCHAR(50),
    Webid INT,
    ManierDelen VARCHAR(100),
    FOREIGN KEY (Sid) REFERENCES `SOORT`(Sid),
    FOREIGN KEY (WNid) REFERENCES `WETENSCHAPPELIJKENAAM`(WNid),
    FOREIGN KEY (Lid) REFERENCES `LOCATIE`(Lid),
    FOREIGN KEY (gebruiker) REFERENCES `GEBRUIKER`(email)
);

CREATE TABLE `FOTO` (
    Fid INT AUTO_INCREMENT PRIMARY KEY,
    Foto BLOB
);

CREATE TABLE `FOTOWAARNEMING` (
    Wid INT,
    Fid INT,
    FOREIGN KEY (Wid) REFERENCES `WAARNEMING`(Wid),
    FOREIGN KEY (Fid) REFERENCES `FOTO`(Fid)
);

CREATE TABLE `GELUID` (
    Gid INT AUTO_INCREMENT PRIMARY KEY,
    Geluid BLOB
);

CREATE TABLE `GELUIDWAARNEMING` (
    Wid INT,
    Gid INT,
    FOREIGN KEY (Wid) REFERENCES `WAARNEMING`(Wid),
    FOREIGN KEY (Gid) REFERENCES `GELUID`(Gid)
);

CREATE TABLE `WEBLINK` (
    Webid INT AUTO_INCREMENT PRIMARY KEY,
    Weblink VARCHAR(255)
);

CREATE TABLE `WEBLINKWAARNEMING` (
    Wid INT,
    Webid INT,
    FOREIGN KEY (Wid) REFERENCES `WAARNEMING`(Wid),
    FOREIGN KEY (Webid) REFERENCES `WEBLINK`(Webid)
);
