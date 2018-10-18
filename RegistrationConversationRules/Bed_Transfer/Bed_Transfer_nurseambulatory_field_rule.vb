'***********************************************
'Requires room and bed fields if the CDF meaning
'of the currentprompt (nurse unit) = "NURSEUNIT" 
'***********************************************

dim unit as double
unit = cdbl(getvalue(currentprompt))

if codemeaning(unit) = "NURSEUNIT" then
  call requireprompt("person.encounter.transfer.loc_room_cd")
  call requireprompt("person.encounter.transfer.loc_bed_cd")
  call requireprompt("person.encounter.transfer.req_accommodation_cd")
else
  call unrequireprompt("person.encounter.transfer.loc_room_cd")
  call unrequireprompt("person.encounter.transfer.loc_bed_cd")
  call unrequireprompt("person.encounter.transfer.req_accommodation_cd")
end if