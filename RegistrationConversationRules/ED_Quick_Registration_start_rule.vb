'Warns user if a patient has an ER active encounter
dim person_id as double
dim encntr_id as double
dim eid       as double
dim class_cd  as double
dim str       as string


'Gets person id and encntr_id
person_id = getvalue("person.person_id")
encntr_id = getvalue("person.encounter.encntr_id")

'Verifies if person exists and user is adding a new encounter
if person_id > 0 and encntr_id <= 0 then
  eid = 0
  'CDF meaning of EMERGENCY must be set in Code Set 71 for Emergency encounter.
  class_cd = meaningcode(71, "EMERGENCY")
  str = "select e.encntr_id from encounter e where e.person_id = " & _
        cstr(person_id) & " and e.encntr_type_cd = "  & _
        cstr(class_cd)  & " and e.disch_dt_tm = NULL" & _
        " and e.active_ind = 1" & _
        " and e.beg_effective_dt_tm <= cnvtdatetime(curdate,curtime3)" & _
        " and e.end_effective_dt_tm >= cnvtdatetime(curdate,curtime3)"
  eid = cdbl(ccl(str,TRUE))

  if eid > 0 then
    call message("Patient selected has an active Emergency encounter.Please select the existing encounter.")
    call exitconversation
      exit sub
    end if
  end if


'downtime MRN and CSN by random selection function
dim dt_mrn as string
dim dt_csn as string
dim str2   as string
dim dup    as string
dim encntrid as double
dim counter as integer
dim min,max
max = 999999
min = 111111
randomize()
'call message("rnd" &rnd)
if encntr_id <= 0 then
   str2=" "
   'str2 = cstr(int(rnd*99999999))
   str2 = cstr((int((max - min+1) *Rnd+min)))
   'call message("encntr str2: " &str2)
   dt_csn = ("DT" &str2)
   'call message("dt_csn:" &dt_csn)
  call setvalue("PERSON.ENCOUNTER.FINNBR.ALIAS",dt_csn) 

end if