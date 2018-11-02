dim eid as double
dim fin_no as double
dim fin_len as integer

eid = getvalue("PERSON.ENCOUNTER.ENCNTR_ID")
fin_no = cdbl(getvalue("PERSON.ENCOUNTER.FINNBR.ALIAS"))
fin_len = len(getvalue("PERSON.ENCOUNTER.FINNBR.ALIAS"))

'call message(cstr(vid_no))
if (eid <= 0) then 
  if (fin_len <> 8 or fin_no < 61250001 or fin_no > 61350001 and fin_no <> 0) then 
    call warning("The Financial Number you entered is not a valid Downtime number. Please re-enter.")
    call setvalue("PERSON.ENCOUNTER.FINNBR.ALIAS", "")
    call changefocus("PERSON.ENCOUNTER.FINNBR.ALIAS")
  end if 
elseif (eid > 0 and fin_no > 0) then 
  call disableprompt("PERSON.ENCOUNTER.FINNBR.ALIAS")
end if

