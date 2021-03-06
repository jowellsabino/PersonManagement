'*Phone # must be 10 digits for Patient and Guarantor if USA is the phone format

dim tmpphn as string
dim usa_cd as double
usa_cd = meaningcode(281,"US")

'Patient's Home Phone
tmpphn = getvalue("person.home_phone.phone_num")
if getvalue("person.home_phone.phone_format_cd") = usa_cd then
   if len(tmpphn) > 0 and len(tmpphn) <> 10 then
      call message("Patient's home phone number must be 10 digits, please reverify.")
      call changefocus("person.home_phone.phone_num")
     exit sub
   end if
end if

dim tmpphn2 as string
usa_cd = getvalue("PERSON.GUARANTOR_01.PERSON.HOME_ADDRESS.COUNTRY_CD")

'Guarantor's Home Phone
tmpphn2 = getvalue("PERSON.GUARANTOR_01.PERSON.HOME_PHONE.PHONE_NUM")
if usa_cd = 309221 or usa_cd = 309220 then
   if len(tmpphn2) > 0 and len(tmpphn2) <> 10 then
      call message("Guarantor's home phone number must be 10 digits, please reverify.")
      call changefocus("person.guarantor_01.person.home_phone.phone_num")
     exit sub
   end if
end if




'*Rule to consider COB if Self Pay is selected

dim plan_name as string
dim plan_name2 as string

'Get the primary subscriber information
plan_name = ucase$(getvalue("PERSON.SUBSCRIBER_01.PERSON.HEALTH_PLAN.PLAN_INFO.PLAN_NAME"))

'Get the secondary subscriber information
plan_name2 = ucase$(getvalue("PERSON.SUBSCRIBER_02.PERSON.HEALTH_PLAN.PLAN_INFO.PLAN_NAME"))

if plan_name = "SELF PAY" then
   if (plan_name2 > "" and plan_name2 <> "SELF PAY") then
     call message("Please check the COB. Self Pay cannot be primary if Secondary plan is valued.")
     call changefocus("PERSON.ENCOUNTER.USER_DEFINED.NEWINSCOMMEN")
   exit sub
   end if
end if
 
'*Checks that the Guarantor, Patient, and Primary Subsciber have valid SSN #'s

dim tmplen as integer

tmplen = 0

'Patient's SSN
tmplen = len(getvalue("person.ssn.alias"))
if tmplen > 0 and tmplen < 7 then
   call message("Patient's SSN is not valid, please reverify.")
   call changefocus("person.ssn.alias")
  exit sub
end if

'Guarantor's SSN
tmplen = len(getvalue("person.guarantor_01.person.ssn.alias"))
if tmplen > 0 and tmplen < 7 then
   call message("Guarantor's SSN is not valid, please reverify.")
   call changefocus("person.guarantor_01.person.ssn.alias")
  exit sub
end if

'Subscriber 01
tmplen = len(getvalue("person.subscriber_01.person.ssn.alias"))
if tmplen > 0 and tmplen < 7 then
   call message("Primary Subscriber's SSN is not valid, please reverify.")
   call changefocus("person.subscriber_01.person.ssn.alias")
  exit sub
end if



'*Checks that the Guarantor and Primary Subsciber have valid zipcodes if USA is the country


dim tmpzip as string
dim tmpfld as string

'Guarantor Home Zip
tmpfld = "PERSON.GUARANTOR_01.PERSON.HOME_ADDRESS.ZIPCODE"
tmpzip = getvalue(tmpfld)
tmplen = len(tmpzip)
if codedisplaykey(getvalue(parentfieldname(tmpfld) & ".country_cd")) = "USA" then
  if tmplen > 0 and tmplen <> 5 and tmplen <> 9 then
     call message("Guarantor's home zipcode must be 5 or 9 digits.")
     call changefocus(tmpfld)
  exit sub
  end if
end if


'Subscriber 01 Home Zipcode
tmpfld = "PERSON.SUBSCRIBER_01.PERSON.HOME_ADDRESS.ZIPCODE"
tmpzip = getvalue(tmpfld)
tmplen = len(tmpzip)
if codedisplaykey(getvalue(parentfieldname(tmpfld) & ".country_cd")) = "USA" then
  if tmplen > 0 and tmplen <> 5 and tmplen <> 9 then
     call message("Primary Subscriber's home zipcode must be 5 or 9 digits.")
     call changefocus(tmpfld)
  exit sub
  end if
end if


'*Placed in Bed Date/Time must be greater than Admit Date/Time
if comparedates("person.encounter.assign_to_loc_dt_tm", "person.encounter.reg_dt_tm") = "LESS" then
  call message("Placed in Bed Date/Time must be greater than Admit Date/Time.")
  call setvalue("person.encounter.assign_to_loc_dt_tm", "0000000000000000")
  call changefocus("person.encounter.assign_to_loc_dt_tm")
  exit sub
end if

'*clears out the new person comments from the new comment field and adds them to the old person comment field

dim add_field as string
dim old_field as string
dim user as string
dim user_name as string
dim add_comment as string
dim new_comment as string
dim old_comment as string

add_field = "PERSON.USER_DEFINED.NEWPERSONCOM"
old_field = "PERSON.COMMENT_01.LONG_TEXT"

user = getvalue ("TRANSACTION_INFO.PRSNL_ID")

user_name = ccl("select p.name_full_formatted from prsnl p where p.person_id = " & user, TRUE) 

add_comment = getvalue(add_field)
old_comment = getvalue(old_field)

if len(add_comment) > 0 then

new_comment = System.DateTime.Now & "   " & "Comment by: " & user_name & "   "
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment &  add_comment
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment & "--------------------------------------"
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment & old_comment
call setvalue (old_field, new_comment)
call setvalue (add_field, "")
 call setvalue(add_comment, "")
 call setvalue(old_comment, "") 
 call setvalue(new_comment, "")
end if



'*clears out the new HIPAA Contact comments from the new comment field  and adds them to the previous HIPAA comment field

dim add_field2 as string
dim old_field2 as string
dim add_comment2 as string
dim new_comment2 as string
dim old_comment2 as string

add_field2 = "PERSON.ENCOUNTER.USER_DEFINED.NEWHIPAACOMM"
old_field2 = "PERSON.ENCOUNTER.COMMENT_10.LONG_TEXT"
add_comment2 = getvalue(add_field2)
old_comment2 = getvalue(old_field2)

if len(add_comment2) >= 1 then

new_comment2 =  System.DateTime.Now & "   " & "Comment by: " & user_name & "   "
new_comment2 = new_comment2 & chr(13) & chr(10)
new_comment2 = new_comment2 &  add_comment2
new_comment2 = new_comment2 & chr(13) & chr(10)
new_comment2 = new_comment2 & "--------------------------------------"
new_comment2 = new_comment2 & chr(13) & chr(10)
new_comment2 = new_comment2 & chr(13) & chr(10)
new_comment2 = new_comment2 & old_comment2
call setvalue (old_field2, new_comment2)
call setvalue (add_field2, "")
call setvalue(add_comment2, "")
call setvalue(old_comment2,  "") 
call setvalue(new_comment2, "")
end if

'*clears out the new encounter comments from the new encunter comment field and adds them to the old encounter comment field

DIM ADD_FIELD3 AS STRING
DIM OLD_FIELD3 AS STRING
DIM ADD_COMMENT3 AS STRING
DIM NEW_COMMENT3 AS STRING
DIM OLD_COMMENT3 AS STRING

add_field3 = "PERSON.ENCOUNTER.USER_DEFINED.NEWENCNTRCOM"
old_field3 = "PERSON.ENCOUNTER.COMMENT_01.LONG_TEXT"
add_comment3 = getvalue(add_field3)
old_comment3 = getvalue(old_field3)


if len(add_comment3) > 0 then

new_comment3 =  System.DateTime.Now & "   " & "Comment by: " & user_name & "   "
new_comment3 = new_comment3 & chr(13) & chr(10)
new_comment3 = new_comment3 &  add_comment3
new_comment3 = new_comment3 & chr(13) & chr(10)
new_comment3 = new_comment3 & "--------------------------------------"
new_comment3 = new_comment3 & chr(13) & chr(10)
new_comment3 = new_comment3 & chr(13) & chr(10)
new_comment3 = new_comment3 & old_comment3
call setvalue (old_field3, new_comment3)
call setvalue (add_field3, "")
call setvalue(add_comment3, "")
call setvalue(old_comment3, "") 
call setvalue(new_comment3, "")
end if

'*clears out the new insurance comments from the new insurance comment field and adds them to the old insurance comment field


add_field = "PERSON.ENCOUNTER.USER_DEFINED.NEWINSCOMMEN"
old_field = "PERSON.ENCOUNTER.COMMENT_02.LONG_TEXT"

user = getvalue ("TRANSACTION_INFO.PRSNL_ID")

user_name = ccl("select p.name_full_formatted from prsnl p where p.person_id = " & user, TRUE) 
add_comment = getvalue(add_field)
old_comment = getvalue(old_field)


if len(add_comment) < 1 then
exit sub
else
new_comment =  System.DateTime.Now & "   " & "Comment by: " & user_name & "   "
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment &  add_comment
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment & "--------------------------------------"
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment & chr(13) & chr(10)
new_comment = new_comment & old_comment
call setvalue (old_field, new_comment)
call setvalue (add_field, "")
 add_field = ""
 old_field = ""
 add_comment= ""
 old_comment = "" 
 new_comment = ""
end if

'*Verifies that question #1 of the MSP has been answered

dim INSCOMP1   as double

dim x as integer
dim Fieldname as string 

x = 0
For x = 1 to 3
  Fieldname = "person.subscriber_0" & cstr(x) & ".person.health_plan.plan_info.financial_class_cd"

INSCOMP1 = getvalue(fieldname)

if codemeaning(inscomp1) = "MEDICARE" then
   if (getvalue("person.questionnaire_01.questions_01.value_cd") = 0) then
          call changefocus("person.questionnaire_01.questions_01.value_cd")
   end if
end if

next x