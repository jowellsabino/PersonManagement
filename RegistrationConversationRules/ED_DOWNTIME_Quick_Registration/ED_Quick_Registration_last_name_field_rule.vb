'*Remove any blank spaces before name

dim clean as string

clean = trim$(cstr(getvalue(currentprompt)))
call setvalue(currentprompt, clean)

'*Do not allow non-alphanumeric characters in the field
dim name as string
dim cnt       as double
dim check     as string

'Get string value of prompt
name = ucase$(trim$(cstr(getvalue(currentprompt))))

'Check logic
for cnt = 1 to len(name)
  check = mid(name, cnt, 1)
  select case check
    case "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", "[", "{", "]", "}", "\", "|", ";", ":", "'", ",", "<", ".", ">", "/", "?", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
      call message("Name fields must be alpha entry only. Please re-enter.")
      call setvalue(currentprompt, "")
      exit for
  end select
next