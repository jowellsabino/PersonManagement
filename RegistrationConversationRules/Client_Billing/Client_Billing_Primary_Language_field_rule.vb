'*Requires the "Interpreter required" field if ENGLISH is not selected

dim Language as string
Language = ucase$(trim$(codedisplaykey(currentprompt)))

if Language <> "ENGLISH" then
 call requireprompt("PERSON.PATIENT.INTERP_REQUIRED_CD")
 call changefocus("PERSON.PATIENT.INTERP_REQUIRED_CD")   
else
 call unrequireprompt("PERSON.PATIENT.INTERP_REQUIRED_CD")  
end if