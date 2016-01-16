update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.origin =(select Code from NOTAMDB.dbo.NTM_ORIGIN 
where NOTAMDB.dbo.NTM_ORIGIN.Id = NOTAMDB.dbo.NTM_NOTAM.Origin_Id)

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.fir_code =(select Code from NOTAMDB.dbo.NTM_FIR 
where NOTAMDB.dbo.NTM_FIR.Id = NOTAMDB.dbo.NTM_NOTAM.fir_Id)

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.code =(select top(1) 
NOTAMDB.dbo.NTM_CODES.Subject+NOTAMDB.dbo.NTM_CODES.Condition from NOTAMDB.dbo.NTM_CODES 
where NOTAMDB.dbo.NTM_CODES.Id = NOTAMDB.dbo.NTM_NOTAM.Code_Id) 

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.traffic =(select
NOTAMDB.dbo.NTM_CODES.Traffic from NOTAMDB.dbo.NTM_CODES 
where NOTAMDB.dbo.NTM_CODES.Id = NOTAMDB.dbo.NTM_NOTAM.Code_Id) 

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.object =(select
NOTAMDB.dbo.NTM_CODES.Scope from NOTAMDB.dbo.NTM_CODES 
where NOTAMDB.dbo.NTM_CODES.Id = NOTAMDB.dbo.NTM_NOTAM.Code_Id) 

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.range =(select 
NOTAMDB.dbo.NTM_CODES.Purpose from NOTAMDB.dbo.NTM_CODES 
where NOTAMDB.dbo.NTM_CODES.Id = NOTAMDB.dbo.NTM_NOTAM.Code_Id) 


insert into NOFDB.dbo.notam_tab(
origin,type,number,year,notam_type,ref_type,ref_num,ref_year,fir_code,code,
traffic,object,range,lower_limit,higher_limit,latitude,longtitude,radius,fir_aero,fir_a2,fir_a3,fir_a4,fir_a5,
from_date,from_rdate,to_date,to_rdate,perm_est,dfreetxt,efreetxt,ffreetxt,gfreetxt,sendtime)
select origin,Type,Number,Year,NotamType,RefType,RefNum,RefYear,fir_code,code,
traffic,object,range,LowerLimit,HigherLimit,Latitude,Longtitude,radius,firaero,FirA2,fira3,fira4,fira5,
fromdate,fromrdate,todate,tordate,permest,dfreetxt,efreetxt,ffreetxt,gfreetxt,sendtime
 from NOTAMDB.dbo.NTM_NOTAM where Status='D' AND 
 NOT EXISTS(select * from  NOFDB.dbo.notam_tab where NOFDB.dbo.notam_tab.number = NOTAMDB.dbo.NTM_NOTAM.Number)
 AND NOTAMDB.dbo.NTM_NOTAM.code IS NOT NULL  
 AND NOTAMDB.dbo.NTM_NOTAM.origin IS NOT NULL  
 AND NOTAMDB.dbo.NTM_NOTAM.fir_code IS NOT NULL  
 

 insert into NOFDB.dbo.notamarch_tab(
origin,type,number,year,notam_type,ref_type,ref_num,ref_year,fir_code,code,
traffic,object,range,lower_limit,higher_limit,latitude,longtitude,radius,fir_aero,fir_a2,fir_a3,fir_a4,fir_a5,
from_date,from_rdate,to_date,to_rdate,perm_est,dfreetxt,efreetxt,ffreetxt,gfreetxt,sendtime)
select origin,Type,Number,Year,NotamType,RefType,RefNum,RefYear,fir_code,code,
traffic,object,range,LowerLimit,HigherLimit,Latitude,Longtitude,radius,firaero,FirA2,fira3,fira4,fira5,
fromdate,fromrdate,todate,tordate,permest,dfreetxt,efreetxt,ffreetxt,gfreetxt,sendtime
 from NOTAMDB.dbo.NTM_NOTAM where Status='A' AND 
 NOT EXISTS(select * from  NOFDB.dbo.notam_tab where NOFDB.dbo.notam_tab.number = NOTAMDB.dbo.NTM_NOTAM.Number)
 AND NOTAMDB.dbo.NTM_NOTAM.code IS NOT NULL  
 AND NOTAMDB.dbo.NTM_NOTAM.origin IS NOT NULL  
 AND NOTAMDB.dbo.NTM_NOTAM.fir_code IS NOT NULL  