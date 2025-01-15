--SqLite
CREATE TABLE InheemseSoort (
    Naam TEXT NOT NULL,
    LocatieNaam TEXT NOT NULL,
    Longitude DECIMAL(10, 7) NOT NULL,
    Latitude DECIMAL(10, 7) NOT NULL,
    Datum DATETIME NOT NULL
);