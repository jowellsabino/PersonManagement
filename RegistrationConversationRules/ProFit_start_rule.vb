'*Hides the beggining MSP question
call hideprompt("PERSON.QUESTIONNAIRE_01.QUESTIONS_29.VALUE_CD")
call hideprompt("PERSON.QUESTIONNAIRE_01.QUESTIONS_01.VALUE_CD")


'*Warns user if patient selected has a deceased date/time.

dim death as string
dim str2 as string

str2 = "Please be informed that this patient is already deceased." & Chr(13) & Chr(10) & "Are you sure you need to modify this encounter?"

death = getvalue("person.deceased_dt_tm")

if death <> "0000000000000000" then
  if msgbox(str2, vbYesNo + vbQuestion, "Active Encounter") = vbNo then 
  'call message("This person is already deceased.")
  call exitconversation
  end if
end if