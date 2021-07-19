-- DROP SEQUENCES
DROP SEQUENCE JUDETE_codJudet_SEQ;
DROP SEQUENCE LOCALITATI_idLocalitate_SEQ;
DROP SEQUENCE LOCALITATI_codPostal_SEQ;
DROP SEQUENCE SPITALE_idSpital_SEQ;
DROP SEQUENCE SECTII_codSectie_SEQ;
DROP SEQUENCE SECTII_SPITALE_idSectie_SEQ;
DROP SEQUENCE FUNCTII_codFunctie_SEQ;
DROP SEQUENCE DOCTORI_codParafa_SEQ;
DROP SEQUENCE PACIENTI_idPacient_SEQ;
DROP SEQUENCE CONSULTATII_idConsultatie_SEQ;
DROP SEQUENCE BOLI_codBoala_SEQ;
DROP SEQUENCE DIAGNOSTICE_idDiagnostic_SEQ;
DROP SEQUENCE SIMPTOME_codSimptom_SEQ;
DROP SEQUENCE TRATAMENTE_idTratament_SEQ;
DROP SEQUENCE MEDICAMENTE_idMedicament_SEQ;

-- DROP TRIGGERS
DROP TRIGGER CHECK_dataInternare;
DROP TRIGGER CHECK_Internare;
DROP TRIGGER CHECK_dataExternare;
DROP TRIGGER CHECK_dataConsultatie;

-- DROP TABLES
ALTER TABLE TRATAMENTE_ADMINISTRATE 
    DROP CONSTRAINT TRAT_ADM_idTratament_FK
    DROP CONSTRAINT TRAT_ADM_idMedicament_FK;
DROP TABLE TRATAMENTE_ADMINISTRATE ;

DROP TABLE MEDICAMENTE;

ALTER TABLE TRATAMENTE
    DROP CONSTRAINT TRATAMENTE_codBoala_FK;
DROP TABLE TRATAMENTE;

ALTER TABLE DIAGNOSTICE_SIMPTOME
    DROP CONSTRAINT DIAG_SIMP_codSimptom_FK
    DROP CONSTRAINT DIAG_SIMP_idDiagnostic_FK;
DROP TABLE DIAGNOSTICE_SIMPTOME;

DROP TABLE SIMPTOME;

ALTER TABLE DIAGNOSTICE
    DROP CONSTRAINT DIAGNOSTICE_codBoala_FK
    DROP CONSTRAINT DIAGNOSTICE_idConsultatie_FK;
DROP TABLE DIAGNOSTICE;

DROP TABLE BOLI;

ALTER TABLE CONSULTATII
    DROP CONSTRAINT CONSULTATII_idPacient_FK
    DROP CONSTRAINT CONSULTATII_codParafa_FK;
DROP TABLE CONSULTATII;

DROP TABLE PACIENTI;

ALTER TABLE DOCTORI
    DROP CONSTRAINT DOCTORI_idSectie_FK
    DROP CONSTRAINT DOCTORI_codFunctie_FK;
DROP TABLE DOCTORI;

DROP TABLE FUNCTII;

ALTER TABLE SECTII_SPITALE
DROP CONSTRAINT SECTII_SPITALE_idSpital_FK
DROP CONSTRAINT SECTII_SPITALE_codSectie_FK;
DROP TABLE SECTII_SPITALE;

DROP TABLE SECTII;

ALTER TABLE SPITALE
    DROP CONSTRAINT SPITALE_idLocalitate_FK;
DROP TABLE SPITALE;

ALTER TABLE LOCALITATI
    DROP CONSTRAINT LOCALITATI_codJudet_FK;
    
DROP TABLE LOCALITATI;
DROP TABLE JUDETE;

-- CREATE TABLE
-- CREAREA TABELULUI JUDETE
CREATE SEQUENCE JUDETE_codJudet_SEQ MAXVALUE 42 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE JUDETE (
    codJudet NUMBER(2, 0)
        CONSTRAINT JUDETE_codJudet_PK PRIMARY KEY,
        
    numeJudet VARCHAR2(30 CHAR)
        CONSTRAINT JUDETE_numeJudet_NN NOT NULL
        CONSTRAINT JUDETE_numeJudet_U UNIQUE
);

-- CREAREA TABELULUI LOCALITATI
CREATE SEQUENCE LOCALITATI_idLocalitate_SEQ MAXVALUE 99999 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE SEQUENCE LOCALITATI_codPostal_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 100000 NOCACHE ORDER NOCYCLE;
CREATE TABLE LOCALITATI (
    idLocalitate NUMBER(5, 0)
        CONSTRAINT LOCALITATI_idLocalitate_PK PRIMARY KEY,
        
    numeLocalitate VARCHAR2(30 CHAR)
        CONSTRAINT LOCALITATI_numeLocalitate_NN NOT NULL,
        
    codPostal NUMBER(6, 0)
        CONSTRAINT LOCALITATI_codPostal_NN NOT NULL
        CONSTRAINT LOCALITATI_codPostal_U UNIQUE,
        
    codJudet NUMBER(2, 0)
        CONSTRAINT LOCALITATI_codJudet_FK REFERENCES JUDETE(codJudet) ON DELETE CASCADE
        CONSTRAINT LOCALITATI_codJudet_NN NOT NULL
);

-- CREAREA TABELULUI SPITALE
CREATE SEQUENCE SPITALE_idSpital_SEQ MAXVALUE 99999 INCREMENT BY 1 START WITH 10001 NOCACHE ORDER NOCYCLE;
CREATE TABLE SPITALE (
    idSpital NUMBER(5, 0)
        CONSTRAINT SPITALE_idSpital_PK PRIMARY KEY, 
        
    numeSpital VARCHAR2(50 CHAR)
        CONSTRAINT SPITALE_numeSpital_NN NOT NULL,
        
    idLocalitate NUMBER(5, 0)
        CONSTRAINT SPITALE_idLocalitate_FK REFERENCES LOCALITATI(idLocalitate) ON DELETE CASCADE
        CONSTRAINT SPITALE_idLocalitate_NN NOT NULL,
        
    strada VARCHAR2(30 CHAR)
        CONSTRAINT SPITALE_strada_NN NOT NULL,
        
    numar NUMBER(3, 0)
        CONSTRAINT SPITALE_numar_NN NOT NULL,
        
    telefon VARCHAR2(10 CHAR)
        CONSTRAINT SPITALE_telefon_NN NOT NULL
        CONSTRAINT SPITALE_telefon_U UNIQUE
        CONSTRAINT SPITALE_telefon_C CHECK (LENGTH(telefon)=10),
        
    email VARCHAR2(50 CHAR)
        CONSTRAINT SPITALE_email_NN NOT NULL
        CONSTRAINT SPITALE_email_U UNIQUE
        CONSTRAINT SPITALE_email_C CHECK (REGEXP_LIKE(email, '[[:alnum:]]+@[[:alnum:]]+\.[[:alnum:]]'))
);

-- CREAREA TABELULUI SECTII
CREATE SEQUENCE SECTII_codSectie_SEQ MAXVALUE 100 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE SECTII (
    codSectie   NUMBER(3, 0)
        CONSTRAINT SECTII_codSectie_PK PRIMARY KEY, 
        
    numeSectie  VARCHAR2(40 CHAR)
        CONSTRAINT SECTII_numeSectie_NN NOT NULL
        CONSTRAINT SECTII_numeSectie_U UNIQUE
);

-- CREAREA TABELULUI SECTII_SPITALE
CREATE SEQUENCE SECTII_SPITALE_idSectie_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 100001 NOCACHE ORDER NOCYCLE;
CREATE TABLE SECTII_SPITALE (
    idSectie NUMBER(6, 0)
        CONSTRAINT SECTII_SPITALE_idSectie_PK PRIMARY KEY, 
        
    idSpital NUMBER(5, 0)
        CONSTRAINT SECTII_SPITALE_idSpital_FK REFERENCES SPITALE(idSpital) ON DELETE CASCADE
        CONSTRAINT SECTII_SPITALE_idSpital_NN NOT NULL,
        
    codSectie NUMBER(3, 0)
        CONSTRAINT SECTII_SPITALE_codSectie_FK REFERENCES SECTII(codSectie) ON DELETE SET NULL,
        
    corp VARCHAR2(15 CHAR),
    
    etaj NUMBER(1, 0)
        CONSTRAINT SECTII_SPITALE_etaj_NN NOT NULL,
        
    telefon VARCHAR2(10 CHAR)
        CONSTRAINT SECTII_SPITALE_telefon_U UNIQUE
        CONSTRAINT SECTII_SPITALE_telefon_C CHECK (LENGTH(telefon)=10),
        
    email VARCHAR2(50 CHAR)
        CONSTRAINT SECTII_SPITALE_email_U UNIQUE
        CONSTRAINT SECTII_SPITALE_email_C CHECK (REGEXP_LIKE(email, '[[:alnum:]]+@[[:alnum:]]+\.[[:alnum:]]'))
);

-- CREAREA TABELULUI FUNCTII
CREATE SEQUENCE FUNCTII_codFunctie_SEQ MAXVALUE 100 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE FUNCTII (
    codFunctie NUMBER(3, 0)
        CONSTRAINT FUNCTII_codFunctie_PK PRIMARY KEY,
    
    numeFunctie VARCHAR2(30 CHAR)
        CONSTRAINT FUNCTII_numeFunctie_NN NOT NULL
        CONSTRAINT FUNCTII_numeFunctii_U UNIQUE
);

-- CREAREA TABELULUI DOCTORI
CREATE SEQUENCE DOCTORI_codParafa_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 100000 NOCACHE ORDER NOCYCLE;
CREATE TABLE DOCTORI (
    codParafa NUMBER(6, 0)
        CONSTRAINT DOCTORI_codParafa_PK PRIMARY KEY,
    
    nume VARCHAR2(30 CHAR)
        CONSTRAINT DOCTORI_nume_NN NOT NULL,
    
    prenume VARCHAR2(30 CHAR)
        CONSTRAINT DOCTORI_prenume_NN NOT NULL,
    
    CNP VARCHAR2(13 CHAR)
        CONSTRAINT DOCTORI_CNP_NN NOT NULL
        CONSTRAINT DOCTORI_CNP_U UNIQUE
        CONSTRAINT DOCTORI_CNP_C CHECK (LENGTH(CNP)=13),
    
    telefon VARCHAR2(10 CHAR)
        CONSTRAINT DOCTORI_telefon_NN NOT NULL
        CONSTRAINT DOCTORI_telefon_U UNIQUE
        CONSTRAINT DOCTORI_telefon_C CHECK (LENGTH(telefon)=10),
    
    email VARCHAR2(30 CHAR)
        CONSTRAINT DOCTORI_email_NN NOT NULL
        CONSTRAINT DOCTORI_email_U UNIQUE
        CONSTRAINT DOCTORI_email_C CHECK (REGEXP_LIKE(email, '[[:alnum:]]+@[[:alnum:]]+\.[[:alnum:]]')),
    
    codFunctie NUMBER(3, 0)
        CONSTRAINT DOCTORI_codFunctie_FK REFERENCES FUNCTII(codFunctie) ON DELETE SET NULL,
    
    idSectie NUMBER(6, 0)
        CONSTRAINT DOCTORI_idSectie_FK REFERENCES SECTII_SPITALE(idSectie) ON DELETE SET NULL
);

-- CREAREA TABELULUI PACIENTI
CREATE SEQUENCE PACIENTI_idPacient_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE PACIENTI (
    idPacient NUMBER(6, 0)
        CONSTRAINT PACIENTI_idPacient_PK PRIMARY KEY,
    
    nume VARCHAR2(30 CHAR)
        CONSTRAINT PACIENTI_nume_NN NOT NULL,
    
    prenume VARCHAR2(30 CHAR)
        CONSTRAINT PACIENTI_prenume_NN NOT NULL,
    
    CNP VARCHAR2(13 CHAR)
        CONSTRAINT PACIENTI_CNP_NN NOT NULL
        CONSTRAINT PACIENTI_CNP_C CHECK (LENGTH(CNP)=13),
    
    strada VARCHAR2(30 CHAR)
        CONSTRAINT PACIENTI_strada_NN NOT NULL,
    
    numar NUMBER(3, 0)
        CONSTRAINT PACIENTI_numar_NN NOT NULL,
    
    localitate VARCHAR2(30 CHAR)
        CONSTRAINT PACIENTI_localitate_NN NOT NULL,
    
    telefon VARCHAR2(10 CHAR)
        CONSTRAINT PACIENTI_telefon_NN NOT NULL
        CONSTRAINT PACIENTI_telefon_C CHECK (LENGTH(telefon)=10),
    
    email VARCHAR2(30 CHAR)
        CONSTRAINT PACIENTI_email_C CHECK (REGEXP_LIKE(email, '[[:alnum:]]+@[[:alnum:]]+\.[[:alnum:]]')),
    
    asigurat NUMBER(1, 0)
        CONSTRAINT PACIENTI_asigurat_NN NOT NULL
        CONSTRAINT PACIENTI_asigurat_C CHECK (asigurat=1 OR asigurat=0),
    
    dataInternare DATE
        CONSTRAINT PACIENTI_dataInternare_NN NOT NULL,
    
    dataExternare DATE
);
CREATE OR REPLACE TRIGGER CHECK_dataInternare
    BEFORE INSERT OR UPDATE ON PACIENTI
    FOR EACH ROW
BEGIN
        IF(:NEW.dataInternare < date '1900-01-01' or :NEW.dataInternare > sysdate)
        THEN 
            RAISE_APPLICATION_ERROR(-20001, 'Data internarii nu poate fi mai mica decat 01-JAN-1900 sau mai mare decat data curenta!');
        END IF;
END; 
/
CREATE OR REPLACE TRIGGER CHECK_Internare
    BEFORE INSERT ON PACIENTI
    FOR EACH ROW
DECLARE
    pacienti_CNT NUMBER;
BEGIN
        SELECT COUNT(*) INTO pacienti_CNT FROM PACIENTI WHERE (CNP=:NEW.CNP AND dataExternare IS NULL);
        IF(pacienti_CNT > 0)
        THEN
            RAISE_APPLICATION_ERROR(-20001, 'Pacientul este deja internat in spital!');
        END IF;
END; 
/
CREATE OR REPLACE TRIGGER CHECK_dataExternare
    BEFORE INSERT OR UPDATE ON PACIENTI
    FOR EACH ROW
BEGIN
    IF(:NEW.dataExternare > sysdate)
    THEN
        RAISE_APPLICATION_ERROR(-20001, 'Data externarii nu poate fi mai mare decat data curenta!');
    ELSIF(:NEW.dataExternare < :OLD.dataInternare)
    THEN
        RAISE_APPLICATION_ERROR(-20001, 'Data externarii nu poate fi mai mica decat data internarii!');
    END IF;
END;
/

CREATE SEQUENCE CONSULTATII_idConsultatie_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE CONSULTATII (
    idConsultatie NUMBER(6, 0)
        CONSTRAINT CONSULTATII_idConsultatie_PK PRIMARY KEY,
    
    codParafa NUMBER(6, 0)
        CONSTRAINT CONSULTATII_codParafa_FK REFERENCES DOCTORI(codParafa) ON DELETE SET NULL,
    
    idPacient NUMBER(6, 0)
        CONSTRAINT CONSULTATII_idPacient_FK REFERENCES PACIENTI(idPacient) ON DELETE CASCADE
        CONSTRAINT CONSULTATII_idPacient_NN NOT NULL,
    
    dataConsultatie DATE
        CONSTRAINT CONSULTATII_dataConsultatie_NN NOT NULL
);
CREATE OR REPLACE TRIGGER CHECK_dataConsultatie
    BEFORE INSERT OR UPDATE ON CONSULTATII
    FOR EACH ROW
DECLARE
    pacienti_CNT NUMBER;
    dataInt DATE;
BEGIN
    SELECT COUNT(*) INTO pacienti_CNT FROM PACIENTI;
    
    IF pacienti_CNT > 0 
    THEN
        SELECT dataInternare INTO dataInt FROM PACIENTI WHERE (idPacient=:NEW.idPacient AND dataExternare IS NULL);
        IF( :NEW.dataConsultatie > sysdate)
        THEN
            RAISE_APPLICATION_ERROR(-20001, 'Data consultatiei nu poate fi mai mare decat data curenta!');
        ELSIF(:NEW.dataConsultatie < dataInt)
        THEN
            RAISE_APPLICATION_ERROR(-20001, 'Data consultatiei nu poate fi mai mica decat data internarii!');
        END IF;
    ELSE 
        RAISE_APPLICATION_ERROR(-20001, 'Pacientul nu figureaza ca fiind internat in spital!');
    END IF;
END;
/

-- CREAREA TABELULUI BOLI
CREATE SEQUENCE BOLI_codBoala_SEQ MAXVALUE 999 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE BOLI (
    codBoala NUMBER(3, 0)
        CONSTRAINT BOLI_codBoala_PK PRIMARY KEY,
    
    denumireBoala VARCHAR2(50 CHAR)
        CONSTRAINT BOLI_denumireBoala_NN NOT NULL
        CONSTRAINT BOLI_denumireBoala_U UNIQUE
);

-- CREAREA TABELULUI DIAGNOSTICE
CREATE SEQUENCE DIAGNOSTICE_idDiagnostic_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 100000 NOCACHE ORDER NOCYCLE;
CREATE TABLE DIAGNOSTICE (
    idDiagnostic NUMBER(6, 0)
        CONSTRAINT DIAGNOSTICE_idDiagnostic_PK PRIMARY KEY,
    
    idConsultatie NUMBER(6, 0)
        CONSTRAINT DIAGNOSTICE_idConsultatie_FK REFERENCES CONSULTATII(idConsultatie) ON DELETE CASCADE
        CONSTRAINT DIAGNOSTICE_idConsultatie_NN NOT NULL,
    
    codBoala NUMBER(3, 0)
        CONSTRAINT DIAGNOSTICE_codBoala_FK REFERENCES BOLI(codBoala) ON DELETE SET NULL
);

-- CREAREA TABELULUI SIMPTOME
CREATE SEQUENCE SIMPTOME_codSimptom_SEQ MAXVALUE 999 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE SIMPTOME (
    codSimptom NUMBER(3, 0)
        CONSTRAINT SIMPTOME_codSimptom_PK PRIMARY KEY,
    
    denumireSimptom VARCHAR2(30 CHAR)
        CONSTRAINT SIMPTOME_denumireSimptom_NN NOT NULL
        CONSTRAINT SIMPTOME_denumireSimptom_U UNIQUE
);

CREATE TABLE DIAGNOSTICE_SIMPTOME (
    idDiagnostic NUMBER(6, 0)
        CONSTRAINT DIAG_SIMP_idDiagnostic_FK REFERENCES DIAGNOSTICE(idDiagnostic) ON DELETE CASCADE,
    
    codSimptom NUMBER(3, 0)
        CONSTRAINT DIAG_SIMP_codSimptom_FK REFERENCES SIMPTOME(codSimptom) ON DELETE CASCADE,
    
    CONSTRAINT DIAG_SIMP_PK PRIMARY KEY(idDiagnostic, codSimptom)
);

-- CREAREA TABELULUI TRATAMENTE
CREATE SEQUENCE TRATAMENTE_idTratament_SEQ MAXVALUE 999999 INCREMENT BY 1 START WITH 100000 NOCACHE ORDER NOCYCLE;
CREATE TABLE TRATAMENTE (
    idTratament NUMBER(6, 0)
        CONSTRAINT TRATAMENTE_idTratament_PK PRIMARY KEY,
    
    codBoala NUMBER(3, 0)
        CONSTRAINT TRATAMENTE_codBoala_FK REFERENCES BOLI(codBoala) ON DELETE CASCADE
        CONSTRAINT TRATAMENTE_codBoala_NN NOT NULL,
    
    perioadaAdministrare NUMBER(3, 0)
        CONSTRAINT TRATAMENTE_perioadaAdministrare_NN NOT NULL,
    
    indicatii VARCHAR2(250 CHAR)
);

-- CREAREA TABELULUI MEDICAMENTE
CREATE SEQUENCE MEDICAMENTE_idMedicament_SEQ MAXVALUE 99999 INCREMENT BY 1 START WITH 1 NOCACHE ORDER NOCYCLE;
CREATE TABLE MEDICAMENTE (
    idMedicament NUMBER(5, 0)
        CONSTRAINT MEDICAMENTE_idMedicament_PK PRIMARY KEY,
    
    numeMedicament VARCHAR2(30 CHAR)
        CONSTRAINT MEDICAMENTE_numeMedicament_NN NOT NULL,
    
    tipMedicament VARCHAR2(30 CHAR)
        CONSTRAINT MEDICAMENTE_tipMedicament_NN NOT NULL,
    
    dozaUnitate NUMBER(4, 0)
        CONSTRAINT MEDICAMENTE_dozaUnitate_NN NOT NULL
);

-- CREAREA TABELULUI TRATAMENTE_ADMINISTRATE
CREATE TABLE TRATAMENTE_ADMINISTRATE  (
    idTratament NUMBER(6, 0)
        CONSTRAINT TRAT_ADM_idTratament_FK REFERENCES TRATAMENTE(idTratament) ON DELETE CASCADE,
    
    idMedicament NUMBER(5, 0)
        CONSTRAINT TRAT_ADM_idMedicament_FK REFERENCES MEDICAMENTE(idMedicament) ON DELETE CASCADE,
    
    doza NUMBER(1, 0)
        CONSTRAINT TRAT_ADM_doza_NN NOT NULL,
    
    CONSTRAINT TRAT_ADM_PK PRIMARY KEY(idTratament, idMedicament)
);

-- POPULAREA TABELULUI JUDETE
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Alba');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Arad');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Arges');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Bacau');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Bihor');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Bistrita-Nasaud');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Botosani');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Brasov');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Braila');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Buzau');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Caras-Severin');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Calarasi');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Cluj');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Constanta');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Covasna');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Dambovita');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Dolj');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Galati');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Giurgiu');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Gorj');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Harghita');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Hunedoara');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Ialomita');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Iasi');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Ilfov');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Maramures');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Mehedinti');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Mures');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Neamt');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Olt');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Prahova');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Satu Mare');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Salaj');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Sibiu');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Suceava');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Teleorman');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Timis');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Tulcea');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Vaslui');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Valcea');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Vrancea');
INSERT INTO JUDETE (codJudet, numeJudet) VALUES (JUDETE_codJudet_SEQ.NEXTVAL, 'Bucuresti');

-- POPULAREA TABELULUI LOCALITATI
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Alba Iulia', LOCALITATI_codPostal_SEQ.NEXTVAL, 1);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Arad', LOCALITATI_codPostal_SEQ.NEXTVAL, 2);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Pitesti', LOCALITATI_codPostal_SEQ.NEXTVAL, 3);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Bacau', LOCALITATI_codPostal_SEQ.NEXTVAL, 4);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Oradea', LOCALITATI_codPostal_SEQ.NEXTVAL, 5);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Bistrita', LOCALITATI_codPostal_SEQ.NEXTVAL, 6);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Botosani', LOCALITATI_codPostal_SEQ.NEXTVAL, 7);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Brasov', LOCALITATI_codPostal_SEQ.NEXTVAL, 8);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Braila', LOCALITATI_codPostal_SEQ.NEXTVAL, 9);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Buzau', LOCALITATI_codPostal_SEQ.NEXTVAL, 10);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Resita', LOCALITATI_codPostal_SEQ.NEXTVAL, 11);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Calarasi', LOCALITATI_codPostal_SEQ.NEXTVAL, 12);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Cluj-Napoca', LOCALITATI_codPostal_SEQ.NEXTVAL, 13);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Constanta', LOCALITATI_codPostal_SEQ.NEXTVAL, 14);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sfantu Gheorghe', LOCALITATI_codPostal_SEQ.NEXTVAL, 15);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Targoviste', LOCALITATI_codPostal_SEQ.NEXTVAL, 16);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Craiova', LOCALITATI_codPostal_SEQ.NEXTVAL, 17);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Galati', LOCALITATI_codPostal_SEQ.NEXTVAL, 18);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Giurgiu', LOCALITATI_codPostal_SEQ.NEXTVAL, 19);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Targu Jiu', LOCALITATI_codPostal_SEQ.NEXTVAL, 20);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Miercurea Ciuc', LOCALITATI_codPostal_SEQ.NEXTVAL, 21);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Deva', LOCALITATI_codPostal_SEQ.NEXTVAL, 22);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Slobozia', LOCALITATI_codPostal_SEQ.NEXTVAL, 23);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Iasi', LOCALITATI_codPostal_SEQ.NEXTVAL, 24);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Buftea', LOCALITATI_codPostal_SEQ.NEXTVAL, 25);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Baia Mare', LOCALITATI_codPostal_SEQ.NEXTVAL, 26);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Drobeta-Turnu Severin', LOCALITATI_codPostal_SEQ.NEXTVAL, 27);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Targu Mures', LOCALITATI_codPostal_SEQ.NEXTVAL, 28);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Piatra Neamt', LOCALITATI_codPostal_SEQ.NEXTVAL, 29);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Slatina', LOCALITATI_codPostal_SEQ.NEXTVAL, 30);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Ploiesti', LOCALITATI_codPostal_SEQ.NEXTVAL, 31);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Satu Mare', LOCALITATI_codPostal_SEQ.NEXTVAL, 32);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Zalau', LOCALITATI_codPostal_SEQ.NEXTVAL, 33);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sibiu', LOCALITATI_codPostal_SEQ.NEXTVAL, 34);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Suceava', LOCALITATI_codPostal_SEQ.NEXTVAL, 35);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Alexandria', LOCALITATI_codPostal_SEQ.NEXTVAL, 36);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Timisoara', LOCALITATI_codPostal_SEQ.NEXTVAL, 37);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Tulcea', LOCALITATI_codPostal_SEQ.NEXTVAL, 38);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Vaslui', LOCALITATI_codPostal_SEQ.NEXTVAL, 39);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Ramnicu Valcea', LOCALITATI_codPostal_SEQ.NEXTVAL, 40);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Focsani', LOCALITATI_codPostal_SEQ.NEXTVAL, 41);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sectorul 1', LOCALITATI_codPostal_SEQ.NEXTVAL, 42);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sectorul 2', LOCALITATI_codPostal_SEQ.NEXTVAL, 42);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sectorul 3', LOCALITATI_codPostal_SEQ.NEXTVAL, 42);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sectorul 4', LOCALITATI_codPostal_SEQ.NEXTVAL, 42);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sectorul 5', LOCALITATI_codPostal_SEQ.NEXTVAL, 42);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Sectorul 6', LOCALITATI_codPostal_SEQ.NEXTVAL, 42);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Barlad', LOCALITATI_codPostal_SEQ.NEXTVAL, 39);
INSERT INTO LOCALITATI (idLocalitate, numeLocalitate, codPostal, codJudet) VALUES (LOCALITATI_idLocalitate_SEQ.NEXTVAL, 'Otopeni', LOCALITATI_codPostal_SEQ.NEXTVAL, 25);

-- POPULAREA TABELULUI SPITALE
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Alba', 1, 'Bd. Republicii', 25, '0235001002', 'contact@sjab.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Arad', 2, 'Sos. Nationala', 123, '0235002003', 'contact@sjar.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Arges', 3, 'Castanilor', 25, '0235003004', 'contact@sjag.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Bacau', 4, 'Libertatii', 25, '0235004005', 'contact@sjbc.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Bihor', 5, 'Stefan cel Mare', 25, '0235005006', 'contact@sjbh.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Bistita-Nasaud', 6, '1 Decembrie', 25, '0235006007', 'contact@sjbn.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Botosani', 7, '11 Iunie', 25, '0235007008', 'contact@sjbt.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Brasov', 8, 'Aviatorilor', 25, '0235008009', 'contact@sjbv.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Braila', 9, 'Fizicienilor', 25, '0235009010', 'contact@sjbr.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Buzau', 10, 'Bd. Unirii', 25, '0235010011', 'contact@sjbz.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Caras-Severin', 11, 'Libertatii', 25, '0235011012', 'contact@sjcs.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Calarasi', 12, 'Traian', 25, '0235012013', 'contact@sjcl.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Cluj', 13, 'Bd. Traian', 45, '0235013014', 'contact@sjcj.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Constanta', 14, 'Sos. Nationala', 25, '0235014015', 'contact@sjct.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Covasna', 15, 'Libertatii', 95, '0235015016', 'contact@sjcv.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Dambovita', 16, 'Castanilor', 25, '0235016017', 'contact@sjdb.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Dolj', 17, 'Independentilor', 25, '0235017018', 'contact@sjdj.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Galati', 18, 'A.I. Cuza', 23, '0235018019', 'contact@sjgl.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Giurgiu', 19, 'I.L. Caragiale', 25, '0235020021', 'contact@sjgr.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Gorj', 20, 'Stefan cel Mare', 25, '0235021022', 'contact@sjgj.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Harghita', 21, '1 Decembrie', 25, '0235022023', 'contact@sjhr.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Hunedoara', 22, '11 Iunie', 125, '0235023024', 'contact@sjhd.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Ialomita', 23, 'Sos. Nationala', 25, '0235024025', 'contact@sjil.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Iasi', 24, 'Independentilor', 25, '0235025026', 'contact@sjis.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Ilfov', 25, 'Bd. Republicii', 25, '0235026027', 'contact@sjif.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Maramures', 26, 'Bd. Republicii', 25, '0235027028', 'contact@sjmm.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Mehedinti', 27, 'Castanilor', 25, '0235028029', 'contact@sjmh.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Mures', 28, '11 Iunie', 25, '0235029030', 'contact@sjms.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Neamt', 29, 'Bd. Republicii', 25, '0235030031', 'contact@sjnt.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Olt', 30, 'Aviatorilor', 25, '0235031032', 'contact@sjot.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Prahova', 31, 'Independentilor', 25, '0235033034', 'contact@sjph.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Satu Mare', 32, 'Fizicienilor', 25, '0235034035', 'contact@sjsm.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Salaj', 33, 'Castanilor', 25, '0235035036', 'contact@sjsj.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Sibiu', 34, 'Stefan cel Mare', 12, '0235036037', 'contact@sjsb.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Suceava', 35, '11 Iunie', 25, '0235038039', 'contact@sjsv.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Teleorman', 36, 'Stefan cel Mare', 25, '0235039040', 'contact@sjtr.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Timis', 37, 'Sos. Nationala', 25, '0235041042', 'contact@sjtm.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Tulcea', 38, '11 Iunie', 25, '0235042043', 'contact@sjtl.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Vaslui', 39, 'Bd. Republicii', 20, '0235043044', 'contact@sjvs.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Valcea', 40, 'Independentilor', 25, '0235044045', 'contact@sjvl.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Judetean de Urgenta Vrancea', 41, 'Bd. Republicii', 25, '0235046047', 'contact@sjvn.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Sectorului 1', 42, '11 Iunie', 25, '0235047048', 'contact@ss1.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Sectorului 2', 43, 'Sos. Nationala', 40, '0235048049', 'contact@ss2.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Sectorului 3', 44, 'Bd. Republicii', 25, '0235049050', 'contact@ss3.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Sectorului 4', 45, 'Independentilor', 25, '0235050051', 'contact@ss4.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Sectorului 5', 46, 'Castanilor', 45, '0235051052', 'contact@ss5.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Sectorului 6', 47, 'Fizicienilor', 25, '0235052053', 'contact@ss6.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul de Copii', 43, 'Aviatorilor', 25, '0235053054', 'contact@scbuc.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Institutul de Cardiologie Bucuresi', 42, 'Stefan cel Mare', 25, '0235054055', 'contact@icb.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul de Boli Infectioase Barlad', 48, 'Sos. Nationala', 25, '0235055056', 'contact@sbib.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul de Boli Pulmonare Iasi', 24, 'Stefan cel Mare', 25, '0235056057', 'contact@sbpi.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul Municipal Otopeni', 1, '11 Iunie', 25, '0235057058', 'contact@smotp.ro');
INSERT INTO SPITALE (idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
VALUES(SPITALE_idSpital_SEQ.NEXTVAL, 'Spitalul de Copii', 37, 'Independentilor', 25, '0235058059', 'contact@sctim.ro');
COMMIT;

-- POPULAREA TABELULUI SECTII
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Alergologie si imunologie clinica'); --1
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'ATI'); --2
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Cardiologie'); --3
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Chirurgie'); --4
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Endocrinologie'); --5
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Epiedemiologie'); --6
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Gastroeneterologie'); --7
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Neurochirurgie'); --8
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Neurologie'); --9
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Oftalmologie'); --10
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Ortopedie'); --11
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Oncologie'); --12
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Pediatrie'); --13
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Pneumologie'); --14
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Psihiatrie'); --15
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Radiologie'); --16
INSERT INTO SECTII (codSectie, numeSectie) VALUES (SECTII_codSectie_SEQ.NEXTVAL, 'Urologie'); --17

-- POPULAREA TABELULUI SECTII_SPITALE 1000
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10001, 1, 'A', 0, '0235044041', 'alergie@sjab.ro');
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10001, 2, 'A', 1, null, 'ati@sjab.ro');
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10002, 4, 'A', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10002, 10, 'B', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10003, 17, null, 1, '0235054041', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10004, 4, null, 0, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10005, 2, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10006, 12, null, 1, null, 'oncologie@sjbn.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10007, 5, null, 1, '0235044042', 'endocrinologie@sjbt.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10008, 1, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10009, 6, 'A', 4, null, 'epidemie@sjbr.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10009, 14, 'B', 5, null, 'neuro@sjbr.ro');
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10010, 10, null, 3, '0235044043', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10011, 12, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10012, 13, 'A', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10012, 14, 'A', 1, '0235044044', null);
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10013, 15, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10014, 16, null, 1, '0235064041', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10015, 17, null, 3, '0235044045', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10016, 11, 'A', 1, null, null);
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10016, 10, 'B', 3, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10017, 2, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10018, 3, null, 1, '0235044046', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10019, 4, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10020, 5, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10021, 6, 'A', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10021, 7, 'B', 1, '0235044047', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10021, 8, 'B', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10022, 9, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10023, 10, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10024, 11, null, 2, '0235044048', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10025, 12, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10026, 13, null, 0, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10027, 14, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10028, 15, null, 1, null, 'psiho@sjms.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10029, 16, 'A', 1, null, null);
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10029, 17, 'A', 2, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10029, 1, 'B', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10029, 2, 'C', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10030, 3, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10031, 4, null, 1, '0235044049', null);
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10032, 5, null, 3, null, null);
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10033, 6, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10034, 7, null, 1, '0235044040', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10035, 8, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10036, 9, null, 4, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10037, 10, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10038, 11, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10039, 12, null, 5, '0235044015', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10040, 13, 'A', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10040, 14, 'B', 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10041, 15, null, 2, '0235044025', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10042, 16, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10043, 17, 'A', 3, '0235074041', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10043, 16, 'A', 1, null, null);
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10044, 15, null, 4, '0235044035', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10045, 14, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10046, 13, null, 2, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10047, 12, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10048, 11, 'A', 1, '0235044095', 'ortopedie@scbuc.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10048, 10, 'B', 1, null, 'ofta@scbuc.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10049, 9, null, 0, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10050, 8, null, 1, '0235084041', null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10051, 7, null, 1, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10052, 6, 'B', 0, null, null); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10052, 5, 'B', 1, '0235044055', 'endocrino@smotp.ro'); 
INSERT INTO SECTII_SPITALE (idSectie, idSpital, codSectie, corp, etaj, telefon, email) VALUES (SECTII_SPITALE_idSectie_SEQ.NEXTVAL, 10053, 4, null, 1, null, null); 

-- POPULAREA TABELULUI FUNCTII
INSERT INTO FUNCTII (codFunctie, numeFunctie) VALUES (FUNCTII_codFunctie_SEQ.NEXTVAL, 'Medic chirurg');
INSERT INTO FUNCTII (codFunctie, numeFunctie) VALUES (FUNCTII_codFunctie_SEQ.NEXTVAL, 'Medic primar');
INSERT INTO FUNCTII (codFunctie, numeFunctie) VALUES (FUNCTII_codFunctie_SEQ.NEXTVAL, 'Medic anestezist');
INSERT INTO FUNCTII (codFunctie, numeFunctie) VALUES (FUNCTII_codFunctie_SEQ.NEXTVAL, 'Medic rezident');
INSERT INTO FUNCTII (codFunctie, numeFunctie) VALUES (FUNCTII_codFunctie_SEQ.NEXTVAL, 'Medic specialist');

-- POPULAREA TABELULUI DOCTORI -- 10000
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Ioana', '2980807452812', '0741000000', 'medic1@gmail.com', 1, 100001);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Adrian', '1890606340497', '0741000001', 'medic2@gmail.com', 2, 100002);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Gabriel', '1910817408864', '0741000002', 'medic3@gmail.com', 3, 100003);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Maria', '2890325319781', '0741000003', 'medic4@gmail.com', 4, 100004);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Rascanu', 'Gabriela', '2780425329582', '0741000004', 'medic5@gmail.com', 5, 100005);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popa', 'Marcel', '1870620178249', '0741000005', 'medic6@gmail.com', 1, 100006);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radulescu', 'Ioana', '2870110117145', '0741000006', 'medic7@gmail.com', 2, 100007);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Ionescu', 'Andra', '2860808336976', '0741000007', 'medic8@gmail.com', 3, 100008);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Lapa', 'Linda', '2870830168254', '0741000008', 'medic9@gmail.com', 4, 100009);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Gava', 'Carol', '1900420421085', '0741000009', 'medic10@gmail.com', 5, 100010);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Luca', 'Ciprian', '1880605172302', '0741000010', 'medic11@gmail.com', 1, 100011);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Dascalu', 'Elena', '2980912239366', '0741000020', 'medic12@gmail.com', 2, 100012);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Chiriac', 'Andreea', '2901216094151', '0741000030', 'medic13@gmail.com', 3, 100013);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Bunescu', 'Tiberiu', '1870106124461', '0741000040', 'medic14@gmail.com', 4, 100014);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Maria', '2950314399761', '0741000050', 'medic15@gmail.com', 5, 100015);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Sabina', '2860907073462', '0741000060', 'medic16@gmail.com', 1, 100016);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Teodor', '1851017258883', '0741000070', 'medic17@gmail.com', 2, 100017);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Andreea', '2911106321724', '0741000080', 'medic18@gmail.com', 3, 100018);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Tobias', '1930314236475', '0741000090', 'medic19@gmail.com', 4, 100019);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Balan', 'Carmen', '2850906026136', '0741000100', 'medic20@gmail.com', 5, 100020);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Maria', '2921010094437', '0741000200', 'medic21@gmail.com', 1, 100021);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Ionescu', 'Costica', '1970923044828', '0741000300', 'medic22@gmail.com', 2, 100022);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Ionascu', 'Dumitru', '1940713017299', '0741000400', 'medic23@gmail.com', 3, 100023);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Postu', 'Larisa', '2970413153510', '0741000500', 'medic24@gmail.com', 4, 100024);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Patrichi', 'Bianca', '2951026148010', '0741000600', 'medic25@gmail.com', 5, 100025);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Cecilia', '2950314399762', '0741000700', 'medic26@gmail.com', 1, 100026);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Andreescu', 'Camelia', '2860907073463', '0741000800', 'medic27@gmail.com', 2, 100027);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Anton', '1851017258884', '0741000900', 'medic28@gmail.com', 3, 100028);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Anton', 'Camelia', '2911106321725', '0741001000', 'medic29@gmail.com', 4, 100029);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Cardon', 'Radu', '1930314236476', '0741002000', 'medic30@gmail.com', 5, 100030);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Patrichi', 'Roberta', '2850906026137', '0741003000', 'medic31@gmail.com', 1, 100031);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Camelia', '2921010094438', '0741004000', 'medic32@gmail.com', 2, 100032);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Ionita', 'Ion', '1970923044829', '0741005000', 'medic33@gmail.com', 3, 100033);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Paul', '1940713017290', '0741006000', 'medic34@gmail.com', 1, 100034);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Lavric', 'Maria', '2970413153591', '0741007000', 'medic45@gmail.com', 2, 100035);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Lovrin', 'Camelia', '2951026148041', '0741008000', 'medic36@gmail.com', 3, 100036);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Ioana', '2950314399763', '0741009000', 'medic37@gmail.com', 4, 100037);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Ionescu', 'Maria', '2860907073464', '0741010000', 'medic38@gmail.com', 5, 100038);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Paul', '1851017258885', '0741020000', 'medic91@gmail.com', 1, 100039);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Andreea', '2911106321726', '0741030000', 'medic39@gmail.com', 2, 100040);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Andrei', '1930314236477', '0741040000', 'medic40@gmail.com', 3, 100041);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Bianca', '2850906026138', '0741050000', 'medic41@gmail.com', 2, 100042);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Andreea', '2921010094439', '0741060000', 'medic42@gmail.com', 1, 100043);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Rosu', 'Robert', '1970923044820', '0741070000', 'medic43@gmail.com', 2, 100044);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Coco', '1940713017291', '0741080000', 'medic44@gmail.com', 3, 100045);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Camelia', '2970413153592', '0741090000', 'medic92@gmail.com', 4, 100046);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Burechita', 'Ioana', '2951026148042', '0741100000', 'medic46@gmail.com', 5, 100047);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Bianca', '2950314399764', '0741200000', 'medic47@gmail.com', 1, 100048);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Loboda', 'Alexandra', '2860907073465', '0741300000', 'medic48@gmail.com', 1, 100049);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Cotae', 'Alexandru', '1851017258886', '0741400000', 'medic49@gmail.com', 1, 100050);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Camelia', '2911106321727', '0741500000', 'medic50@gmail.com', 1, 100051);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Gacea', 'Andrei', '1930314236478', '0741600000', 'medic51@gmail.com', 1, 100052);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Patrichi', 'Alexa', '2850906026139', '0741700000', 'medic52@gmail.com', 3, 100053);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Burechita', 'Maria', '2921010094430', '0741800000', 'medic53@gmail.com', 1, 100054);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popa', 'Andrei', '1970923044821', '0741900000', 'medic54@gmail.com', 4, 100055);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Andrei', '1940713017292', '0741111000', 'medic55@gmail.com', 5, 100056);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Lovrin', 'Bianca', '2970413153593', '0741120000', 'medic56@gmail.com', 1, 100057);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Gacea', 'Radu', '2951026148043', '0741000011', 'medic57@gmail.com', 2, 100058);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Bianca', '2950314399765', '0741000012', 'medic58@gmail.com', 3, 100059);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Postu', 'Larisa', '2860907073466', '0741000013', 'medic59@gmail.com', 4, 100060);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Andrei', '1851017258887', '0741000014', 'medic60@gmail.com', 1, 100061);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Pandele', 'Raluca', '2911106321728', '0741000015', 'medic61@gmail.com', 2, 100062);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Laur', '1930314236479', '0741000016', 'medic62@gmail.com', 1, 100063);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Gacea', 'Bianca', '2850906026130', '0741000017', 'medic63@gmail.com', 2, 100064);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Burechita', 'Andreea', '2921010094431', '0741230000', 'medic64@gmail.com', 1, 100065);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Laur', '1970923044822', '0741000018', 'medic65@gmail.com', 1, 100066);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Dan', 'Alexandru', '1940713017293', '0741000019', 'medic66@gmail.com', 3, 100001);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Larisa', '2970413153594', '0741000021', 'medic67@gmail.com', 4, 100002);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Gacea', 'Bianca', '2951026148044', '0741000022', 'medic68@gmail.com', 5, 100003);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Firea', 'Ioana', '2950314399766', '0741000023', 'medic69@gmail.com', 2, 100004);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Andra', '2860907073467', '0741000024', 'medic70@gmail.com', 2, 100005);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Alexandru', '1851017258888', '0741000025', 'medic71@gmail.com', 1, 100006);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Maria', '2911106321729', '0741000026', 'medic72@gmail.com', 1, 100007);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Dumitru', 'Alexandru', '1930314236470', '0741000270', 'medic73@gmail.com', 2, 100008);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Andra', '2850906026131', '0741000028', 'medic74@gmail.com', 1, 100009);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Dumitrescu', 'Larisa', '2921010094432', '0741000029', 'medic75@gmail.com', 1, 100010);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Ciprian', '1970923044823', '0741000031', 'medic76@gmail.com', 3, 100011);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Lovrin', 'Laur', '1940713017294', '0741000032', 'medic77@gmail.com', 4, 100012);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Gacea', 'Bianca', '2970413153595', '0741000033', 'medic78@gmail.com', 5, 100013);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Litoiu', 'Andra', '2951026148045', '0741000034', 'medic79@gmail.com', 1, 100014);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Larisa', '2950314399767', '0741000035', 'medic80@gmail.com', 1, 100015);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popa', 'Maria', '2860907073468', '0741000036', 'medic81@gmail.com', 1, 100016);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Maxim', 'Laur', '1851017258889', '0741000037', 'medic82@gmail.com', 1, 100017);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Tanase', 'Andra', '2911106321720', '0741000038', 'medic83@gmail.com', 4, 100018);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Litoiu', 'Ciprian', '1930314236471', '0741000041', 'medic84@gmail.com', 3, 100019);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Bianca', '2850906026132', '0741000042', 'medic85@gmail.com', 2, 100020);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Lovrin', 'Andra', '2921010094433', '0741000043', 'medic86@gmail.com', 1, 100021);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Radu', 'Ciprian', '1970923044824', '0741000044', 'medic87@gmail.com', 1, 100022);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popescu', 'Raul', '1940713017295', '0741000045', 'medic88@gmail.com', 2, 100023);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Popa', 'Ioana', '2970413153596', '0741000046', 'medic89@gmail.com', 3, 100024);
INSERT INTO DOCTORI (codParafa, nume, prenume, cnp, telefon, email, codFunctie, idSectie)
VALUES (DOCTORI_codParafa_SEQ.NEXTVAL, 'Litoiu', 'Ioana', '2951026148046', '0741000047', 'medic90@gmail.com', 2, 100025);

-- POPULAREA TABELULUI PACIENTI
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Dimitrina', 'Gratian', '1600501152003', 'Aleea Vlad tepes', 250, 'Radauti', '0710590770', null, 0, TO_DATE('25/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Pompilia', 'Catinca', '2580716089546', 'Splaiul Jiului', 203, 'Mun. Pitesti', '0710860757', null, 0, TO_DATE('31/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Natasa', 'Tiberiu', '1510637943602', 'Splaiul Ion Creanga', 52, 'Galati', '0722330302', null, 1, TO_DATE('04/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Geanina', 'Roza', '6001269995931', 'Splaiul Eroilor', 44, 'Mun. Bragadiru', '0792060082', null, 0, TO_DATE('08/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Mihail', 'George', '1760422603709', 'Aleea Padurii', 49, 'Ardud', '0743717625', null, 1, TO_DATE('12/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Paul', 'Achim', '1781214391087', 'Aleea Piersicului', 79, 'Mun. Isaccea', '0264226364', null, 1, TO_DATE('17/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Alistar', 'Alida', '2970513200102', 'P-ta Bradutului', 271, 'Ocnele Mari', '0369673873', null, 1, TO_DATE('08/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Theodor', 'Paul', '5010415117227', 'B-dul. Henri Coanda', 121, 'Mun. Sighisoara', '0718587491', 'theo.paul@gmail.com', 1, TO_DATE('13/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Beniamin', 'Carla', '6000625431550', 'Str. Frasinului', 138, 'Mun. Targu Carbunesti', '0733532379', null, 0, TO_DATE('31/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Antoniu', 'Crina', '2850327057189', 'B-dul. Mihai Eminescu', 287, 'Mun. Calafat', '0717523908', null, 1, TO_DATE('13/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Adela', 'Serban', '5007418817691', 'P-ta Mircea cel Batran', 215, 'Mun. Breaza', '0353319866', null, 1, TO_DATE('16/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Maria', 'Robertina', '5611211057357', 'Str. Muncii', 152, 'Mun. Pitesti', '0351813877', null, 1, TO_DATE('19/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ilarie', 'Florin', '1850726184859', 'P-ta Jiului', 219, 'Pantelimon', '0211527027', null, 0, TO_DATE('23/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Anatolie', 'Ernest', '1521319144072', 'Str. Herculane', 194, 'Calan', '0718161582', null, 1, TO_DATE('30/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gelu', 'Vasilica', '1050817094622', 'B-dul. Muncii', 175, 'Brezoi', '0774665988', null, 1, TO_DATE('14/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Petronela', 'Margareta', '6411123685489', 'Aleea Louis Pasteur', 113, 'Buftea', '0313236487', null, 1, TO_DATE('17/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Rodica', 'Vicentia', '2880522229769', 'P-ta Independentei', 98, 'Mun. Teius', '0279565627', null, 1, TO_DATE('18/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Iancu', 'Dida', '2880525373021', 'Str. Ciresilor', 274, 'Mun. Babadag', '0344103949', null, 1, TO_DATE('15/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Timotei', 'Titus', '5020412072024', 'P-ta Mesteacanului', 75, 'Slanic-Moldova', '0350616589', null, 0, TO_DATE('24/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Petruta', 'Roberta', '2860407138103', 'Str. Castanilor', 129, 'Mun. Rovinari', '0779198495', null, 0, TO_DATE('15/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Georgia', 'Silvian', '1950916114171', 'P-ta Faget', 30, 'Ghimbav', '0217529438', null, 0, TO_DATE('25/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cezar', 'Gabi', '1941229031288', 'Calea Muncii', 264, 'Turda', '0246965725', null, 1, TO_DATE('12/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Tamara', 'Sabina', '2871112336821', 'Calea Crisan', 177, 'Nucet', '0767687473', null, 0, TO_DATE('16/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ghita', 'Daiana', '2880823370257', 'Calea Traian', 176, 'Brezoi', '0700980650', null, 0, TO_DATE('22/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Codrut', 'Clara', '2941019349725', 'Splaiul Zidarilor', 236, 'Bailesti', '0706796015', null, 0, TO_DATE('26/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cristobal', 'Flaviu', '1921005174791', 'Calea Petrache Poenaru', 107, 'Deta', '0770630572', null, 0, TO_DATE('15/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Maximilian', 'Teona', '2861008065841', 'Calea Bega', 264, 'Piatra Neamt', '0786636409', null, 0, TO_DATE('25/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Valentina', 'Nicodim', '1941213215132', 'B-dul. Closca', 74, 'Dabuleni', '0351393617', null, 0, TO_DATE('14/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Virginia', 'Viviana', '2910612526148', 'P-ta Unirii', 277, 'Mun. Navodari', '0764960077', null, 0, TO_DATE('18/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Catrina', 'Nichifor', '1930815035396', 'P-ta Mircea cel Batran', 138, 'Faget', '0732799260', null, 1, TO_DATE('03/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ciprian', 'Damian', '1960824464318', 'Splaiul Memorandumului', 35, 'Mun. Horezu', '0263198829', null, 1, TO_DATE('11/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ramona', 'Serban', '5020517312716', 'Aleea Piersicului', 153, 'Covasna', '0767173472', null, 0, TO_DATE('21/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Titus', 'Ianis', '5021211014375', 'Aleea Decebal', 250, 'Viseu de Sus', '0341990705', null, 0, TO_DATE('28/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gabi', 'Valter', '1850615130888', 'Calea J.J Rousseau', 51, 'Macin', '0375499916', null, 0, TO_DATE('11/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ioan', 'Caius', '1965768246026', 'P-ta Muncii', 67, 'Constanta', '0785692624', null, 1, TO_DATE('05/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Carmina', 'Florin', '1900731122359', 'Aleea invatatorului', 26, 'Sangeorz-Bai', '0362093670', null, 1, TO_DATE('07/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Mariana', 'Eric', '1860903360344', 'Calea Pacurari', 284, 'Talmaciu', '0746112107', null, 0, TO_DATE('10/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aurelian', 'Theodor', '5020201385816', 'P-ta Pacurari', 15, 'Comanesti', '0754023711', null, 1, TO_DATE('14/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Paul', 'Paraschiva', '2990714065261', 'Str. Henri Coanda', 278, 'Sibiu', '0749691074', null, 1, TO_DATE('08/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Radu', 'Viviana', '2911001456939', 'Str. Mircea cel Batran', 248, 'Mun. Aninoasa', '0374881461', null, 0, TO_DATE('13/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Calin', 'Bogdan', '1930401123518', 'Aleea Croitorilor', 124, 'Slanic', '0262859044', null, 1, TO_DATE('24/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Patru', 'Emanuel', '1990604304883', 'Splaiul Zidarilor', 238, 'Calarasi', '0360873147', null, 0, TO_DATE('27/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Maia', 'Silvia', '2870821160775', 'Str. J.J Rousseau', 149, 'Sacele', '0749024231', null, 0, TO_DATE('29/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ieremia', 'Georgel', '5001025118521', 'Aleea Bega', 40, 'Mun. Sibiu', '0339541455', null, 1, TO_DATE('05/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Georgeta', 'Sebastian', '1941001469207', 'B-dul. Decebal', 213, 'Mun. Milisauti', '0758448207', null, 1, TO_DATE('02/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Georgian', 'Floare', '2910904203972', 'Aleea Petrache Poenaru', 252, 'Mun. Ramnicu Sarat', '0367017816', null, 1, TO_DATE('06/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Toma', 'Antoniu', '1930307172839', 'Aleea Henri Coanda', 226, 'Simeria', '0727800222', null, 1, TO_DATE('09/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Calin', 'Henrieta', '2930110118886', 'Str. Pacurari', 288, 'Scornicesti', '0788906022', null, 1, TO_DATE('20/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ancuta', 'Aristide', '2920228079427', 'Splaiul Piersicului', 195, 'Bicaz', '0355601783', null, 1, TO_DATE('02/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Sever', 'Horatiu', '5020712322719', 'Aleea Aurel Vlaicu', 6, 'Dragomiresti', '0271382622', null, 0, TO_DATE('12/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Carmen', 'Emil', '2843221416645', 'P-ta Ion Creanga', 13, 'Murgeni', '0341866083', null, 1, TO_DATE('22/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Laurentia', 'Vicentia', '6021114450847', 'B-dul. Albert Einstein', 215, 'Mun. Comarnic', '0700129472', null, 1, TO_DATE('25/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Barbu', 'Gianina', '2880520334107', 'Str. Independentei', 41, 'Mun. Ploiesti', '0771881400', null, 1, TO_DATE('19/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Bogdana', 'Malina', '5000108031641', 'Splaiul Piersicului', 188, 'Macin', '0760885288', null, 0, TO_DATE('14/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Adriana', 'Celia', '2850721450261', 'Aleea Horea', 199, 'Piatra-Olt', '0368801637', null, 1, TO_DATE('18/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Jana', 'Lorelei', '2850908128688', 'Aleea Henri Coanda', 106, 'Panciu', '0773131533', null, 1, TO_DATE('24/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Mitica', 'Veta', '2980725139200', 'P-ta Vlad tepes', 6, 'Mun. Cehu Silvaniei', '0212273602', null, 0, TO_DATE('27/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Norbert', 'Dariana', '2900201346417', 'Calea Salcamilor', 157, 'Mun. Dolhasca', '0354220873', null, 0, TO_DATE('03/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Dorel', 'Iustina', '2891106406914', 'Splaiul Castanilor', 17, 'Mun. Draganesti-Olt', '0799162586', null, 0, TO_DATE('05/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Eugenia', 'Vladimir', '1880130116597', 'B-dul. Aurel Vlaicu', 11, 'Bistrita', '0336931581', null, 1, TO_DATE('28/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Teodor', 'Cerasela', '2851207520019', 'Aleea Petrache Poenaru', 196, 'Mun. Jibou', '0744385773', null, 0, TO_DATE('13/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Flaviu', 'Cornel', '1960211063118', 'B-dul. Aurel Vlaicu', 243, 'Mun. Gaesti', '0708387522', null, 0, TO_DATE('02/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Petrut', 'Sandu', '1990426112692', 'Splaiul Louis Pasteur', 144, 'Mun. Otopeni', '0361868357', null, 1, TO_DATE('03/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Petru', 'Ovidiu', '5000614403407', 'P-ta Sinaia', 43, 'Huedin', '0334712333', null, 0, TO_DATE('06/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Florenta', 'Teodor', '1850803086158', 'B-dul. Constantin Brancusi', 57, 'Titu', '0262265930', null, 0, TO_DATE('09/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cantemir', 'Adam', '1870223462493', 'Aleea 1 Decembrie', 71, 'Arad', '0214169412', null, 1, TO_DATE('30/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Emil', 'Decebal', '5020424365916', 'Calea Pacurari', 179, 'Bolintin-Vale', '0212905154', null, 1, TO_DATE('07/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ivona', 'Lorin', '2930824407331', 'Aleea Sinaia', 167, 'Campia Turzii', '0701901930', null, 0, TO_DATE('23/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Silvian', 'Astrid', '2910131088396', 'Calea Bradutului', 274, 'Nadlac', '0267384988', null, 0, TO_DATE('27/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Amanda', 'Maxim', '1921129146088', 'Splaiul Sinaia', 55, 'Pitesti', '0732790450', null, 0, TO_DATE('31/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Zamfira', 'Ilona', '6000722145784', 'B-dul. Frasinului', 124, 'Focsani', '0267065853', null, 1, TO_DATE('03/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Henrieta', 'Sânziana', '2920717462933', 'B-dul. Mesteacanului', 73, 'Mun. Zalau', '0350577950', null, 0, TO_DATE('11/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Beniamin', 'Andreea', '1838028646803', 'Aleea Zidarilor', 69, 'Mun. Zarnesti', '0312263508', null, 1, TO_DATE('17/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gabriela', 'Vicentiu', '1940111063371', 'Aleea Ciresilor', 238, 'Navodari', '0760645791', null, 1, TO_DATE('18/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Claudia', 'Horea', '1940111063373', 'Calea Closca', 131, 'Urlati', '0714596921', null, 1, TO_DATE('24/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Albertina', 'Bianca', '2940111063371', 'P-ta Bradutului', 181, 'Mun. Balcesti', '0358021757', null, 1, TO_DATE('20/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Emanoil', 'Mihaela', '2840111063371', 'P-ta Closca', 289, 'Mun. Petrosani', '0787574340', null, 1, TO_DATE('30/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Laurentiu', 'Emil', '1940111063372', 'Str. Pacurari', 145, 'Mun. Comarnic', '0361818052', null, 0, TO_DATE('11/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Augustin', 'Valeria', '2930308095214', 'Calea Castanilor', 32, 'Mun. Hateg', '0752778884', null, 0, TO_DATE('12/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cristobal', 'Ionelia', '2930308095215', 'Str. Constantin Brancusi', 11, 'Mangalia', '0376511756', null, 0, TO_DATE('15/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Tinca', 'Fabia', '2930310095214', 'Aleea Castanilor', 172, 'Fieni', '0789001676', null, 0, TO_DATE('18/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Daciana', 'Dariana', '2730308095214', 'Splaiul Horea', 284, 'Negru Voda', '0775320471', null, 0, TO_DATE('22/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aura', 'Patricia', '2530408095214', 'B-dul. Padurii', 84, 'Toplita', '0317643761', null, 0, TO_DATE('14/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ozana', 'Agripina', '2930309095213', 'Splaiul Louis Pasteur', 17, 'Mun. Siret', '0362344747', null, 1, TO_DATE('16/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Narcisa', 'Ionel', '1930308095214', 'Calea Traian', 193, 'Mun. Baicoi', '0270196537', null, 0, TO_DATE('03/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Victoria', 'Casandra', '2951208095214', 'Splaiul Frasinului', 267, 'Mun. Pascani', '0725455675', null, 1, TO_DATE('09/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Carina', 'Anda', '2961117138332', 'Str. Padis', 154, 'Constanta', '0707490095', null, 1, TO_DATE('16/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Titus', 'Gheorghe', '1961117138332', 'Splaiul Frunzisului', 123, 'Mun. Mihailesti', '0780373875', null, 0, TO_DATE('21/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Lioara', 'Claudiu', '1961117138333', 'Splaiul Bradutului', 205, 'Mun. Predeal', '0256319292', null, 1, TO_DATE('25/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Silviana', 'Anuta', '2861117138322', 'P-ta Sinaia', 14, 'Fierbinti-Targ', '0232723597', null, 0, TO_DATE('14/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Remus', 'Eustatiu', '1661117138332', 'Str. Albert Einstein', 1, 'Baile Olanesti', '0260953845', null, 1, TO_DATE('27/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Doru', 'Sergiu', '1971117135332', 'Calea Padis', 294, 'Mun. Bistrita', '0215083067', null, 0, TO_DATE('04/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ciprian', 'Marin', '1761214138332', 'Aleea Independentei', 74, 'Petrila', '0700693442', null, 0, TO_DATE('27/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ladislau', 'Andrada', '2861117138332', 'P-ta Mesteacanului', 267, 'Mun. Petrila', '0341405586', null, 0, TO_DATE('25/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aureliana', 'Jean', '1461218138332', 'Str. invatatorului', 209, 'Mun. Sovata', '0269479094', null, 0, TO_DATE('27/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Andrei', 'Letitia', '2910809429424', 'P-ta Mesterilor', 15, 'Mun. Orsova', '0760738887', null, 1, TO_DATE('30/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cipriana', 'Clementina', '2810809429424', 'Str. Mihai Viteazul', 179, 'Nadlac', '0746549875', null, 0, TO_DATE('17/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gina', 'Corneliu', '1910809429424', 'Str. Vlad tepes', 49, 'Pucioasa', '0757833508', null, 1, TO_DATE('24/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Melina', 'Tinca', '2910809429454', 'B-dul. Bradutului', 274, 'Salcea', '0312227718', null, 1, TO_DATE('08/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Agata', 'Lelia', '2910809429426', 'P-ta Closca', 244, 'Mun. Medias', '0769244635', null, 1, TO_DATE('14/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Albertina', 'Ionut', '1710809429424', 'Calea Croitorilor', 94, 'Mun. Mioveni', '0358021757', null, 0, TO_DATE('20/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Emanoil', 'Ivan', '1910714429424', 'B-dul. Unirii', 246, 'Bumbesti-Jiu', '0787574340', null, 1, TO_DATE('30/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Laurentiu', 'Martin', '1510809424424', 'Splaiul Crisan', 102, 'Vicovu de Sus', '0361818052', null, 0, TO_DATE('11/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Augustin', 'Cristobal', '5010809429424', 'Aleea Henri Coanda', 177, 'Mun. Racari', '0752778884', null, 1, TO_DATE('12/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cristobal', 'Valter', '5100809429424', 'Splaiul Padis', 269, 'Anina', '0376511756', null, 1, TO_DATE('15/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Tinca', 'Madalina', '2970415413060', 'B-dul. Ion Creanga', 67, 'Brezoi', '0789001676', null, 1, TO_DATE('18/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Daciana', 'Cantemir', '1970415413060', 'Str. Mesterilor', 252, 'Mun. Plopeni', '0775320471', null, 1, TO_DATE('22/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aura', 'Francisc', '1870415413060', 'Aleea Petrache Poenaru', 300, 'Mun. Harlau', '0317643761', null, 0, TO_DATE('14/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ozana', 'Florina', '2570415413060', 'Str. Croitorilor', 166, 'Solca', '0362344747', null, 0, TO_DATE('16/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Narcisa', 'Benone', '1970415413560', 'Str. Pacurari', 297, 'Craiova', '0270196537', null, 0, TO_DATE('03/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Victoria', 'Gratian', '1970415413062', 'P-ta Zidarilor', 208, 'Bals', '0725455675', null, 1, TO_DATE('09/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Carina', 'Matilda', '2940615413060', 'B-dul. Croitorilor', 182, 'Mun. Anina', '0707490095', null, 1, TO_DATE('16/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Titus', 'Eusebiu', '1970425413060', 'P-ta Frasinului', 43, 'Mun. Carei', '0780373875', null, 0, TO_DATE('21/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Lioara', 'Amedeu', '1971124413060', 'Str. Louis Pasteur', 157, 'Comanesti', '0256319292', null, 0, TO_DATE('25/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Silviana', 'Doru', '5010107022922', 'B-dul. Independentei', 298, 'Odorheiu Secuiesc', '0232723597', null, 0, TO_DATE('14/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Remus', 'Malina', '6010107022922', 'Splaiul Unirii', 285, 'tandarei', '0260953845', null, 1, TO_DATE('27/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Doru', 'Ladislau', '5020107022922', 'Str. Somes', 145, 'Pucioasa', '0215083067', null, 0, TO_DATE('04/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ciprian', 'Antim', '5011207022922', 'B-dul. Henri Coanda', 21, 'Balcesti', '0700693442', null, 1, TO_DATE('27/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ladislau', 'Marcheta', '6050107022922', 'P-ta Memorandumului', 226, 'Valenii de Munte', '0341405586', null, 1, TO_DATE('25/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aureliana', 'Theodor', '1951029157481', 'Str. Franklin Delano Rosevelt', 207, 'Mun. Cazanesti', '0269479094', null, 1, TO_DATE('27/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Andrei', 'Sinica', '1851029157481', 'Calea Louis Pasteur', 262, 'Botosani', '0760738887', null, 0, TO_DATE('30/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cipriana', 'Svetlana', '2951029157481', 'Str. Castanilor', 95, 'Mun. Campulung', '0746549875', null, 0, TO_DATE('17/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gina', 'Maxim', '1951228157481', 'Calea 1 Decembrie', 128, 'Mun. Campia Turzii', '0757833508', null, 0, TO_DATE('24/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Melina', 'Silviana', '2960114126375', 'Calea Unirii', 297, 'Mun. Magurele', '0312227718', null, 0, TO_DATE('08/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Agata', 'Theodor', '1960114126375', 'B-dul. Crisan', 91, 'Mun. Chisineu-Cris', '0769244635', null, 0, TO_DATE('14/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Albertina', 'Atena', '2860114126375', 'Aleea Petrache Poenaru', 258, 'Nucet', '0727926873', null, 1, TO_DATE('29/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Emanoil', 'Petrisor', '1670114126375', 'B-dul. Eroilor', 236, 'Mun. Cisnadie', '0733475298', null, 1, TO_DATE('30/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Laurentiu', 'Aurelia', '2961124126375', 'P-ta Unirii', 26, 'Sacele', '0703900643', null, 0, TO_DATE('01/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Augustin', 'Viorela', '2840114126375', 'Calea Unirii', 188, 'Mun. Buhusi', '0273975521', null, 0, TO_DATE('07/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cristobal', 'Alexe', '1880920332538', 'Aleea J.J Rousseau', 71, 'Mun. Deta', '0318287292', null, 0, TO_DATE('19/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Tinca', 'Raluca', '2880920332538', 'Str. Mesteacanului', 132, 'Mun. Lupeni', '0363876529', null, 1, TO_DATE('28/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Daciana', 'Cezar', '1680920332538', 'Str. Pacurari', 167, 'Hateg', '0771962314', null, 1, TO_DATE('08/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aura', 'Mihnea', '1980520332538', 'B-dul. 1 Decembrie', 41, 'Mun. Sfantu Gheorghe', '0276291211', null, 1, TO_DATE('25/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ozana', 'Ionica', '1940529414527', 'Splaiul Vlad tepes', 223, 'Mun. Caracal', '0783040496', null, 1, TO_DATE('30/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Narcisa', 'Eric', '1740529414527', 'P-ta Vlad tepes', 22, 'Saveni', '0734223608', null, 0, TO_DATE('05/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Victoria', 'Iosif', '1940629414527', 'B-dul. Eroilor', 259, 'Mun. Baia de Aries', '0780678427', null, 0, TO_DATE('14/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Carina', 'Jean', '1950529414527', 'Str. Franklin Delano Rosevelt', 54, 'Victoria', '0757139767', null, 0, TO_DATE('20/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Titus', 'Radu', '1840529414927', 'B-dul. Sinaia', 215, 'Popesti-Leordeni', '0243802196', null, 0, TO_DATE('01/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Lioara', 'Ludmila', '2931201525001', 'Splaiul Pacurari', 244, 'Mun. Campina', '0747842090', null, 0, TO_DATE('20/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Silviana', 'Horatiu', '1931201525001', 'Str. Padurii', 250, 'Pucioasa', '0338945154', null, 0, TO_DATE('01/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Remus', 'Bradut', '1930211525001', 'B-dul. Pacurari', 262, 'Miercurea Ciuc', '0756613185', null, 1, TO_DATE('09/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Doru', 'Silviana', '2941221525001', 'Calea Independentei', 52, 'Mun. Huedin', '0731940398', null, 0, TO_DATE('03/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ciprian', 'Vasile', '1920123020195', 'Splaiul Vlad tepes', 130, 'Sannicolau Mare', '0348617005', null, 0, TO_DATE('04/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ladislau', 'Teea', '2760123020195', 'Str. invatatorului', 96, 'Filiasi', '0715670171', null, 0, TO_DATE('07/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aureliana', 'Madalina', '2940123020895', 'Calea Bega', 213, 'Mun. Harlau', '0776250627', null, 1, TO_DATE('14/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Andrei', 'Clarisa', '2990123020195', 'P-ta Unirii', 123, 'Vaslui', '0247709203', null, 1, TO_DATE('28/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cipriana', 'Claudia', '6020803402558', 'Calea Ciresilor', 267, 'Mun. Budesti', '0750496450', null, 1, TO_DATE('06/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gina', 'Traian', '5020803402558', 'Calea Padis', 255, 'Mun. Baia Mare', '0787403505', null, 0, TO_DATE('07/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Melina', 'Amelia', '6120803402558', 'Calea Mesteacanului', 229, 'Tasnad', '0736712328', null, 0, TO_DATE('10/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Agata', 'Domnica', '6020803402918', 'P-ta Pacurari', 284, 'Mun. Frasin', '0772162801', null, 0, TO_DATE('23/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Georgian', 'Catinca', '2910818078140', 'Splaiul Mihai Viteazul', 74, 'Mun. Caransebes', '0747954867', null, 1, TO_DATE('29/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Calin', 'Aurica', '2880818078140', 'Calea Constantin Brancusi', 288, 'Baile Herculane', '0706200669', null, 0, TO_DATE('30/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Grigore', 'Rica', '1910818078140', 'Calea Petrache Poenaru', 21, 'Bumbesti-Jiu', '0767707573', null, 0, TO_DATE('01/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Viorel', 'Cosmin', '1980818078140', 'P-ta Padurii', 292, 'Mun. Nasaud', '0717554839', null, 0, TO_DATE('07/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Iuliana', 'Victoria', '2910115078140', 'Str. Louis Pasteur', 133, 'Fierbinti-Targ', '0360614222', null, 1, TO_DATE('19/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Valentina', 'Ieremia', '2910615408580', 'Calea Memorandumului', 123, 'Babadag', '0796085795', null, 1, TO_DATE('28/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Celia', 'Geanina', '2900615408580', 'Splaiul Louis Pasteur', 114, 'Mun. Cluj-Napoca', '0757792864', null, 0, TO_DATE('08/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Catalin', 'Dorina', '2870615408580', 'Str. Bradutului', 13, 'Mun. Constanta', '0350066427', null, 0, TO_DATE('25/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aleodor', 'Achim', '1900620408580', 'P-ta Unirii', 219, 'Tasnad', '0755285460', null, 0, TO_DATE('30/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Cantemir', 'Anabela', '2920906304788', 'Aleea Ion Creanga', 168, 'Sovata', '0791910610', null, 1, TO_DATE('05/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Olimpia', 'Marcheta', '2780906304788', 'Str. Ciresilor', 222, 'Mun. Sighetu Marmatiei', '0267197076', null, 1, TO_DATE('14/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gratiana', 'Iris', '2921206304788', 'B-dul. Mircea cel Batran', 210, 'Chisineu-Cris', '0216630045', null, 0, TO_DATE('20/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Iulia', 'Sandu', '1920906304788', 'B-dul. Faget', 3, 'Mun. Baile Govora', '0236865720', null, 0, TO_DATE('01/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Fiodor', 'Anamaria', '2920906312788', 'Splaiul Mihai Viteazul', 165, 'Plopeni', '0366700945', null, 0, TO_DATE('20/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Alida', 'Pompilia', '2991031350013', 'P-ta Somes', 122, 'Mun. Amara', '0341316627', null, 1, TO_DATE('01/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Lavinia', 'Sidonia', '2691231350013', 'Str. Somes', 79, 'Mun. Odorheiu Secuiesc', '0718918052', null, 1, TO_DATE('09/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Dana', 'Damian', '2750631086119', 'Splaiul Decebal', 173, 'Mun. Gheorgheni', '0791095847', null, 1, TO_DATE('03/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Claudia', 'Gelu', '1870817259139', 'Aleea Petrache Poenaru', 291, 'Mun. Vatra Dornei', '0247759589', null, 0, TO_DATE('04/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Codrut', 'Mugur', '1971117259139', 'Aleea Castanilor', 132, 'Mun. Sarmasu', '0361508081', null, 1, TO_DATE('07/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Basarab', 'Iasmina', '2850331420189', 'P-ta Horea', 13, 'Mun. Pitesti', '0377184889', null, 0, TO_DATE('14/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ionel', 'Eftimia', '2950331420189', 'Calea Memorandumului', 132, 'Corabia', '0218005384', null, 0, TO_DATE('28/09/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Ladislau', 'Olivia', '2990431420189', 'Str. Mircea cel Batran', 65, 'Ocnele Mari', '0231501617', null, 1, TO_DATE('06/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Filip', 'Ramona', '2900925254351', 'Str. 1 Decembrie', 196, 'Sangeorgiu de Padure', '0368239187', null, 1, TO_DATE('07/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aristide', 'Elisaveta', '2600925254351', 'Calea Mircea cel Batran', 59, 'Beclean', '0790738168', null, 0, TO_DATE('10/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Costin', 'Lucretia', '2951025254351', 'Str. 1 Decembrie', 179, 'Mun. Pancota', '0339298947', null, 0, TO_DATE('23/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Aurel', 'Silvia', '2891214525091', 'Str. Padurii', 278, 'Ineu', '0723429067', null, 1, TO_DATE('22/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Severin', 'Lavinia', '2991214525091', 'Aleea Vlad tepes', 247, 'Murfatlar', '0258266755', null, 0, TO_DATE('28/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Florenta', 'Mirona', '2791118525091', 'B-dul. Croitorilor', 293, 'Mun. Sangeorz-Bai', '0705105696', null, 1, TO_DATE('31/01/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Leontina', 'Eustatiu', '1900426217625', 'B-dul. Florilor', 296, 'Valea lui Mihai', '0775527403', null, 0, TO_DATE('06/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Antonia', 'Floare', '2900426217625', 'B-dul. Unirii', 199, 'Mun. Bragadiru', '0799276821', null, 0, TO_DATE('15/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Zaharia', 'Voichita', '2971126217625', 'Calea Mesterilor', 237, 'Mun. Brosteni', '0370679826', null, 0, TO_DATE('16/02/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gentiana', 'Camil', '1971206205528', 'P-ta Salcamilor', 221, 'Oradea', '0218317471', null, 1, TO_DATE('12/03/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Gianina', 'Miruna', '2971206205528', 'Calea Piersicului', 297, 'Mun. Cehu Silvaniei', '0217882318', null, 0, TO_DATE('06/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Nidia', 'Marioara', '2871206205528', 'Str. Salcamilor', 276, 'Viseu de Sus', '0703300739', null, 1, TO_DATE('11/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Vladimir', 'Cantemir', '1971206205538', 'Aleea Henri Coanda', 57, 'Reghin', '0365623105', null, 1, TO_DATE('22/04/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Nora', 'Andra', '2971206206528', 'Splaiul Closca', 164, 'Targu Lapus', '0333022469', null, 1, TO_DATE('17/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Corneliu', 'Laurentiu', '1971206206598', 'P-ta Padurii', 274, 'Mun. Brosteni', '0750249910', null, 1, TO_DATE('24/05/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Valeriu', 'Dragos', '1920613099222', 'Str. Henri Coanda', 275, 'Mun. Vascau', '0715082314', null, 1, TO_DATE('09/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Stefania', 'Codrina', '2920613099222', 'B-dul. Independentei', 198, 'Mun. Drobeta-Turnu Severin', '0719109483', null, 1, TO_DATE('11/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Viorel', 'Eric', '1520613099232', 'P-ta Unirii', 185, 'Mun. Campina', '0316320333', null, 0, TO_DATE('29/06/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Malvina', 'Costache', '1620625099222', 'B-dul. Vlad tepes', 273, 'Mun. Sangeorz-Bai', '0311570678', null, 1, TO_DATE('06/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Janeta', 'Alberta', '6010312052781', 'Aleea Herculane', 15, 'Mizil', '0341366588', null, 0, TO_DATE('19/07/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Filip', 'Aglaia', '6110312052781', 'Splaiul Mihai Viteazul', 86, 'Mun. Campulung', '0733599561', null, 0, TO_DATE('09/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Mircea', 'Albertina', '2901026368288', 'P-ta Sinaia', 41, 'Baile Tusnad', '0211929314', null, 0, TO_DATE('16/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Valeriu', 'Veronica', '2801026368288', 'Splaiul Petrache Poenaru', 111, 'Mun. Iasi', '0330991458', null, 0, TO_DATE('20/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Betina', 'Gratian', '1901026368288', 'P-ta Jiului', 225, 'Mun. Slobozia', '0760758688', null, 0, TO_DATE('26/08/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Iustin', 'Jenel', '1951226368288', 'Calea Aurel Vlaicu', 78, 'Mizil', '0708543968', null, 0, TO_DATE('18/10/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Constantin', 'Ionut', '5111026368288', 'Aleea Constantin Brancusi', 278, 'Mun. Milisauti', '0794895908', null, 0, TO_DATE('02/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Henrieta', 'Alexandru', '5001026368288', 'Calea Faget', 176, 'Mun. Bicaz', '0344781778', null, 0, TO_DATE('17/11/2020', 'DD/MM/YYYY'), null);
INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Amanda', 'Lacramioara', '6001228368288', 'P-ta Padis', 99, 'Mun. Draganesti-Olt', '0373845986', null, 1, TO_DATE('20/11/2020', 'DD/MM/YYYY'), null);

-- POPULAREA TABELULUI CONSULTATII
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100000, 1, TO_DATE('25/01/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 2, TO_DATE('31/01/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 3, TO_DATE('04/02/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100010, 4, TO_DATE('08/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100022, 5, TO_DATE('12/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 6, TO_DATE('17/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100044, 7, TO_DATE('08/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100088, 8, TO_DATE('13/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100040, 9, TO_DATE('31/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 10, TO_DATE('13/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 11, TO_DATE('16/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100029, 12, TO_DATE('19/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 13, TO_DATE('23/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 14, TO_DATE('30/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100059, 15, TO_DATE('14/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100023, 16, TO_DATE('17/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 17, TO_DATE('18/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 18, TO_DATE('15/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100042, 19, TO_DATE('24/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100042, 20, TO_DATE('15/09/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100076, 21, TO_DATE('25/09/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100076, 22, TO_DATE('12/10/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100015, 23, TO_DATE('16/11/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100080, 24, TO_DATE('22/11/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100075, 25, TO_DATE('26/11/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100034, 26, TO_DATE('15/02/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100034, 27, TO_DATE('25/02/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100016, 28, TO_DATE('14/03/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 29, TO_DATE('18/03/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100026, 30, TO_DATE('03/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100043, 31, TO_DATE('11/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 32, TO_DATE('21/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 33, TO_DATE('28/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 34, TO_DATE('11/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 35, TO_DATE('05/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 36, TO_DATE('07/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100020, 37, TO_DATE('10/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100086, 38, TO_DATE('14/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100032, 39, TO_DATE('08/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100088, 40, TO_DATE('13/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100050, 41, TO_DATE('24/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100056, 42, TO_DATE('27/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100056, 43, TO_DATE('29/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100061, 44, TO_DATE('05/09/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100055, 45, TO_DATE('02/10/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100054, 46, TO_DATE('06/10/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100062, 47, TO_DATE('09/10/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100058, 48, TO_DATE('20/10/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100053, 49, TO_DATE('02/11/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100041, 50, TO_DATE('12/11/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 51, TO_DATE('22/02/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 52, TO_DATE('25/03/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100083, 53, TO_DATE('19/04/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100020, 54, TO_DATE('14/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100032, 55, TO_DATE('18/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100075, 56, TO_DATE('24/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100005, 57, TO_DATE('27/05/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100006, 58, TO_DATE('03/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100007, 59, TO_DATE('05/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100008, 60, TO_DATE('28/06/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100009, 61, TO_DATE('13/07/2020', 'DD/MM/YYYY')+1);--1
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 62, TO_DATE('02/08/2020', 'DD/MM/YYYY')+1);--2
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 63, TO_DATE('03/08/2020', 'DD/MM/YYYY')+1);--3
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100067, 64, TO_DATE('06/08/2020', 'DD/MM/YYYY')+1);--4
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100022, 65, TO_DATE('09/08/2020', 'DD/MM/YYYY')+1);--5
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100027, 66, TO_DATE('30/08/2020', 'DD/MM/YYYY')+1);--6
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100027, 67, TO_DATE('07/09/2020', 'DD/MM/YYYY')+1);--7
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100023, 68, TO_DATE('23/09/2020', 'DD/MM/YYYY')+1);--8
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100089, 69, TO_DATE('27/09/2020', 'DD/MM/YYYY')+1);--9
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 70, TO_DATE('31/10/2020', 'DD/MM/YYYY')+1);--10
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100062, 71, TO_DATE('03/11/2020', 'DD/MM/YYYY')+1);--11
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100087, 72, TO_DATE('11/11/2020', 'DD/MM/YYYY')+1);--12
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100024, 73, TO_DATE('17/11/2020', 'DD/MM/YYYY')+1);--13
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100020, 74, TO_DATE('18/11/2020', 'DD/MM/YYYY')+1);--14
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100030, 75, TO_DATE('24/11/2020', 'DD/MM/YYYY')+1);--15
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100026, 76, TO_DATE('20/01/2020', 'DD/MM/YYYY')+1);--16
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100030, 77, TO_DATE('30/01/2020', 'DD/MM/YYYY')+1);--17
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 78, TO_DATE('11/02/2020', 'DD/MM/YYYY')+1);--18
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100057, 79, TO_DATE('12/02/2020', 'DD/MM/YYYY')+1);--19
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100032, 80, TO_DATE('15/02/2020', 'DD/MM/YYYY')+1);--20
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100032, 81, TO_DATE('18/02/2020', 'DD/MM/YYYY')+1);--21
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100025, 82, TO_DATE('22/02/2020', 'DD/MM/YYYY')+1);--22
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100033, 83, TO_DATE('14/03/2020', 'DD/MM/YYYY')+1);--23
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100015, 84, TO_DATE('16/03/2020', 'DD/MM/YYYY')+1);--24
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100000, 85, TO_DATE('03/04/2020', 'DD/MM/YYYY')+1);--25
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100075, 86, TO_DATE('09/04/2020', 'DD/MM/YYYY')+1);--26
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100072, 87, TO_DATE('16/05/2020', 'DD/MM/YYYY')+1);--27
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100034, 88, TO_DATE('21/05/2020', 'DD/MM/YYYY')+1);--28
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 89, TO_DATE('25/05/2020', 'DD/MM/YYYY')+1);--29
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100026, 90, TO_DATE('14/06/2020', 'DD/MM/YYYY')+1);--30
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 91, TO_DATE('27/06/2020', 'DD/MM/YYYY')+1);--31
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 92, TO_DATE('04/07/2020', 'DD/MM/YYYY')+1);--32
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 93, TO_DATE('27/08/2020', 'DD/MM/YYYY')+1);--33
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 94, TO_DATE('25/09/2020', 'DD/MM/YYYY')+1);--34
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 95, TO_DATE('27/09/2020', 'DD/MM/YYYY')+1);--35
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 96, TO_DATE('30/09/2020', 'DD/MM/YYYY')+1);--36
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 97, TO_DATE('17/10/2020', 'DD/MM/YYYY')+1);--37
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 98, TO_DATE('24/10/2020', 'DD/MM/YYYY')+1);--38
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100052, 99, TO_DATE('08/11/2020', 'DD/MM/YYYY')+1);--39
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100038, 100, TO_DATE('14/11/2020', 'DD/MM/YYYY')+1);--40
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100033, 101, TO_DATE('20/01/2020', 'DD/MM/YYYY')+1);--41
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100081, 102, TO_DATE('30/01/2020', 'DD/MM/YYYY')+1);--42
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100015, 103, TO_DATE('11/02/2020', 'DD/MM/YYYY')+1);--43
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 104, TO_DATE('12/02/2020', 'DD/MM/YYYY')+1);--44
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100024, 105, TO_DATE('15/02/2020', 'DD/MM/YYYY')+1);--45
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100027, 106, TO_DATE('18/02/2020', 'DD/MM/YYYY')+1);--46
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100062, 107, TO_DATE('22/02/2020', 'DD/MM/YYYY')+1);--47
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100058, 108, TO_DATE('14/03/2020', 'DD/MM/YYYY')+1);--48
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100018, 109, TO_DATE('16/03/2020', 'DD/MM/YYYY')+1);--49
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100024, 110, TO_DATE('03/04/2020', 'DD/MM/YYYY')+1);--50
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100084, 111, TO_DATE('09/04/2020', 'DD/MM/YYYY')+1);--51
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100017, 112, TO_DATE('16/05/2020', 'DD/MM/YYYY')+1);--52
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100085, 113, TO_DATE('21/05/2020', 'DD/MM/YYYY')+1);--53
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100014, 114, TO_DATE('25/05/2020', 'DD/MM/YYYY')+1);--54
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100080, 115, TO_DATE('14/06/2020', 'DD/MM/YYYY')+1);--55
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100080, 116, TO_DATE('27/06/2020', 'DD/MM/YYYY')+1);--56
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100064, 117, TO_DATE('04/07/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 118, TO_DATE('27/08/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 119, TO_DATE('25/09/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100067, 120, TO_DATE('27/09/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100000, 121, TO_DATE('30/09/2020', 'DD/MM/YYYY')+1);--1
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 122, TO_DATE('17/10/2020', 'DD/MM/YYYY')+1);--2
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 123, TO_DATE('24/10/2020', 'DD/MM/YYYY')+1);--3
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100010, 124, TO_DATE('08/11/2020', 'DD/MM/YYYY')+1);--4
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100022, 125, TO_DATE('14/11/2020', 'DD/MM/YYYY')+1);--5
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 126, TO_DATE('29/01/2020', 'DD/MM/YYYY')+1);--6
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100044, 127, TO_DATE('30/01/2020', 'DD/MM/YYYY')+1);--7
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100088, 128, TO_DATE('01/02/2020', 'DD/MM/YYYY')+1);--8
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100040, 129, TO_DATE('07/03/2020', 'DD/MM/YYYY')+1);--9
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 130, TO_DATE('19/03/2020', 'DD/MM/YYYY')+1);--10
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 131, TO_DATE('28/03/2020', 'DD/MM/YYYY')+1);--11
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100029, 132, TO_DATE('08/04/2020', 'DD/MM/YYYY')+1);--12
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 133, TO_DATE('25/04/2020', 'DD/MM/YYYY')+1);--13
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 134, TO_DATE('30/04/2020', 'DD/MM/YYYY')+1);--14
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100059, 135, TO_DATE('05/05/2020', 'DD/MM/YYYY')+1);--15
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100023, 136, TO_DATE('14/05/2020', 'DD/MM/YYYY')+1);--16
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 137, TO_DATE('20/05/2020', 'DD/MM/YYYY')+1);--17
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 138, TO_DATE('01/06/2020', 'DD/MM/YYYY')+1);--18
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100042, 139, TO_DATE('20/06/2020', 'DD/MM/YYYY')+1);--19
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100042, 140, TO_DATE('01/07/2020', 'DD/MM/YYYY')+1);--20
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100076, 141, TO_DATE('09/07/2020', 'DD/MM/YYYY')+1);--21
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100076, 142, TO_DATE('03/08/2020', 'DD/MM/YYYY')+1);--22
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100015, 143, TO_DATE('04/08/2020', 'DD/MM/YYYY')+1);--23
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100080, 144, TO_DATE('07/09/2020', 'DD/MM/YYYY')+1);--24
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100075, 145, TO_DATE('14/09/2020', 'DD/MM/YYYY')+1);--25
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100034, 146, TO_DATE('28/09/2020', 'DD/MM/YYYY')+1);--26
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100034, 147, TO_DATE('06/10/2020', 'DD/MM/YYYY')+1);--27
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100016, 148, TO_DATE('07/10/2020', 'DD/MM/YYYY')+1);--28
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 149, TO_DATE('10/10/2020', 'DD/MM/YYYY')+1);--29
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100026, 150, TO_DATE('23/10/2020', 'DD/MM/YYYY')+1);--30
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100043, 151, TO_DATE('29/01/2020', 'DD/MM/YYYY')+1);--31
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 152, TO_DATE('30/01/2020', 'DD/MM/YYYY')+1);--32
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 153, TO_DATE('01/02/2020', 'DD/MM/YYYY')+1);--33
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 154, TO_DATE('07/03/2020', 'DD/MM/YYYY')+1);--34
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 155, TO_DATE('19/03/2020', 'DD/MM/YYYY')+1);--35
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 156, TO_DATE('28/03/2020', 'DD/MM/YYYY')+1);--36
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100020, 157, TO_DATE('08/04/2020', 'DD/MM/YYYY')+1);--37
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100086, 158, TO_DATE('25/04/2020', 'DD/MM/YYYY')+1);--38
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100032, 159, TO_DATE('30/04/2020', 'DD/MM/YYYY')+1);--39
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100088, 160, TO_DATE('05/05/2020', 'DD/MM/YYYY')+1);--40
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100050, 161, TO_DATE('14/05/2020', 'DD/MM/YYYY')+1);--41
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100056, 162, TO_DATE('20/05/2020', 'DD/MM/YYYY')+1);--42
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100056, 163, TO_DATE('01/06/2020', 'DD/MM/YYYY')+1);--43
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100061, 164, TO_DATE('20/06/2020', 'DD/MM/YYYY')+1);--44
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100055, 165, TO_DATE('01/07/2020', 'DD/MM/YYYY')+1);--45
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100054, 166, TO_DATE('09/07/2020', 'DD/MM/YYYY')+1);--46
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100062, 167, TO_DATE('03/08/2020', 'DD/MM/YYYY')+1);--47
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100058, 168, TO_DATE('04/08/2020', 'DD/MM/YYYY')+1);--48
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100053, 169, TO_DATE('07/09/2020', 'DD/MM/YYYY')+1);--49
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100041, 170, TO_DATE('14/09/2020', 'DD/MM/YYYY')+1);--50
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 171, TO_DATE('28/09/2020', 'DD/MM/YYYY')+1);--51
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 172, TO_DATE('06/10/2020', 'DD/MM/YYYY')+1);--52
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100083, 173, TO_DATE('07/10/2020', 'DD/MM/YYYY')+1);--53
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100020, 174, TO_DATE('10/10/2020', 'DD/MM/YYYY')+1);--54
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100032, 175, TO_DATE('23/10/2020', 'DD/MM/YYYY')+1);--55
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 176, TO_DATE('22/01/2020', 'DD/MM/YYYY')+1);--56
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100040, 177, TO_DATE('28/01/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100042, 178, TO_DATE('31/01/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100044, 179, TO_DATE('06/02/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100046, 180, TO_DATE('15/02/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100000, 181, TO_DATE('16/02/2020', 'DD/MM/YYYY')+1);--1
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100066, 182, TO_DATE('12/03/2020', 'DD/MM/YYYY')+1);--2
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100001, 183, TO_DATE('06/04/2020', 'DD/MM/YYYY')+1);--3
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100010, 184, TO_DATE('11/04/2020', 'DD/MM/YYYY')+1);--4
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100022, 185, TO_DATE('22/04/2020', 'DD/MM/YYYY')+1);--5
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100028, 186, TO_DATE('17/05/2020', 'DD/MM/YYYY')+1);--6
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100044, 187, TO_DATE('24/05/2020', 'DD/MM/YYYY')+1);--7
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100088, 188, TO_DATE('09/06/2020', 'DD/MM/YYYY')+1);--8
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100040, 189, TO_DATE('11/06/2020', 'DD/MM/YYYY')+1);--9
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 190, TO_DATE('29/06/2020', 'DD/MM/YYYY')+1);--10
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100039, 191, TO_DATE('06/07/2020', 'DD/MM/YYYY')+1);--11
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100029, 192, TO_DATE('19/07/2020', 'DD/MM/YYYY')+1);--12
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 193, TO_DATE('09/08/2020', 'DD/MM/YYYY')+1);--13
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100060, 194, TO_DATE('16/08/2020', 'DD/MM/YYYY')+1);--14
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100059, 195, TO_DATE('20/08/2020', 'DD/MM/YYYY')+1);--15
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100023, 196, TO_DATE('26/08/2020', 'DD/MM/YYYY')+1);--16
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100063, 197, TO_DATE('18/10/2020', 'DD/MM/YYYY')+1);--17
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100065, 198, TO_DATE('02/11/2020', 'DD/MM/YYYY')+1);--18
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100042, 199, TO_DATE('17/11/2020', 'DD/MM/YYYY')+1);--19
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100059, 200, TO_DATE('20/11/2020', 'DD/MM/YYYY')+1);

-- ADAUGARE DATE EXTERNARE
UPDATE PACIENTI
SET dataExternare = TO_DATE('14/02/2020', 'DD/MM/YYYY')
WHERE idPacient = 2;

UPDATE PACIENTI
SET dataExternare = TO_DATE('23/05/2020', 'DD/MM/YYYY')
WHERE idPacient = 8;

UPDATE PACIENTI
SET dataExternare = TO_DATE('04/07/2020', 'DD/MM/YYYY')
WHERE idPacient = 14;

UPDATE PACIENTI
SET dataExternare = TO_DATE('28/11/2020', 'DD/MM/YYYY')
WHERE idPacient = 25;

UPDATE PACIENTI
SET dataExternare = TO_DATE('11/10/2020', 'DD/MM/YYYY')
WHERE idPacient =68;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/10/2020', 'DD/MM/YYYY')
WHERE idPacient = 96;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/09/2020', 'DD/MM/YYYY')
WHERE idPacient = 118;

UPDATE PACIENTI
SET dataExternare = TO_DATE('28/09/2020', 'DD/MM/YYYY')
WHERE idPacient = 119;

UPDATE PACIENTI
SET dataExternare = TO_DATE('19/10/2020', 'DD/MM/YYYY')
WHERE idPacient = 122;

UPDATE PACIENTI
SET dataExternare = TO_DATE('18/11/2020', 'DD/MM/YYYY')
WHERE idPacient = 125;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/02/2020', 'DD/MM/YYYY')
WHERE idPacient = 126;

UPDATE PACIENTI
SET dataExternare = TO_DATE('03/02/2020', 'DD/MM/YYYY')
WHERE idPacient = 127;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/09/2020', 'DD/MM/YYYY')
WHERE idPacient = 142;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/02/2020', 'DD/MM/YYYY')
WHERE idPacient = 151;

UPDATE PACIENTI
SET dataExternare = TO_DATE('15/07/2020', 'DD/MM/YYYY')
WHERE idPacient = 166;

UPDATE PACIENTI
SET dataExternare = TO_DATE('29/09/2020', 'DD/MM/YYYY')
WHERE idPacient = 171;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/10/2020', 'DD/MM/YYYY')
WHERE idPacient = 173;

UPDATE PACIENTI
SET dataExternare = TO_DATE('13/02/2020', 'DD/MM/YYYY')
WHERE idPacient = 178;

UPDATE PACIENTI
SET dataExternare = TO_DATE('10/04/2020', 'DD/MM/YYYY')
WHERE idPacient = 183;

UPDATE PACIENTI
SET dataExternare = TO_DATE('02/10/2020', 'DD/MM/YYYY')
WHERE idPacient = 191;

UPDATE PACIENTI
SET dataExternare = TO_DATE('25/11/2020', 'DD/MM/YYYY')
WHERE idPacient = 200;

INSERT INTO PACIENTI (idPacient, nume, prenume, cnp, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
VALUES (PACIENTI_idPacient_SEQ.NEXTVAL, 'Amanda', 'Lacramioara', '6001228368288', 'P-ta Padis', 99, 'Mun. Draganesti-Olt', '0373845986', null, 1, TO_DATE('21/12/2020', 'DD/MM/YYYY'), null);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100035, 201, TO_DATE('21/12/2020', 'DD/MM/YYYY')+1);
INSERT INTO CONSULTATII (idConsultatie, codParafa, idPacient, dataConsultatie) VALUES (CONSULTATII_idConsultatie_SEQ.NEXTVAL, 100037, 201, TO_DATE('22/12/2020', 'DD/MM/YYYY')+1);

-- POPULAREA TABELULUI BOLI
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Alergie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Intoleranta');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Insuficienta respiratorie acuta');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'COVID-19');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Boala coronariana');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'AVC');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Anevrism');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Arteriopatie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Disectie aortica');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Hipertensiune arteriala');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Trombo-embolism');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Insuficienta cardiaca');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Diabet zaharat');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Acromegalia');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Gigantism');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Nanism');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Obezitate');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Boala Conn');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Gripa');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Rujeola');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Scrabie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'SIDA');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Tuberculoza');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Varicela');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Boala Lyme');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Boala Parkinson');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Epilepsie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Boala Alzheimer');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Sindromul Tourette');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Gastrita');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Ulcer gastric');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Sindromul intestinului iritabil');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Cataracta');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Astigmatism');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Daltonism');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Miopie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Himermetropie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Orjelet');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Fractura');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Infarct miocardic');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Pneumonie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Bronsita');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Astm');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'ADHD');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Delir');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Insomnie');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Nevroza');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Cancer');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Insuficienta renala');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Hepatita');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Calculi renali');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Lesin');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Pierdere cunostinta');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Cadere de calciu');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Cadere de magneziu');
INSERT INTO BOLI (codBoala, denumireBoala) VALUES(BOLI_codBoala_SEQ.NEXTVAL, 'Cadere de fier');

-- POPULAREA TABELULUI SIMPTOME
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Febra cu fiori reci');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Febra persistenta');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Cefalee');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Durere acuta');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Sincopa');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Senilitate');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Soc');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Ganglioni limfatici mariti');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Convulsii');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Indispozitie');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Obositate');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Durere cronica');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Edem');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Anorexie');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Gura uscata');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Pierderea anormala a greutatii');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Lipsa apetitului');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Contuzie');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Plaga deschisa');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Leziune');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Arsura');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Fractura inchisa');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Fractura deschisa');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Palpitatii');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Tuse');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dispnee');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Respiratie pe gura');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Hiperventilatie');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Stop respirator');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Stop cardiac');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Greata');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Voma');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Crampe');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dureri musculare');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Lipsa de Ca');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Glicemie scazuta');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Lipsa de Mg');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Lipsa de Fe');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dificultati de mers');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dureri abdominale');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dureri lombare');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dureri intestinale');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dureri in piept');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Dureri ale oaselor');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Durere in gat');
INSERT INTO SIMPTOME (codSimptom, denumireSimptom) VALUES(SIMPTOME_codSimptom_SEQ.NEXTVAL, 'Rinita');

-- POPULAREA TABELULUI DIAGNOSTICE
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 1, 1);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 2, 2);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 3, 3);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 4, 4);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 5, 5);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 6, 6);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 7, 7);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 8, 8);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 9, 9);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 10, 10);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 11, 11);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 12, 12);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 13, 13);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 14, 14);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 15, 15);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 16, 16);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 17, 17);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 18, 18);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 19, 19);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 20, 20);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 21, 21);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 22, 22);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 23, 23);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 24, 24);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 25, 25);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 26, 26);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 27, 27);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 28, 28);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 29, 29);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 30, 30);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 31, 31);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 32, 32);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 33, 33);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 34, 34);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 35, 35);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 36, 36);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 37, 37);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 38, 38);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 39, 39);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 40, 40);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 41, 41);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 42, 42);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 43, 43);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 44, 44);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 45, 45);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 46, 46);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 47, 47);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 48, 48);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 49, 49);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 50, 50);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 51, 51);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 52, 52);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 53, 53);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 54, 54);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 55, 55);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 56, 56);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 57, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 58, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 59, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 60, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 61, 1);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 62, 2);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 63, 3);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 64, 4);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 65, 5);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 66, 6);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 67, 7);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 68, 8);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 69, 9);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 70, 10);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 71, 11);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 72, 12);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 73, 13);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 74, 14);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 75, 15);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 76, 16);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 77, 17);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 78, 18);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 79, 19);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 80, 20);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 81, 21);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 82, 22);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 83, 23);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 84, 24);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 85, 25);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 86, 26);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 87, 27);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 88, 28);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 89, 29);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 90, 30);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 91, 31);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 92, 32);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 93, 33);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 94, 34);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 95, 35);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 96, 36);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 97, 37);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 98, 38);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 99, 39);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 100, 40);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 101, 41);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 102, 42);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 103, 43);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 104, 44);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 105, 45);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 106, 46);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 107, 47);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 108, 48);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 109, 49);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 110, 50);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 111, 51);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 112, 52);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 113, 53);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 114, 54);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 115, 55);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 116, 56);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 117, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 118, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 119, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 120, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 121, 1);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 122, 2);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 123, 3);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 124, 4);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 125, 5);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 126, 6);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 127, 7);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 128, 8);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 129, 9);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 130, 10);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 131, 11);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 132, 12);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 133, 13);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 134, 14);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 135, 15);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 136, 16);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 137, 17);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 138, 18);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 139, 19);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 140, 20);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 141, 21);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 142, 22);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 143, 23);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 144, 24);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 145, 25);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 146, 26);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 147, 27);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 148, 28);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 149, 29);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 150, 30);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 151, 31);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 152, 32);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 153, 33);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 154, 34);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 155, 35);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 156, 36);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 157, 37);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 158, 38);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 159, 39);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 160, 40);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 161, 41);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 162, 42);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 163, 43);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 164, 44);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 165, 45);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 166, 46);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 167, 47);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 168, 48);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 169, 49);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 170, 50);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 171, 51);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 172, 52);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 173, 53);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 174, 54);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 175, 55);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 176, 56);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 177, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 178, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 179, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 180, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 181, 1);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 182, 2);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 183, 3);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 184, 4);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 185, 5);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 186, 6);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 187, 7);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 188, 8);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 189, 9);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 190, 10);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 191, 11);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 192, 12);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 193, 13);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 194, 14);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 195, 15);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 196, 16);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 197, 17);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 198, 18);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 199, 19);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 200, null);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 201, 39);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 202, 1);
INSERT INTO DIAGNOSTICE (idDiagnostic, idConsultatie, codBoala) VALUES (DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL, 202, 2);

-- POPULAREA TABELULUI DIAGNOSTICE_SIMPTOME
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100000, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100000, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100001, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100001, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100002, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100002, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100003, 1);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100003, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100004, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100004, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100005, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100005, 20);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100006, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100006, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100007, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100007, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100008, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100008, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100009, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100009, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100010, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100010, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100011, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100011, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100012, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100013, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100014, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100015, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100015, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100016, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100017, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100017, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100018, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100019, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100020, 31);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100021, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100021, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100022, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100022, 34);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100023, 6);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100023, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100024, 14);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100024, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100025, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100026, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100026, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100027, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100028, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100028, 16);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100029, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100029, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100030, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100030, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100031, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100032, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100033, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100034, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100035, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100036, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100037, 22);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100038, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100038, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100039, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100040, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100041, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100042, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100042, 6);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100043, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100043, 3);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100044, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100044, 11);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100045, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100045, 15);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100046, 8);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100047, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100047, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100048, 17);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100048, 16);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100049, 15);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100049, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100050, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100050, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100051, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100051, 39);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100052, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100053, 37);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100054, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100055, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100055, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100060, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100060, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100061, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100061, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100062, 1);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100062, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100063, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100063, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100064, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100064, 20);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100065, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100065, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100066, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100066, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100067, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100067, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100068, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100068, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100069, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100069, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100070, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100070, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100071, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100072, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100073, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100074, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100074, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100075, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100076, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100076, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100077, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100078, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100079, 31);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100080, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100080, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100081, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100081, 34);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100082, 6);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100082, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100083, 14);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100083, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100084, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100085, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100085, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100086, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100087, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100087, 16);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100088, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100088, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100089, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100089, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100090, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100091, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100092, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100093, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100094, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100095, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100096, 22);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100097, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100097, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100098, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100099, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100100, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100101, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100101, 6);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100102, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100102, 3);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100103, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100103, 11);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100104, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100104, 15);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100105, 8);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100106, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100106, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100107, 17);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100107, 16);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100108, 15);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100108, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100109, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100109, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100110, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100110, 39);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100111, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100112, 37);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100113, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100114, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100114, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100115, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100115, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100120, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100120, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100121, 1);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100121, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100122, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100122, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100123, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100123, 20);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100124, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100124, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100125, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100125, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100126, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100126, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100127, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100127, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100128, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100128, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100129, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100129, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100130, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100131, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100132, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100133, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100133, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100134, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100135, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100135, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100136, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100137, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100138, 31);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100139, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100139, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100140, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100140, 34);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100141, 6);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100141, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100142, 14);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100142, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100143, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100144, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100144, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100145, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100146, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100146, 16);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100147, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100147, 40);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100148, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100148, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100149, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100150, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100151, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100152, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100153, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100154, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100155, 22);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100156, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100156, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100157, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100158, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100159, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100160, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100160, 6);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100161, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100161, 3);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100162, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100162, 11);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100163, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100163, 15);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100164, 8);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100165, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100165, 33);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100166, 17);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100166, 16);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100167, 15);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100167, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100168, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100168, 36);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100169, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100169, 39);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100170, 35);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100171, 37);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100172, 38);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100173, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100173, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100174, 10);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100174, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100175, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100175, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100180, 1);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100180, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100181, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100181, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100182, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100182, 20);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100183, 29);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100183, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100184, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100184, 43);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100185, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100185, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100186, 13);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100186, 7);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100187, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100187, 5);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100188, 26);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100188, 28);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100189, 9);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100190, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100191, 44);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100192, 27);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100192, 30);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100193, 12);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100194, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100194, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100195, 2);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100196, 24);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100197, 31);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100198, 25);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100198, 32);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100200, 39);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100201, 8);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100202, 31);
INSERT INTO DIAGNOSTICE_SIMPTOME (idDiagnostic, codSimptom) VALUES (100202, 38);

-- POPULAREA TABELULUI TRATAMENTE
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 1, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 2, 10, 'nu este recomandat formelor usoare');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 3, 7, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 4, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 5, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 6, 10, 'nu este recomandat formelor usoare');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 7, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 8, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 9, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 10, 3, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 11, 10, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 12, 7, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 13, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 14, 3, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 15, 30, 'nu este recomandat formelor usoare');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 16, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 17, 5, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 18, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 19, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 20, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 21, 10, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 22, 30, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 23, 10, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 24, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 25, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 26, 3, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 27, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 28, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 29, 30, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 30, 7, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 31, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 32, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 33, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 34, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 35, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 36, 30, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 37, 3, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 38, 3, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 39, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 40, 3, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 41, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 42, 30, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 43, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 44, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 45, 30, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 46, 5, 'se poate prelungi peiroada de tratament');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 47, 7, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 48, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 49, 5, 'se poate prelungi peiroada de tratament');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 50, 5, 'nu este recomandat formelor usoare');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 51, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 52, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 53, 5, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 54, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 55, 30, 'se poate prelungi peiroada de tratament');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 56, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 23, 5, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 2, 60, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 34, 15, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 27, 7, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 4, 15, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 53, 4, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 8, 3, null);
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 44, 15, 'se poate micsora doza');
INSERT INTO TRATAMENTE (idTratament, codBoala, perioadaAdministrare, indicatii) VALUES(TRATAMENTE_idTratament_SEQ.NEXTVAL, 27, 3, null);

-- POPULAREA TABELULUI MEDICAMENTE -10000
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Apixabanum', 'anticoagulant', 250);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'FLUOROURACIL ', 'perfuzie', 100);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Eteri', 'anestezic', 50);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Tetracicline', 'antibiotic', 1000);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Cloramfenicol', 'antibiotic', 500);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Ampicillinum', 'antibiotic', 250);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Aztreonam', 'antibiotic', 250);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Streptomycinum', 'antibiotic', 1000);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Paracetamol', 'analgezice', 500);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Nurofen', 'analgezice', 250);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Vitamina C', 'supliment', 1000);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Calciu', 'supliment', 500);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Fier', 'supliment', 250);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Magneziu', 'supliment', 500);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Triferment', 'gastric', 50);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Dicarbocalm', 'gastric', 30);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Pacid', 'antiacid', 20);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Piroxicalm', 'crema', 10);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Canestren', 'crema', 10);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Claritine', 'antialergic', 10);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Reactin', 'antialergic', 10);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Aspirina', 'cardiace', 39);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Rompirin', 'cardiace', 100);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Betadine', 'solutie', 10);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Fucidin', 'unguent', 20);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Visine', 'picaturi ochi', 15);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Aceclofen', 'antibiotic', 50);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Amoxiplus', 'antibiotic', 200);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Ampicilina Atb', 'antibiotic', 500);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Ampiplus', 'antibiotic', 1000);
INSERT INTO MEDICAMENTE (idMedicament, numeMedicament, tipMedicament, dozaUnitate) VALUES(MEDICAMENTE_idMedicament_SEQ.NEXTVAL, 'Ampiplus', 'antibiotic', 500);

-- POPULAREA TABELULUI TRATAMENTE_ADMINISTRATE
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100001, 27, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100002, 26, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100003, 25, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100004, 24, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100005, 23, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100006, 22, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100007, 21, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100008, 20, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100009, 19, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100010, 18, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100011, 17, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100012, 16, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100013, 15, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100014, 14, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100015, 13, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100016, 27, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100017, 11, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100018, 10, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100019, 9, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100020, 8, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100021, 7, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100022, 6, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100023, 5, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100024, 4, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100025, 3, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100026, 2, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100027, 1, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100028, 2, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100029, 3, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100030, 4, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100031, 5, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100032, 6, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100033, 7, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100034, 8, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100035, 9, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100036, 10, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100037, 11, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100038, 12, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100039, 13, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100040, 14, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100041, 15, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100042, 16, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100043, 17, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100044, 18, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100045, 19, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100046, 20, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100047, 21, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100048, 22, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100049, 23, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100050, 24, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100051, 25, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100052, 26, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100053, 27, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100054, 26, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100055, 25, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100056, 23, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100057, 22, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100058, 12, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100059, 18, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100060, 21, 4);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100002, 19, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100004, 18, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100006, 17, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100008, 16, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100010, 15, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100012, 14, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100014, 13, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100016, 12, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100018, 11, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100020, 10, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100001, 9, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100003, 8, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100005, 7, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100007, 6, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100009, 5, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100011, 4, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100013, 3, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100015, 2, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100017, 1, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100019, 2, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100020, 3, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100018, 4, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100016, 5, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100014, 6, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100012, 7, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100019, 8, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100022, 9, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100025, 10, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100030, 11, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100060, 12, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100001, 13, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100005, 14, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100015, 15, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100029, 16, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100036, 11, 2);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100045, 18, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100060, 13, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100061, 5, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100062, 1, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100063, 9, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100064, 11, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100047, 20, 3);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100033, 21, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100027, 22, 1);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100026, 23, 4);
INSERT INTO TRATAMENTE_ADMINISTRATE (idTratament, idMedicament, doza) VALUES(100047, 24, 1);
COMMIT;