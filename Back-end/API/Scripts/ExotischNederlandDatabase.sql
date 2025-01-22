--SqLite
CREATE TABLE InheemseSoort (
    Naam TEXT NOT NULL,
    LocatieNaam TEXT NOT NULL,
    Longitude DECIMAL(10, 7) NOT NULL,
    Latitude DECIMAL(10, 7) NOT NULL,
    Datum DATETIME NOT NULL
);


--TEST
CREATE TABLE SOORT (
    Soort TEXT,
    Voorkomen TEXT
);

INSERT INTO SOORT (Soort, Voorkomen) VALUES
('Vogel', 'Algemeen'),
('Zoogdier', 'Zeldzaam'),
('Amfibie', 'Algemeen'),
('Insect', 'Algemeen'),
('Vis', 'Zeldzaam'),
('Reptiel', 'Zeldzaam'),
('Vogel', 'Algemeen'),
('Zoogdier', 'Algemeen'),
('Amfibie', 'Zeldzaam'),
('Insect', 'Zeldzaam'),
('Vis', 'Algemeen'),
('Reptiel', 'Algemeen'),
('Vogel', 'Zeldzaam'),
('Zoogdier', 'Algemeen'),
('Amfibie', 'Algemeen'),
('Insect', 'Algemeen'),
('Vis', 'Zeldzaam'),
('Reptiel', 'Zeldzaam'),
('Vogel', 'Algemeen'),
('Zoogdier', 'Zeldzaam'),
('Amfibie', 'Algemeen'),
('Insect', 'Algemeen'),
('Vis', 'Zeldzaam'),
('Reptiel', 'Zeldzaam'),
('Vogel', 'Algemeen'),
('Zoogdier', 'Algemeen'),
('Amfibie', 'Zeldzaam'),
('Insect', 'Zeldzaam'),
('Vis', 'Algemeen'),
('Reptiel', 'Zeldzaam');