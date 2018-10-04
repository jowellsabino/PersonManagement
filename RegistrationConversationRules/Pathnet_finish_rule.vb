'blank CHB MRN from person comment field
if (trim$(cstr(getvalue("person.user_defined.newpersoncom"))) > "") then
call setvalue("person.user_defined.newpersoncom","")
end if

'Rule description: end rule to capitalize fields
'Patient name

Dim pt_last as string
Dim pt_name as string

   pt_last = getvalue("person.current_name.name_last")
   pt_name = ucase$(pt_last)
   call setvalue("person.current_name.name_last",pt_name)

Dim pt_first as string
Dim pt_first_name as string

   pt_first = getvalue("person.current_name.name_first")
   pt_first_name = ucase$(pt_first)
   call setvalue("person.current_name.name_first",pt_first_name)

Dim pt_middle as string
Dim pt_middle_name as string

   pt_middle = getvalue("person.current_name.name_middle")
   pt_middle_name = ucase$(pt_middle)
   call setvalue("person.current_name.name_middle",pt_middle_name)