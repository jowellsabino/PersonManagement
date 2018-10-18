'*Unrequires SSN field if no SSN field is valued

dim ssn as double
ssn = getvalue("person.user_defined.nossnreason")

if ssn > 0 then 
call unrequireprompt("person.ssn.alias")
call setvalue("person.ssn.alias", "")
else
call requireprompt("person.ssn.alias")
end if