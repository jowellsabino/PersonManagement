dim default_prompt  as string
dim daysdiff as long

'Get currentprompt value
default_prompt  = getvalue(currentprompt)
daysdiff = comparetocurrentdate(currentprompt)


if default_prompt = "0000000000000000" then
  exit sub

'Registration Date/Time cannot be 200 days less than current date/time.
elseif daysdiff < -200 then
  call message("Admit Date/Time cannot be 200 days less than current date/time.")
  call setvalue(currentprompt, "0000000000000000")
  exit sub

'Validates that the given date isn't in the future
elseif futuredate(currentprompt) then
  call message("The date entered is in the future.  Please re-enter the date and time.")
  call setvalue(currentprompt, "0000000000000000")
  exit sub
end if