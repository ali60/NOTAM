insert into NOTAMDB.dbo.NTM_DISTRIBUTIONS(Series) select dist_tab.series from NOFDB.dbo.dist_tab

insert into NOTAMDB.dbo.NTM_FIR(Code,Name,Code_atc,NorthLimit,SouthLimit,WestLimit,EastLimit) 
select fir_code,fir_name,atc_code,north_limit,south_limit,west_limit,east_limit from NOFDB.dbo.fir_tab

update NOTAMDB.dbo.NTM_FIR set NOTAMDB.dbo.NTM_FIR.Origin_Id=(select top(1) Id from NOTAMDB.dbo.NTM_ORIGIN 
where NOTAMDB.dbo.NTM_ORIGIN.Code = 
(select top(1) origin from NOFDB.dbo.fir_tab 
where NOFDB.dbo.fir_tab.fir_code = NOTAMDB.dbo.NTM_FIR.Code))

insert into NOTAMDB.dbo.NTM_AERODOMS (Code,Latitude,LongTitude,Direction,VOR,I_L,address) 
select aerodome_code,latitude,longtitude,direction,vor,International,address from NOFDB.dbo.aerodome_tab
go

update NOTAMDB.dbo.NTM_AERODOMS set NOTAMDB.dbo.NTM_AERODOMS.Fir_Id =(select top(1) Id from NOTAMDB.dbo.NTM_FIR 
where NOTAMDB.dbo.NTM_FIR.Code = 
( select top(1) NOFDB.dbo.aerodome_tab.fir_code from NOFDB.dbo.aerodome_tab 
where NOFDB.dbo.aerodome_tab.aerodome_code = NOTAMDB.dbo.NTM_AERODOMS.Code))
go
update NOTAMDB.dbo.NTM_AERODOMS set NOTAMDB.dbo.NTM_AERODOMS.Country_Id =(select top(1) Id from NOTAMDB.dbo.NTM_COUNTRY 
where NOTAMDB.dbo.NTM_COUNTRY.Code = 
( select top(1) NOFDB.dbo.aerodome_tab.country from NOFDB.dbo.aerodome_tab 
where NOFDB.dbo.aerodome_tab.aerodome_code = NOTAMDB.dbo.NTM_AERODOMS.Code))
go

insert into NOTAMDB.dbo.NTM_NOTAM(origin,Type,Number,Year,NotamType,RefType,RefNum,RefYear,fir_code,code,
traffic,object,range,LowerLimit,HigherLimit,Latitude,Longtitude,radius,firaero,FirA2,fira3,fira4,fira5,
fromdate,fromrdate,todate,tordate,permest,dfreetxt,efreetxt,ffreetxt,gfreetxt,sendtime) 
select origin,type,number,year,notam_type,ref_type,ref_num,ref_year,fir_code,code,
traffic,object,range,lower_limit,higher_limit,latitude,longtitude,radius,fir_aero,fir_a2,fir_a3,fir_a4,fir_a5,
from_date,from_rdate,to_date,to_rdate,perm_est,dfreetxt,efreetxt,ffreetxt,gfreetxt,sendtime
 from NOFDB.dbo.notam_tab


update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.origin_Id =(select top(1) Id from NOTAMDB.dbo.NTM_ORIGIN 
where NOTAMDB.dbo.NTM_ORIGIN.Code = NOTAMDB.dbo.NTM_NOTAM.origin)

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.fir_Id =(select top(1) Id from NOTAMDB.dbo.NTM_FIR 
where NOTAMDB.dbo.NTM_FIR.Code = NOTAMDB.dbo.NTM_NOTAM.fir_code)

update NOTAMDB.dbo.NTM_NOTAM  set NOTAMDB.dbo.NTM_NOTAM.Code_Id =(select top(1) Id from NOTAMDB.dbo.NTM_CODES 
where NOTAMDB.dbo.NTM_CODES.Subject = LEFT(NOTAMDB.dbo.NTM_NOTAM.code,2) AND 
	NOTAMDB.dbo.NTM_CODES.Condition = RIGHT(NOTAMDB.dbo.NTM_NOTAM.code,2)) 

update NOTAMDB.dbo.NTM_NOTAM set [Status]='D' 



insert into NOTAMDB.dbo.NTM_SNOWTAM(
           [origin]
           ,[country]
           ,[Number]
           ,[Location]
           ,[Obsrvdate]
           ,[Obsrvrdate]
           ,[Opgroup]
           ,[aerodome]
           ,[Obsrvfulldate]
           ,[Obsrvfullrdate]
           ,[Runway]
           ,[ClearedRunwayLen]
           ,[ClearedRunwayWidth]
           ,[Depositon]
           ,[MeanDepth]
           ,[Friction]
           ,[CriticalSnowbank]
           ,[RunwayLight]
           ,[FurtherClearance]
           ,[FurtherClearanceExp]
           ,[Taxiway]
           ,[TaxiwaySnowbank]
           ,[Runway2]
           ,[ClearedRunwayLen2]
           ,[ClearedRunwayWidth2]
           ,[Depositon2]
           ,[MeanDepth2]
           ,[Friction2]
           ,[CriticalSnowbank2]
           ,[RunwayLight2]
           ,[FurtherClearance2]
           ,[FurtherClearanceexp2]
           ,[Taxiway2]
           ,[TaxiwaySnowbank2]
           ,[Runway3]
           ,[ClearedRunwayLen3]
           ,[ClearedRunwayWidth3]
           ,[Depositon3]
           ,[MeanDepth3]
           ,[Friction3]
           ,[CriticalSnowbank3]
           ,[RunwayLight3]
           ,[FurtherClearance3]
           ,[FurtherClearanceexp3]
           ,[Taxiway3]
           ,[TaxiwaySnowbank3]
           ,[Apron]
           ,[NextObsrv]
           ,[FreeTextt]
           ,[Sendtime]
           ,[Archtime]
           
) 
select [origin],[country],[number],[location],[obsrv_date],[obsrv_rdate],[op_group],[aerodome],[obsrv_full_date],[obsrv_full_rdate],[runway],[cleared_runway_len],[cleared_runway_width],[depositon],[mean_depth],[friction],[critical_snowbank],[runway_light],[further_clearance],[further_clearance_exp],[taxiway],[taxiway_snowbank],[runway2],[cleared_runway_len2],[cleared_runway_width2],[depositon2],[mean_depth2],[friction2],[critical_snowbank2],[runway_light2],[further_clearance2],[further_clearance_exp2],[taxiway2],[taxiway_snowbank2],[runway3],[cleared_runway_len3],[cleared_runway_width3],[depositon3],[mean_depth3],[friction3],[critical_snowbank3],[runway_light3],[further_clearance3],[further_clearance_exp3],[taxiway3],[taxiway_snowbank3],[apron],[next_obsrv],[free_text_t],[sendtime],[archtime]
 from NOFDB.dbo.snowtamarch_tab

update NOTAMDB.dbo.NTM_SNOWTAM set [Status]='A' 






select fir_code from NOFDB.dbo.fir_tab where NOFDB.dbo.fir_tab.origin = NOTAMDB.dbo.NTM_ORIGIN.Code

insert into NOTAMDB.dbo.NTM_ORIGIN(Code,Name,CenterType,DeskType,Serial) 
select origin,name,centertype,desktype,serial from NOFDB.dbo.origin_tab


insert into NOTAMDB.dbo.NTM_CODES (Subject,Subject_Desc,Condition,Condition_Desc,Scope,Category,Traffic,Purpose) 
select subject,subjectDes,condition,conditionDes,range,category,traffic,object from NOFDB.dbo.notamcode_tab

insert into NOTAMDB.dbo.NTM_DISTRIBUTIONS (Series) 
select series from NOFDB.dbo.dist_tab

update NOTAMDB.dbo.NTM_DIST_TO_ADDR set NOTAMDB.dbo.NTM_DIST_TO_ADDR.origin_Id =(select top(1) Id from NOTAMDB.dbo.NTM_ORIGIN 
where NOTAMDB.dbo.NTM_ORIGIN.Code = 
( select top(1) NOFDB.dbo.aerodome_tab.country from NOFDB.dbo.aerodome_tab 
where NOFDB.dbo.aerodome_tab.aerodome_code = NOTAMDB.dbo.NTM_AERODOMS.Code))


