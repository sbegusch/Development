/***************** PRODUKTION **************/
select * from V_BIG_WORKGROUP_MAPPING order by oebez_neu, destination_name;

--select * from BIG_WORKGROUP_MAPPING where DESTINATION_NAME like '%0010%';
--update BIG_WORKGROUP_MAPPING set DESTINATION_NAME = REPLACE(DESTINATION_NAME, '-0010', '') where DESTINATION_NAME like '%0010%';
--commit;

--> MAPPING TABLE
select * from BIG_WORKGROUP_MAPPING where (source_name like 'W1%' or source_name like 'W2%'); --> WIRD ZU "W1"
--update BIG_WORKGROUP_MAPPING set SOURCE_NAME = '-' || SOURCE_NAME where (source_name like 'W1%' or source_name like 'W2%'); 

select * from BIG_WORKGROUP_MAPPING where (source_name like 'W3%' or source_name like 'W4%'); --> WIRD ZU "W2"
--update BIG_WORKGROUP_MAPPING set SOURCE_NAME = '-' || SOURCE_NAME where (source_name like 'W3%' or source_name like 'W4%'); 

select * from BIG_WORKGROUP_MAPPING where (source_name like 'W5%' or source_name like 'W6%'); --> WIRD ZU "W3"
--update BIG_WORKGROUP_MAPPING set SOURCE_NAME = '-' || SOURCE_NAME where (source_name like 'W5%' or source_name like 'W6%'); 

commit;

--> MITARBEITER
select * from MITARBEITER where (username like 'W1%' or username like 'W2%') and status = 0 and ist_rolle = 1; --> WIRD ZU "W1"
--update MITARBEITER set username = '-' || username where (username like 'W1%' or username like 'W2%') and status = 0 and ist_rolle = 1; 
select * from MITARBEITER where (username like 'W3%' or username like 'W4%') and status = 0 and ist_rolle = 1; --> WIRD ZU "W2"
--update MITARBEITER set username = '-' || username where (username like 'W3%' or username like 'W4%') and status = 0 and ist_rolle = 1; 
select * from MITARBEITER where (username like 'W5%' or username like 'W6%') and status = 0 and ist_rolle = 1; --> WIRD ZU "W3"
--update MITARBEITER set username = '-' || username where (username like 'W5%' or username like 'W6%') and status = 0 and ist_rolle = 1; 

commit;

