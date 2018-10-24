dim deceased as string

deceased = codedisplaykey("person.encounter.discharge.req_disch_disposition_cd")

if deceased = "EXPIRED" or deceased = "DECEASED" then
call showprompt("person.cause_of_death")
call hideprompt("person.deceased_cd")
call showprompt("person.deceased_dt_tm")
call requireprompt("person.cause_of_death")
call requireprompt("person.deceased_cd")
call requireprompt("person.deceased_dt_tm")
call setvalue("person.deceased_cd", 684729)

else
call hideprompt("person.cause_of_death")
call hideprompt("person.deceased_cd")
call hideprompt("person.deceased_dt_tm")
call unrequireprompt("person.cause_of_death")
call unrequireprompt("person.deceased_cd")
call unrequireprompt("person.deceased_dt_tm")
call setvalue("person.cause_of_death","")
call setvalue("person.deceased_cd","")
call setvalue("person.deceased_dt_tm","")
end if