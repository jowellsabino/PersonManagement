'**********
'F-1
'Phone # must be 10 digits for Patient if USA is the phone format
'**********

dim usa_cd as double
dim tmpphn as string
usa_cd = meaningcode(281,"US")

'Patient's Primary Home Phone
tmpphn = getvalue("person.pri_home_phone.phone_num")
if getvalue("person.home_phone.phone_format_cd") = usa_cd then
   if len(tmpphn) > 0 and len(tmpphn) <> 10 then
      call message("Reverify Patient's Phone Number.")
      call changefocus("person.pri_home_phone.phone_num")
   end if
end if