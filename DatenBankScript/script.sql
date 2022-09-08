USE Bug3;
GO
CREATE TABLE Mitarbeiter(
    MitarbeiterID INTEGER IDENTITY(1,1) NOT NULL,
    Vorname VARCHAR(100) NOT NULL,
    Nachname VARCHAR(100) NOT NULL,
    Bereich VARCHAR(100) NOT NULL,
    CONSTRAINT PK_MitarbeiterID PRIMARY KEY (MitarbeiterID),
    CONSTRAINT CK_Bereich CHECK (Bereich IN ('Tester', 'Entwickler'))
);

CREATE TABLE Projekt(
    ProjektID INTEGER IDENTITY(1,1) NOT NULL,
    ProjektName VARCHAR(100) NOT NULL,
    StartDatum DATE NOT NULL,
    EndDatum DATE,
    CONSTRAINT PK_ProjektID PRIMARY KEY (ProjektID)
);

CREATE TABLE Bug(
    BugID INTEGER IDENTITY(1,1) NOT NULL,
    Titel VARCHAR(100) NOT NULL,
    Beschreibung VARCHAR(max) NOT NULL,
    ErfassungDatum DATE NOT NULL,
    BehebungsDatum DATE,
    TesterID INTEGER ,
    EntwicklerID INTEGER,
    ProjektID INTEGER ,
    CONSTRAINT PK_BugID PRIMARY KEY (BugID),
    CONSTRAINT FK_TesterID FOREIGN KEY (TesterID) REFERENCES Mitarbeiter(MitarbeiterID),
    CONSTRAINT FK_EntwicklerID FOREIGN KEY (EntwicklerID) REFERENCES Mitarbeiter(MitarbeiterID),
    CONSTRAINT FK_ProjektID FOREIGN KEY (ProjektID) REFERENCES Projekt(ProjektID) 
);


USE Bug3;
INSERT INTO Mitarbeiter(Vorname, Nachname, Bereich)
VALUES ('Mohamed', 'Ali', 'Entwickler');
USE Bug3;
INSERT INTO Mitarbeiter( Vorname, Nachname, Bereich)
VALUES ('Maha', 'Alakhras', 'Tester');
INSERT INTO Mitarbeiter( Vorname, Nachname, Bereich)
VALUES ('Bogdan', 'Ghinea', 'Tester');
INSERT INTO Mitarbeiter( Vorname, Nachname, Bereich)
VALUES ('Dada', 'Dada', 'Tester');
USE Bug3;
INSERT INTO Projekt (ProjektName,Startdatum)
VALUES ('DHL','2022-09-05');
INSERT INTO Bug (Titel,Beschreibung,ErfassungDatum,BehebungsDatum,TesterID)
VALUES ('css','blalalala','2022-09-10','2022-09-20',2);
INSERT INTO Bug (Titel,Beschreibung,ErfassungDatum,BehebungsDatum,TesterID)
VALUES ('js','blalalala','2022-09-10','2022-09-20',3);
INSERT INTO Bug (Titel,Beschreibung,ErfassungDatum,BehebungsDatum,TesterID)
VALUES ('html','blalalala','2022-09-10','2022-09-20',4);
INSERT INTO Bug (Titel,Beschreibung,ErfassungDatum,BehebungsDatum,EntwicklerID)
VALUES ('Sql','blalalala','2022-09-10','2022-09-20',1);
