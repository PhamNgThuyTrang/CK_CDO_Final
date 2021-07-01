create or replace procedure SP_PAGINATION_HOSE
(
 h_Ma In Varchar,
 h_Ngay In Date,
 h_sortOrder In Varchar,
 h_pageIndex In Number,
 cv_1 OUT SYS_REFCURSOR
)
as 
v_FirstIndex   NUMBER;
v_LastIndex    NUMBER;

begin
-- Paging
 v_LastIndex := 10 * h_pageIndex;
 v_FirstIndex := v_LastIndex - 10 + 1;

 OPEN cv_1 FOR 
 SELECT * 
 FROM 
    (
        SELECT a.*, ROWNUM AS rnum
        FROM 
        (
            Select * From HOSE hj 
            where ((h_Ma IS NULL) OR (Trim(Upper(hj.MA)) like '%' || Trim(Upper(h_Ma)) || '%'))
            -- Filtering
            And ((h_Ngay IS NULL) OR (hj.NGAY = h_Ngay))
            -- Sorting     
            order by
            Case when h_sortOrder is null then  hj.NGAY End desc,
            Case when h_sortOrder = 'Ngay'then  hj.NGAY End,
            Case When h_sortOrder = 'Ngay_desc' then hj.NGAY End desc,   
            Case when h_sortOrder = 'Name'then  hj.MA End,
            Case When h_sortOrder = 'Name_desc' then hj.MA End desc,     
            Case when h_sortOrder = 'Open'then  hj.GIAMOCUA End,
            Case When h_sortOrder = 'Open_desc' then hj.GIAMOCUA End desc,
            Case when h_sortOrder = 'Close'then  hj.GIADONGCUA End,
            Case When h_sortOrder = 'Close_desc' then hj.GIADONGCUA End desc
        )a
        WHERE ROWNUM <= v_LastIndex
    )    
        WHERE rnum >= v_FirstIndex;
end;

CREATE OR REPLACE procedure CDO.SP_PAGINATION_HNX
(
 h_Ma In Varchar,
 h_Ngay In Date,
 h_sortOrder In Varchar,
 h_pageIndex In Number,
 cv_1 OUT SYS_REFCURSOR
)
as 
v_FirstIndex   NUMBER;
v_LastIndex    NUMBER;

begin
-- Paging
 v_LastIndex := 10 * h_pageIndex;
 v_FirstIndex := v_LastIndex - 10 + 1;

 OPEN cv_1 FOR 
 SELECT * 
 FROM 
    (
        SELECT a.*, ROWNUM AS rnum
        FROM 
        (
            Select * From HNX hj 
            Where ((h_Ma IS NULL) OR (Trim(Upper(hj.MA)) like '%' || Trim(Upper(h_Ma)) || '%'))
            -- Filtering
            and ((h_Ngay IS NULL) OR (hj.NGAY = h_Ngay))
            -- Sorting     
            order by
            Case when h_sortOrder is null then  hj.NGAY End desc,
            Case when h_sortOrder = 'Ngay'then  hj.NGAY End,
            Case When h_sortOrder = 'Ngay_desc' then hj.NGAY End desc,
            Case when h_sortOrder = 'Name'then  hj.MA End,
            Case When h_sortOrder = 'Name_desc' then hj.MA End desc,     
            Case when h_sortOrder = 'Open'then  hj.GIAMOCUA End,
            Case When h_sortOrder = 'Open_desc' then hj.GIAMOCUA End desc,
            Case when h_sortOrder = 'Close'then  hj.GIADONGCUA End,
            Case When h_sortOrder = 'Close_desc' then hj.GIADONGCUA End desc
        )a
        WHERE ROWNUM <= v_LastIndex
    )    
        WHERE rnum >= v_FirstIndex;
end;

create or replace procedure SP_PAGINATION_UPCOM
(
 u_Ma In Varchar,
 u_Ngay In Date,
 u_sortOrder In Varchar,
 u_pageIndex In Number,
 cv_1 OUT SYS_REFCURSOR
)
as 
v_FirstIndex   NUMBER;
v_LastIndex    NUMBER;

begin
-- Paging
 v_LastIndex := 10 * u_pageIndex;
 v_FirstIndex := v_LastIndex - 10 + 1;

 OPEN cv_1 FOR 
 SELECT * 
 FROM 
    (
        SELECT a.*, ROWNUM AS rnum
        FROM 
        (
            Select * From UPCOM uj 
            where ((u_Ma IS NULL) OR (Trim(Upper(uj.MA)) like '%' || Trim(Upper(u_Ma)) || '%'))
            -- Filtering
            and ((u_Ngay IS NULL) OR (uj.NGAY = u_Ngay))
            -- Sorting     
            order by
            Case when u_sortOrder is null then  uj.NGAY End desc,
            Case when u_sortOrder = 'Ngay'then  uj.NGAY End,
            Case When u_sortOrder = 'Ngay_desc' then uj.NGAY End desc,
            Case when u_sortOrder = 'Name'then  uj.MA End,
            Case When u_sortOrder = 'Name_desc' then uj.MA End desc,     
            Case when u_sortOrder = 'Open'then  uj.GIAMOCUA End,
            Case When u_sortOrder = 'Open_desc' then uj.GIAMOCUA End desc,
            Case when u_sortOrder = 'Close'then  uj.GIADONGCUA End,
            Case When u_sortOrder = 'Close_desc' then uj.GIADONGCUA End desc
        )a
        WHERE ROWNUM <= v_LastIndex
    )    
        WHERE rnum >= v_FirstIndex;
end;

create or replace procedure SP_PAGINATION_INDEX
(
 i_Ma In Varchar,
 i_Ngay In Date,
 i_sortOrder In Varchar,
 i_pageIndex In Number,
 cv_1 OUT SYS_REFCURSOR
)
as 
v_FirstIndex   NUMBER;
v_LastIndex    NUMBER;

begin
-- Paging
 v_LastIndex := 10 * i_pageIndex;
 v_FirstIndex := v_LastIndex - 10 + 1;

 OPEN cv_1 FOR 
 SELECT * 
 FROM 
    (
        SELECT a.*, ROWNUM AS rnum
        FROM 
        (
            Select * From CDO."INDEX" ij 
            where ((i_Ma IS NULL) OR (Trim(Upper(ij.CHISO)) like '%' || Trim(Upper(i_Ma)) || '%'))
            -- Filtering
            and ((i_Ngay IS NULL) OR (ij.NGAY = i_Ngay))
            -- Sorting     
            order by
            Case when i_sortOrder is null then  ij.NGAY End desc,
            Case when i_sortOrder = 'Ngay'then  ij.NGAY End,
            Case When i_sortOrder = 'Ngay_desc' then ij.NGAY End desc,     
            Case when i_sortOrder = 'Name'then  ij.CHISO End,
            Case When i_sortOrder = 'Name_desc' then ij.CHISO End desc,     
            Case when i_sortOrder = 'Open'then  ij.MOCUA End,
            Case When i_sortOrder = 'Open_desc' then ij.MOCUA End desc,
            Case when i_sortOrder = 'Close'then  ij.DONGCUA End,
            Case When i_sortOrder = 'Close_desc' then ij.DONGCUA End desc
        )a
        WHERE ROWNUM <= v_LastIndex
    )    
        WHERE rnum >= v_FirstIndex;
end;

create or replace procedure SP_PAGINATION_COMPANY
(
 c_Ma In Varchar,
 c_sortOrder In Varchar,
 c_pageIndex In Number,
 cv_1 OUT SYS_REFCURSOR
)
as 
v_FirstIndex   NUMBER;
v_LastIndex    NUMBER;

begin
-- Paging
 v_LastIndex := 10 * c_pageIndex;
 v_FirstIndex := v_LastIndex - 10 + 1;

 OPEN cv_1 FOR 
 SELECT * 
 FROM 
    (
        SELECT a.*, ROWNUM AS rnum
        FROM 
        (
            Select * From COMPANYDETAILS cj 
            where ((c_Ma IS NULL) OR (Trim(Upper(cj.MA)) like '%' || Trim(Upper(c_Ma)) || '%'))
            -- Sorting     
            order by
            Case when c_sortOrder is null then  cj.MA End,   
            Case when c_sortOrder = 'Ma'then  cj.MA End,
            Case When c_sortOrder = 'Ma_desc' then cj.MA End desc,
            Case when c_sortOrder = 'Name'then  cj.TEN End,
            Case When c_sortOrder = 'Name_desc' then cj.TEN End desc,     
            Case when c_sortOrder = 'Nganh'then  cj.NGANHNGHE End,
            Case When c_sortOrder = 'Nganh_desc' then cj.NGANHNGHE End desc,
            Case when c_sortOrder = 'San'then  cj.SAN End,
            Case When c_sortOrder = 'San_desc' then cj.SAN End desc,
            Case when c_sortOrder = 'KLNY'then  cj.KLNY End,
            Case When c_sortOrder = 'KLNY_desc' then cj.KLNY End desc
        )a
        WHERE ROWNUM <= v_LastIndex
    )    
        WHERE rnum >= v_FirstIndex;
end;

CREATE OR REPLACE PROCEDURE SP_PAGINATION_USER
(
 u_Username IN VARCHAR,
 u_sortOrder IN VARCHAR,
 u_pageIndex IN NUMBER,
 cv_1 OUT SYS_REFCURSOR
)
as 
v_FirstIndex   NUMBER;
v_LastIndex    NUMBER;

begin
-- Paging
 v_LastIndex := 10 * u_pageIndex;
 v_FirstIndex := v_LastIndex - 10 + 1;

 OPEN cv_1 FOR 
 SELECT * 
 FROM 
    (
        SELECT a.*, ROWNUM AS rnum
        FROM 
        (
            Select * From CDO."AspNetUsers" uj
            where ((u_Username IS NULL) OR (Trim(Upper(uj."UserName")) like '%' || Trim(Upper(u_Username)) || '%'))
            -- Sorting     
            order by
            Case when u_sortOrder is null then  uj."Id" End,   
            Case when u_sortOrder = 'UserName'then uj."UserName" End,
            Case When u_sortOrder = 'Userame_desc' then uj."UserName" End desc,
            Case when u_sortOrder = 'Level'then uj."Role" End,
            Case When u_sortOrder = 'Level_desc' then uj."Role" End desc     
        )a
        WHERE ROWNUM <= v_LastIndex
    )    
        WHERE rnum >= v_FirstIndex;
end;