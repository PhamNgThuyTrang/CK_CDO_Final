
CREATE TABLE COMPANYDETAILS
(
    MA VARCHAR(20) PRIMARY KEY,
    TEN VARCHAR(200) NOT NULL,
    NGANHNGHE VARCHAR(200) NOT NULL,
    SAN VARCHAR(10) NOT NULL,
    KLNY NUMBER
);

CREATE TABLE HOSE
(
    ID NUMBER PRIMARY KEY,
    MA VARCHAR(20) NOT NULL,
    NGAY DATE NOT NULL,
    GIAMOCUA FLOAT NOT NULL,
    GIATRAN FLOAT NOT NULL,
    GIASAN FLOAT NOT NULL,
    GIADONGCUA FLOAT NOT NULL,
    KHOILUONG NUMBER
)
PARTITION BY RANGE(NGAY)
( 
    PARTITION HOP_2000 VALUES LESS THAN (TO_DATE('01-01-2001','DD-MM-YYYY')),
	PARTITION HOP_2001 VALUES LESS THAN (TO_DATE('01-01-2002','DD-MM-YYYY')),
    PARTITION HOP_2002 VALUES LESS THAN (TO_DATE('01-01-2003','DD-MM-YYYY')),
    PARTITION HOP_2003 VALUES LESS THAN (TO_DATE('01-01-2004','DD-MM-YYYY')),
    PARTITION HOP_2004 VALUES LESS THAN (TO_DATE('01-01-2005','DD-MM-YYYY')),
    PARTITION HOP_2005 VALUES LESS THAN (TO_DATE('01-01-2006','DD-MM-YYYY')),
    PARTITION HOP_2006 VALUES LESS THAN (TO_DATE('01-01-2007','DD-MM-YYYY')),
    PARTITION HOP_2007 VALUES LESS THAN (TO_DATE('01-01-2008','DD-MM-YYYY')),
    PARTITION HOP_2008 VALUES LESS THAN (TO_DATE('01-01-2009','DD-MM-YYYY')),
    PARTITION HOP_2009 VALUES LESS THAN (TO_DATE('01-01-2010','DD-MM-YYYY')),
    PARTITION HOP_2010 VALUES LESS THAN (TO_DATE('01-01-2011','DD-MM-YYYY')),
    PARTITION HOP_2011 VALUES LESS THAN (TO_DATE('01-01-2012','DD-MM-YYYY')),
    PARTITION HOP_2012 VALUES LESS THAN (TO_DATE('01-01-2013','DD-MM-YYYY')),
    PARTITION HOP_2013 VALUES LESS THAN (TO_DATE('01-01-2014','DD-MM-YYYY')),
    PARTITION HOP_2014 VALUES LESS THAN (TO_DATE('01-01-2015','DD-MM-YYYY')),
    PARTITION HOP_2015 VALUES LESS THAN (TO_DATE('01-01-2016','DD-MM-YYYY')),
    PARTITION HOP_2016 VALUES LESS THAN (TO_DATE('01-01-2017','DD-MM-YYYY')),
    PARTITION HOP_2017 VALUES LESS THAN (TO_DATE('01-01-2018','DD-MM-YYYY')),
    PARTITION HOP_2018 VALUES LESS THAN (TO_DATE('01-01-2019','DD-MM-YYYY')),
    PARTITION HOP_2019 VALUES LESS THAN (TO_DATE('01-01-2020','DD-MM-YYYY')),
    PARTITION HOP_2020 VALUES LESS THAN (TO_DATE('01-01-2021','DD-MM-YYYY')),
    PARTITION HOP_2021 VALUES LESS THAN (MAXVALUE)
);

CREATE TABLE HNX
(
    ID NUMBER PRIMARY KEY,
    MA VARCHAR(20) NOT NULL,
    NGAY DATE NOT NULL,
    GIAMOCUA FLOAT NOT NULL,
    GIATRAN FLOAT NOT NULL,
    GIASAN FLOAT NOT NULL,
    GIADONGCUA FLOAT NOT NULL,
    KHOILUONG NUMBER
)
PARTITION BY RANGE(NGAY)
( 
    PARTITION HNP_2000 VALUES LESS THAN (TO_DATE('01-01-2001','DD-MM-YYYY')),
    PARTITION HNP_2001 VALUES LESS THAN (TO_DATE('01-01-2002','DD-MM-YYYY')),
    PARTITION HNP_2002 VALUES LESS THAN (TO_DATE('01-01-2003','DD-MM-YYYY')),
    PARTITION HNP_2003 VALUES LESS THAN (TO_DATE('01-01-2004','DD-MM-YYYY')),
    PARTITION HNP_2004 VALUES LESS THAN (TO_DATE('01-01-2005','DD-MM-YYYY')),
    PARTITION HNP_2005 VALUES LESS THAN (TO_DATE('01-01-2006','DD-MM-YYYY')),
    PARTITION HNP_2006 VALUES LESS THAN (TO_DATE('01-01-2007','DD-MM-YYYY')),
    PARTITION HNP_2007 VALUES LESS THAN (TO_DATE('01-01-2008','DD-MM-YYYY')),
    PARTITION HNP_2008 VALUES LESS THAN (TO_DATE('01-01-2009','DD-MM-YYYY')),
    PARTITION HNP_2009 VALUES LESS THAN (TO_DATE('01-01-2010','DD-MM-YYYY')),
    PARTITION HNP_2010 VALUES LESS THAN (TO_DATE('01-01-2011','DD-MM-YYYY')),
    PARTITION HNP_2011 VALUES LESS THAN (TO_DATE('01-01-2012','DD-MM-YYYY')),
    PARTITION HNP_2012 VALUES LESS THAN (TO_DATE('01-01-2013','DD-MM-YYYY')),
    PARTITION HNP_2013 VALUES LESS THAN (TO_DATE('01-01-2014','DD-MM-YYYY')),
    PARTITION HNP_2014 VALUES LESS THAN (TO_DATE('01-01-2015','DD-MM-YYYY')),
    PARTITION HNP_2015 VALUES LESS THAN (TO_DATE('01-01-2016','DD-MM-YYYY')),
    PARTITION HNP_2016 VALUES LESS THAN (TO_DATE('01-01-2017','DD-MM-YYYY')),
    PARTITION HNP_2017 VALUES LESS THAN (TO_DATE('01-01-2018','DD-MM-YYYY')),
    PARTITION HNP_2018 VALUES LESS THAN (TO_DATE('01-01-2019','DD-MM-YYYY')),
    PARTITION HNP_2019 VALUES LESS THAN (TO_DATE('01-01-2020','DD-MM-YYYY')),
    PARTITION HNP_2020 VALUES LESS THAN (TO_DATE('01-01-2021','DD-MM-YYYY')),
    PARTITION HNP_2021 VALUES LESS THAN (MAXVALUE)
);

CREATE TABLE UPCOM
(
    ID NUMBER PRIMARY KEY,
    MA VARCHAR(20) NOT NULL,
    NGAY DATE NOT NULL,
    GIAMOCUA FLOAT NOT NULL,
    GIATRAN FLOAT NOT NULL,
    GIASAN FLOAT NOT NULL,
    GIADONGCUA FLOAT NOT NULL,
    KHOILUONG NUMBER
)
PARTITION BY RANGE(NGAY)
( 
    PARTITION UPP_2000 VALUES LESS THAN (TO_DATE('01-01-2001','DD-MM-YYYY')),
    PARTITION UPP_2001 VALUES LESS THAN (TO_DATE('01-01-2002','DD-MM-YYYY')),
    PARTITION UPP_2002 VALUES LESS THAN (TO_DATE('01-01-2003','DD-MM-YYYY')),
    PARTITION UPP_2003 VALUES LESS THAN (TO_DATE('01-01-2004','DD-MM-YYYY')),
    PARTITION UPP_2004 VALUES LESS THAN (TO_DATE('01-01-2005','DD-MM-YYYY')),
    PARTITION UPP_2005 VALUES LESS THAN (TO_DATE('01-01-2006','DD-MM-YYYY')),
    PARTITION UPP_2006 VALUES LESS THAN (TO_DATE('01-01-2007','DD-MM-YYYY')),
    PARTITION UPP_2007 VALUES LESS THAN (TO_DATE('01-01-2008','DD-MM-YYYY')),
    PARTITION UPP_2008 VALUES LESS THAN (TO_DATE('01-01-2009','DD-MM-YYYY')),
    PARTITION UPP_2009 VALUES LESS THAN (TO_DATE('01-01-2010','DD-MM-YYYY')),
    PARTITION UPP_2010 VALUES LESS THAN (TO_DATE('01-01-2011','DD-MM-YYYY')),
    PARTITION UPP_2011 VALUES LESS THAN (TO_DATE('01-01-2012','DD-MM-YYYY')),
    PARTITION UPP_2012 VALUES LESS THAN (TO_DATE('01-01-2013','DD-MM-YYYY')),
    PARTITION UPP_2013 VALUES LESS THAN (TO_DATE('01-01-2014','DD-MM-YYYY')),
    PARTITION UPP_2014 VALUES LESS THAN (TO_DATE('01-01-2015','DD-MM-YYYY')),
    PARTITION UPP_2015 VALUES LESS THAN (TO_DATE('01-01-2016','DD-MM-YYYY')),
    PARTITION UPP_2016 VALUES LESS THAN (TO_DATE('01-01-2017','DD-MM-YYYY')),
    PARTITION UPP_2017 VALUES LESS THAN (TO_DATE('01-01-2018','DD-MM-YYYY')),
    PARTITION UPP_2018 VALUES LESS THAN (TO_DATE('01-01-2019','DD-MM-YYYY')),
    PARTITION UPP_2019 VALUES LESS THAN (TO_DATE('01-01-2020','DD-MM-YYYY')),
    PARTITION UPP_2020 VALUES LESS THAN (TO_DATE('01-01-2021','DD-MM-YYYY')),
    PARTITION UPP_2021 VALUES LESS THAN (MAXVALUE)
);

CREATE TABLE INDEXX
(
    ID NUMBER PRIMARY KEY,
    MA VARCHAR(20) NOT NULL,
    NGAY DATE NOT NULL,
    MOCUA FLOAT NOT NULL,
    TRAN FLOAT NOT NULL,
    SAN FLOAT NOT NULL,
    DONGCUA FLOAT NOT NULL,
    KHOILUONG NUMBER
)
PARTITION BY RANGE(NGAY)
( 
    PARTITION INP_2000 VALUES LESS THAN (TO_DATE('01-01-2001','DD-MM-YYYY')),
    PARTITION INP_2001 VALUES LESS THAN (TO_DATE('01-01-2002','DD-MM-YYYY')),
    PARTITION INP_2002 VALUES LESS THAN (TO_DATE('01-01-2003','DD-MM-YYYY')),
    PARTITION INP_2003 VALUES LESS THAN (TO_DATE('01-01-2004','DD-MM-YYYY')),
    PARTITION INP_2004 VALUES LESS THAN (TO_DATE('01-01-2005','DD-MM-YYYY')),
    PARTITION INP_2005 VALUES LESS THAN (TO_DATE('01-01-2006','DD-MM-YYYY')),
    PARTITION INP_2006 VALUES LESS THAN (TO_DATE('01-01-2007','DD-MM-YYYY')),
    PARTITION INP_2007 VALUES LESS THAN (TO_DATE('01-01-2008','DD-MM-YYYY')),
    PARTITION INP_2008 VALUES LESS THAN (TO_DATE('01-01-2009','DD-MM-YYYY')),
    PARTITION INP_2009 VALUES LESS THAN (TO_DATE('01-01-2010','DD-MM-YYYY')),
    PARTITION INP_2010 VALUES LESS THAN (TO_DATE('01-01-2011','DD-MM-YYYY')),
    PARTITION INP_2011 VALUES LESS THAN (TO_DATE('01-01-2012','DD-MM-YYYY')),
    PARTITION INP_2012 VALUES LESS THAN (TO_DATE('01-01-2013','DD-MM-YYYY')),
    PARTITION INP_2013 VALUES LESS THAN (TO_DATE('01-01-2014','DD-MM-YYYY')),
    PARTITION INP_2014 VALUES LESS THAN (TO_DATE('01-01-2015','DD-MM-YYYY')),
    PARTITION INP_2015 VALUES LESS THAN (TO_DATE('01-01-2016','DD-MM-YYYY')),
    PARTITION INP_2016 VALUES LESS THAN (TO_DATE('01-01-2017','DD-MM-YYYY')),
    PARTITION INP_2017 VALUES LESS THAN (TO_DATE('01-01-2018','DD-MM-YYYY')),
    PARTITION INP_2018 VALUES LESS THAN (TO_DATE('01-01-2019','DD-MM-YYYY')),
    PARTITION INP_2019 VALUES LESS THAN (TO_DATE('01-01-2020','DD-MM-YYYY')),
    PARTITION INP_2020 VALUES LESS THAN (TO_DATE('01-01-2021','DD-MM-YYYY')),
    PARTITION INP_2021 VALUES LESS THAN (MAXVALUE)
);

CREATE SEQUENCE AUTO_INCREMENT_SEQUENCE_HOSE
START WITH 1
INCREMENT BY 1;

CREATE OR REPLACE TRIGGER AUTO_INCREMENT_TRIGGER_HOSE
BEFORE INSERT ON
HOSE
REFERENCING NEW AS NEW
FOR EACH ROW 
BEGIN 
    SELECT AUTO_INCREMENT_SEQUENCE_HOSE.NEXTVAL INTO :NEW.ID
    FROM DUAL;
END;


CREATE SEQUENCE AUTO_INCREMENT_SEQUENCE_HNX
START WITH 1
INCREMENT BY 1;

CREATE OR REPLACE TRIGGER AUTO_INCREMENT_TRIGGER_HNX
BEFORE INSERT ON
HNX
REFERENCING NEW AS NEW
FOR EACH ROW 
BEGIN 
    SELECT AUTO_INCREMENT_SEQUENCE_HNX.NEXTVAL INTO :NEW.ID
    FROM DUAL;
END;


CREATE SEQUENCE AUTO_INCREMENT_SEQUENCE_UPCOM
START WITH 1
INCREMENT BY 1;

CREATE OR REPLACE TRIGGER AUTO_INCREMENT_TRIGGER_UPCOM
BEFORE INSERT ON
UPCOM
REFERENCING NEW AS NEW
FOR EACH ROW 
BEGIN 
    SELECT AUTO_INCREMENT_SEQUENCE_UPCOM.NEXTVAL INTO :NEW.ID
    FROM DUAL;
END;


CREATE SEQUENCE AUTO_INCREMENT_SEQUENCE_INDEXX
START WITH 1
INCREMENT BY 1;

CREATE OR REPLACE TRIGGER AUTO_INCREMENT_TRIGGER_INDEXX
BEFORE INSERT ON
INDEXX
REFERENCING NEW AS NEW
FOR EACH ROW 
BEGIN 
    SELECT AUTO_INCREMENT_SEQUENCE_INDEXX.NEXTVAL INTO :NEW.ID
    FROM DUAL;
END;

ALTER TABLE HOSE ADD(CONSTRAINT FK_HOSE_COMPANYDETAILS FOREIGN KEY(MA) REFERENCES COMPANYDETAILS(MA) );
ALTER TABLE HNX ADD(CONSTRAINT FK_HNX_COMPANYDETAILS FOREIGN KEY(MA) REFERENCES COMPANYDETAILS(MA) );
ALTER TABLE UPCOM ADD(CONSTRAINT FK_UPCOM_COMPANYDETAILS FOREIGN KEY(MA) REFERENCES COMPANYDETAILS(MA) );

