'*Warns user if patient selected has a deceased date/time.

dim death as string
death = getvalue("person.deceased_dt_tm")

if death <> "0000000000000000" then
  call message("This person is already deceased.")
  call exitconversation
  exit sub
end if

'*Prevents user from inadvertantly selecting an already discharged encntr when trying to add a new one
' and simply making updates to the discharged encntr

if getvalue("person.encounter.disch.dt.tm") > "0000000000000000" then
   call message("You cannot register a patient with an encounter that has already been discharged")
   call exitconversation
end if

'*Does not allow a new encntr to be added if there is already an active EMERGENCY, INPATIENT, OBSERVATION, OUTPATIENT or
'* DAYSURGERY Encounter

dim person_id    as double
dim encntr_id    as double
dim eclass_cd    as double
dim etype_cd     as double
dim class_cd     as double
dim eid          as double
dim str          as string
dim msg          as string
dim edclass_cd   as double 
dim iclass_cd    as double
dim oclass_cd    as double
dim hclass_cd    as double
dim dsclass_cd   as double

'Gets person id and encntr_id
person_id = getvalue("person.person_id")
encntr_id = getvalue("person.encounter.encntr_id") 

'Verifies if person exists and user is adding a new encounter 
if person_id > 0 and encntr_id <= 0 then 
     eid = 0 
     edclass_cd = displaykeycode("EMERGENCY", 71) 
     iclass_cd = displaykeycode("INPATIENT", 71)
     oclass_cd = displaykeycode("OBSERVATION", 71) 
     hclass_cd = displaykeycode("OUTPATIENT", 71)
     dsclass_cd = displaykeycode("DAYSURGERY", 71)
       str = "select e.encntr_id from encounter e where e.person_id = " 
       str = str & Chr(13) & Chr(10) &   cstr(person_id) & " and (e.encntr_type_cd = "
       str = str & Chr(13) & Chr(10) &   cstr(edclass_cd)   
       str = str & Chr(13) & Chr(10) & " or e.encntr_type_cd = " & cstr(iclass_cd) 
       str = str & Chr(13) & Chr(10) & " or e.encntr_type_cd = " & cstr(oclass_cd) 
       str = str & Chr(13) & Chr(10) & " or e.encntr_type_cd = " & cstr(hclass_cd) 
       str = str & Chr(13) & Chr(10) & " or e.encntr_type_cd = " & cstr(dsclass_cd) & ")"    
       str = str & Chr(13) & Chr(10) & " and e.active_ind = 1" 
       str = str & Chr(13) & Chr(10) & " and e.encntr_status_cd = 854"
       str = str & Chr(13) & Chr(10) & " and e.beg_effective_dt_tm <= cnvtdatetime(curdate,curtime3)" 
       str = str & Chr(13) & Chr(10) & " and e.end_effective_dt_tm >= cnvtdatetime(curdate,curtime3)" 
       eid = cdbl(ccl(str, TRUE)) 
       if eid > 0 then 
     call message("Patient selected has an active Inpatient, Emergency, Observation, Day Surgery, or Hospice encounter. Please discharge those encounters before proceeding.") 
     call exitconversation 
    exit sub 
  end if 
end if