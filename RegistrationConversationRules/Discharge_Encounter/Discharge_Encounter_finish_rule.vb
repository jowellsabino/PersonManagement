'Comparing dates before exiting
if comparedates("person.encounter.disch_dt_tm" , "person.encounter.reg_dt_tm") = "LESS" then
  call message("Discharge Date/Time must come after the Admit Date/Time.")
  call changefocus("person.encounter.disch_dt_tm")
  exit sub
end if

'Comparing dates before exiting
if comparedates("PERSON.DECEASED_DT_TM" , "person.encounter.reg_dt_tm") = "LESS" then
  call message("Deceased Date/Time must come after the Admit Date/Time.")
  call changefocus("PERSON.DECEASED_DT_TM")
  exit sub
end if