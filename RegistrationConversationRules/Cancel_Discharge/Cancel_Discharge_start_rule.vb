'***************************************************************
'This rule is appropriate to add if your business office dictates that you need to readmit if 
'the amount of time has been greater than a defined amount
'discharge can't be cancelled 12 hours past disch date
'****************************************************************

'dim DaysDiff as long

'DaysDiff = CompareToCurrentDate("person.encounter.disch_dt_tm")
'if DaysDiff < -0.5 then
  'call message("You can't make changes 12 hours after discharge. Please contact your supervisor.")
 'call exitconversation
  'exit sub
'end if