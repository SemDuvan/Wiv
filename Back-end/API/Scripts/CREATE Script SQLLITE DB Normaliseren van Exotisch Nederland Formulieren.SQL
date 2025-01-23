--1
CREATE TABLE SOORT (
    Sid INTEGER PRIMARY KEY,
    Soort TEXT,
    Voorkomen TEXT
);

--2
CREATE TABLE WETENSCHAPPELIJKENAAM (
    WNid INTEGER PRIMARY KEY,
    Naam TEXT,
    WetenschappelijkeNaam TEXT
);

--3
CREATE TABLE LOCATIE (
    Lid INTEGER PRIMARY KEY,
    Locatienaam TEXT,
    provincie TEXT,
    breedtegraad REAL,
    lengtegraad REAL
);

--4
CREATE TABLE GEBRUIKER (
    Weergavenaam TEXT,
    naam TEXT,
    email TEXT PRIMARY KEY,
    biografie TEXT,
    taal TEXT,
    geslacht TEXT,
    geboortejaar INTEGER,
    telefoonnummer TEXT,
    land TEXT
);

--5
CREATE TABLE WAARNEMING (
    Wid INTEGER PRIMARY KEY,
    Omschrijving TEXT,
    Sid INTEGER,
    Datum TEXT,
    Tijd TEXT,
    WNid INTEGER,
    Lid INTEGER,
    toelichting TEXT,
    aantal INTEGER,
    geslacht TEXT,
    gebruiker TEXT,
    zekerheid TEXT,
    Webid INTEGER,
    ManierDelen TEXT,
    FOREIGN KEY (Sid) REFERENCES SOORT(Sid),
    FOREIGN KEY (WNid) REFERENCES WETENSCHAPPELIJKENAAM(WNid),
    FOREIGN KEY (Lid) REFERENCES LOCATIE(Lid),
    FOREIGN KEY (gebruiker) REFERENCES GEBRUIKER(email)
);

--6
CREATE TABLE FOTO (
    Fid INTEGER PRIMARY KEY,
    Foto BLOB
);

--7
CREATE TABLE FOTOWAARNEMING (
    Wid INTEGER,
    Fid INTEGER,
    FOREIGN KEY (Wid) REFERENCES WAARNEMING(Wid),
    FOREIGN KEY (Fid) REFERENCES FOTO(Fid)
);

--8
CREATE TABLE GELUID (
    Gid INTEGER PRIMARY KEY,
    Geluid BLOB
);

--9
CREATE TABLE GELUIDWAARNEMING (
    Wid INTEGER,
    Gid INTEGER,
    FOREIGN KEY (Wid) REFERENCES WAARNEMING(Wid),
    FOREIGN KEY (Gid) REFERENCES GELUID(Gid)
);

--10
CREATE TABLE WEBLINK (
    Webid INTEGER PRIMARY KEY,
    Weblink TEXT
);

--11
CREATE TABLE WEBLINKWAARNEMING (
    Wid INTEGER,
    Webid INTEGER,
    FOREIGN KEY (Wid) REFERENCES WAARNEMING(Wid),
    FOREIGN KEY (Webid) REFERENCES WEBLINK(Webid)
);