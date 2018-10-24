dim default_prompt    as string
dim daysdiff   as long


'Get currentprompt value
default_prompt  = cstr(getvalue(currentprompt))
daysdiff = comparetocurrentdate(currentprompt)


if default_prompt = "0000000000000000" then
  exit sub

'Validates that the given birth date isn't in the future
elseif futuredate(currentprompt) then
  call message("The date entered is in the future.  Please re-enter the birth date.")
  call setvalue(currentprompt, "0000000000000000")

'Date of birth cannot be greater than 125 years old
elseif daysdiff < -45656 then
  call message("Age cannot be greater than 125 years old.")
  call setvalue(currentprompt, "0000000000000000")
end if