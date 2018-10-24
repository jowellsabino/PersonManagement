dim disch as string

disch = getvalue("person.encounter.reg_dt_tm")

if disch = "0000000000000000" then
  call message("This patient has not been fully registered.  Please fully register prior to discharging.")
call exitconversation
exit sub

end if