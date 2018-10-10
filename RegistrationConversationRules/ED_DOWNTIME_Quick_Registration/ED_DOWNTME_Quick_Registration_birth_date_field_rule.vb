dim default_prompt as string
dim daysdiff   as long
dim marital    as double
dim marital_cd as double

'Get currentprompt value
default_prompt  = cstr(getvalue(currentprompt))
daysdiff = comparetocurrentdate(currentprompt)
'Get marital status
marital  = cdbl(getvalue("person.marital_type_cd"))

if default_prompt = "0000000000000000" then
  exit sub

'Validates that the given birth date isn't in the future
elseif futuredate(currentprompt) then
  call message("The date entered is in the future.  Please re-enter the birth date.")
  call setvalue(currentprompt, "0000000000000000")

'Marital Status defaults to "single" if patient age is less than 14.
elseif daysdiff > -5113 and marital = 0 then
  marital_cd = displaykeycode("SINGLE", 38)
  call setvalue("person.marital_type_cd", cdbl(marital_cd))

'Date of birth cannot be greater than 125 years old
elseif daysdiff < -45656 then
  call message("Age cannot be greater than 125 years old.")
  call setvalue(currentprompt, "0000000000000000")
end if