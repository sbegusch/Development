/***************** PRODUKTION **************/
select * from V_BIG_WORKGROUP_MAPPING where source_name like 'W%' and source_name = destination_name order by source_name;  --7 Zeilen

select * from BIG_WORKGROUP_MAPPING 
where source_id in (1219,154,1109,152,1411,153,155,  --> WU Arbeitsgruppen
  1224,1005,1114,1007,1416,1008,1006,2104);         --> W1 Arbeitsgruppen

--> UPDATE
--update BIG_WORKGROUP_MAPPING set SOURCE_NAME = 'w' || SOURCE_NAME 
--where source_id in (1219,154,1109,152,1411,153,155,  --> WU Arbeitsgruppen
--    1224,1005,1114,1007,1416,1008,1006,2104);         --> W1 Arbeitsgruppen
commit;

select * from mitarbeiter 
where usernr in (1219,154,1109,152,1411,153,155,  --> WU Arbeitsgruppen
1224,1005,1114,1007,1416,1008,1006,2104);         --> W1 Arbeitsgruppen
--> UPDATE
--update mitarbeiter set username = 'w' || username 
--where usernr in (1219,154,1109,152,1411,153,155,  --> WU Arbeitsgruppen
--1224,1005,1114,1007,1416,1008,1006,2104);         --> W1 Arbeitsgruppen
commit;
