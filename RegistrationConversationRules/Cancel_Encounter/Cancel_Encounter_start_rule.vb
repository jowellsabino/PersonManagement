'************************************************************************
'Will not allow the encounter to be cancelled if there are any active orders or any charges attached to the encounter
'************************************************************************
'Check for orders and charges on encounter before allowing the cancel
dim person_id as double
dim encntr_id as double
dim order as double
dim order_str as string
dim Charges as double
dim Charges_str as string
person_id=cdbl(getvalue("person.person_id"))
encntr_id=cdbl(getvalue("person.encounter.encntr_id"))
dim order_cd    as double
dim str         as string
dim complete_cd as string
dim canceled_cd as string
dim cancel_error as string

charges_str="select ch.charge_item_id from charge ch where ch.person_id=" & cstr(person_id) & " and ch.encntr_id=" & cstr(encntr_id)

order_str="select o.order_id from orders o where o.person_id=" & cstr(person_id) & " and o.encntr_id=" & cstr(encntr_id)

Charges=cdbl(ccl(charges_str,true))

order=cdbl(ccl(order_str,true))
order_cd = 0
complete_cd = cstr(meaningcode(6004, "COMPLETED"))
canceled_cd = cstr(meaningcode(6004, "CANCELED"))

  str = "select o.order_id from orders o"
  str = str & " where o.person_id = " & cstr(person_id)
  str = str & " and o.encntr_id = " & cstr(encntr_id)
  str = str & " and (o.order_status_cd != " & complete_cd
  str = str & " and o.order_status_cd != " & canceled_cd & ")"
  str = str & " and o.active_ind = 1"
  str = str & " with maxqual(o,1)"
  order_cd = cdbl(ccl(str,TRUE))

If Charges>0 then
   Cancel_error="This encounter can not be canceled because it has Charges" & chr(13) & _
                "associated to it." & chr(13) & chr(13) & _
                "Data and Records Services must cancel encounters with orders and/or charges." & chr(13) & chr(13) & _
                "Confirm you have selected the correct encounter." & chr(13) & chr(13) & _
                "If you need this encounter canceled, please call the Help Desk with the following" & chr(13) & _
                "information:" & chr(13) & chr(13) & _
                "1.  Patient name, medical record number and account number." & chr(13) & _
                "2.  The reason for canceling the encounter." & chr(13) & _
                "3.  Alert header."
   msgbox(Cancel_error,vbOKOnly,"Cancel Encounter Alert - Charges")
   call exitconversation
   exit sub
else

If order>0 then
    Cancel_error="This encounter can not be canceled because it has Order activity" & chr(13) & _
                "associated to it." & chr(13) & chr(13) & _
                "Data and Records Services must cancel encounters with orders and/or charges." & chr(13) & chr(13) & _
                "Confirm you have selected the correct encounter." & chr(13) & chr(13) & _
                "If you need this encounter canceled, please call the Help Desk with the following" & chr(13) & _
                "information:" & chr(13) & chr(13) & _
                "1.  Patient name, medical record number and account number." & chr(13) & _
                "2.  The reason for canceling the encounter." & chr(13) & _
                "3.  Alert header."
   msgbox(Cancel_error,vbOKOnly,"Cancel Encounter Alert - Orders")
   call exitconversation
   exit sub
 end if 
end if