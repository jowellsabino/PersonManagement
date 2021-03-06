'*Hides the beggining MSP question
call hideprompt("PERSON.QUESTIONNAIRE_01.QUESTIONS_29.VALUE_CD")
call hideprompt("PERSON.QUESTIONNAIRE_01.QUESTIONS_01.VALUE_CD")

'*Rules to blank out the verify status fields of all insurances so the user is forced
'to ask each time
dim encntr_id2 as double
encntr_id2 = getvalue("person.encounter.encntr_id")

if encntr_id2 = 0 then
call setvalue("person.subscriber_01.person.health_plan.verify_status_cd",0)
call setvalue("person.subscriber_02.person.health_plan.verify_status_cd",0)
call setvalue("person.subscriber_03.person.health_plan.verify_status_cd",0)
end if

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

'dim DaysDiff as long
'DaysDiff = CompareToCurrentDate("person.encounter.disch_dt_tm")
'if DaysDiff < -0.5 then
 ' call message("You have selected a discharged encounter.  You can not make changes to an encounter 12 hours post discharge. Please contact your supervisor.")
  'call exitconversation
  'exit sub
'end if

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


'*warns user if the disease alert field is populated

dim disease_alert_code as double 
disease_alert_code = cstr(getvalue("person.patient.disease_alert_cd"))
if encntr_id = 0 then
if disease_alert_code > 0 then
  call message("This patient has an isolation status, please see the above disease alert field for proper bed placement.")
  end if
end if

'*Warns user and exits conversation if patient has bad debt

dim pers_id    as double
dim encnt_id    as double
dim eid2          as double
dim str1          as string


'Gets person id and encntr_id
pers_id = getvalue("person.person_id")
encnt_id = getvalue("person.encounter.encntr_id")


'Verifies if person exists and user is adding a new encounter 
if pers_id > 0 and encnt_id <= 0 then 
  eid2 = 0 
  str1 = "SELECT A.BAD_DEBT_BALANCE FROM ACCOUNT A, PERSON P, PFT_ACCT_RELTN PFT"
  str1 = str1 & " Plan P WHERE P.PERSON_ID = " & cstr(person_id)
  str1 = str1 & " AND P.ACTIVE_IND = 1"
  str1 = str1 & " AND P.BEG_EFFECTIVE_DT_TM <= CNVTDATETIME(CURDATE, CURTIME3)"
  str1 = str1 & " AND P.END_EFFECTIVE_DT_TM >= CNVTDATETIME(CURDATE, CURTIME3)"
  str1 = str1 & " JOIN PFT WHERE PFT.PARENT_ENTITY_ID = P.PERSON_ID"
  str1 = str1 & " AND PFT.ACTIVE_IND = 1"
  str1 = str1 & " AND PFT.BEG_EFFECTIVE_DT_TM <= CNVTDATETIME(CURDATE, CURTIME3)"
  str1 = str1 & " AND PFT.END_EFFECTIVE_DT_TM >= CNVTDATETIME(CURDATE, CURTIME3)"
  str1 = str1 & " JOIN A WHERE A.ACCT_ID = PFT.ACCT_ID" 
  str1 = str1 & " AND A.BAD_DEBT_BALANCE > 0 AND A.ACTIVE_IND = 1"
  str1 = str1 & " AND A.BEG_EFFECTIVE_DT_TM <= CNVTDATETIME(CURDATE, CURTIME3)"
  str1 = str1 & " AND A.END_EFFECTIVE_DT_TM >= CNVTDATETIME(CURDATE, CURTIME3)"
  eid2 = cdbl(ccl(str1, TRUE)) 

  if eid2 > 0 then 
    call message("This patient has bad debt, please refer them to financial counseling") 
    call exitconversation 
    exit sub 
  end if 
end if