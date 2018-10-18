'Final check to ensure that same bed has not been selected by another user before 
'conversation is saved 

dim bed_cd as double 
dim bed_str as string 
dim status_cd as double 
dim status_mean as string 

bed_cd = getvalue("PERSON.ENCOUNTER.LOC_BED_CD") 

bed_str = "select b.bed_status_cd from bed b" 
bed_str = bed_str & " where b.location_cd = " & bed_cd 

status_cd = cdbl(ccl(bed_str, 1)) 
status_mean = codemeaning(status_cd) 

if status_mean = "ASSIGNED" and bed_cd > 0 then 
 call warning("Another user has placed another patient in the bed you have selected. Please select a currently available bed.") 
 call changefocus("PERSON.ENCOUNTER.LOC_INFO") 
 exit sub 
end if 



'********************************************************** 
'Don't allow baby in room where other occupant is not mother 
'********************************************************** 
dim facility as string 
dim building as string 
dim nurse_unit as string 
dim room as string 
dim domain as string 
dim str1 as string 
dim str1_res as double 
dim eid as string 

facility = getvalue("person.encounter.transfer.loc_facility_cd") 
building = getvalue("person.encounter.transfer.loc_building_cd") 
nurse_unit = getvalue("person.encounter.transfer.loc_nurse_unit_cd") 
room = getvalue("person.encounter.transfer.loc_room_cd") 
eid = getvalue("person.encounter.encntr_id") 

'replace this value with the census code value from cs 339 w/cdf meaning = CENSUS if diffenrent than 1139 
domain = "1139" 

str1 = "select e.person_id from encntr_domain e" 
str1 = str1 & " where e.end_effective_dt_tm >= cnvtdatetime (curdate,curtime3)" 
str1 = str1 & " and e.encntr_domain_type_cd = " & domain 
str1 = str1 & " and e.loc_facility_cd = " & facility 
str1 = str1 & " and e.loc_building_cd = " & building 
str1 = str1 & " and e.loc_nurse_unit_cd = " & nurse_unit 
str1 = str1 & " and e.loc_room_cd = " & room 
str1 = str1 & " and e.loc_bed_cd > 0" 
str1 = str1 & " and e.encntr_id != " & eid 

str1_res = cdbl(ccl(str1, TRUE)) 

if str1_res > 0 then 

dim cclstr as string 
dim inpt as string 
dim m_res as double 
dim pid as string 
dim family_cd as string 

family_cd = cstr(meaningcode (351,"FAMILY")) 
pid = getvalue("person.person_id") 

cclstr = "select p.person_id from person_person_reltn p where p.person_id = " & pid 
cclstr = cclstr & " and p.person_reltn_cd +0 = 0 " 
cclstr = cclstr & " and p.person_reltn_type_cd +0 = " & family_cd 
cclstr = cclstr & " and p.related_person_id +0 = " & cstr(str1_res) 
cclstr = cclstr & " and p.active_ind +0 = 1" 
cclstr = cclstr & " and p.beg_effective_dt_tm <= cnvtdatetime(curdate,curtime3)" 
cclstr = cclstr & " and p.end_effective_dt_tm >= cnvtdatetime(curdate,curtime3)" 

m_res = cdbl(ccl(cclstr,TRUE)) 

if m_res < 1 then 
call warning ("Baby must be in the same room as mother.") 
call setvalue("PERSON.ENCOUNTER.TRANSFER.LOC_ROOM_CD", 0) 
call setvalue("PERSON.ENCOUNTER.TRANSFER.LOC_BED_CD", 0) 
call changefocus("PERSON.ENCOUNTER.TRANSFER.LOC_INFO") 
 end if 
end if 

