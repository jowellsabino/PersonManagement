'*Does not allow "SELF" to be selected as the relationship

dim parent as string
dim reltn_cd as double

parent = parentfieldname(currentprompt)
reltn_cd = cdbl(getvalue(currentprompt)) 

if reltn_cd > 0 then 
   if ucase$(trim$(codedisplaykey(reltn_cd)))="SELF" then
      CALL MESSAGE("Self cannot be used as Emergency Contact. Please re-enter.")
      CALL SETVALUE(CURRENTPROMPT,0)
      CALL SETVALUE(PARENT & ".person_id","")
      CALL SETVALUE(PARENT & ".PERSON.CURRENT_NAME.NAME_LAST", "")
      CALL SETVALUE(PARENT & ".PERSON.CURRENT_NAME.NAME_FIRST", "")
      CALL SETVALUE(PARENT & ".PERSON.BIRTH_DT_TM", "0000000000000000") 
      CALL SETVALUE(PARENT & ".PERSON.SEX_CD", 0)
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.STREET_ADDR", "")
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.STREET_ADDR2", "")
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.COUNTRY_CD", 0)
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.ZIPCODE", "")
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.CITY", "")
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.STATE_CD", 0)
      CALL SETVALUE(PARENT & ".PERSON.HOME_ADDRESS.COUNTY_CD", 0)
      CALL SETVALUE(PARENT & ".PERSON.HOME_PHONE.PHONE_NUM", "")
      CALL SETVALUE(PARENT & ".PERSON.ALT_PHONE.PHONE_NUM", "")
      CALL SETVALUE(PARENT & ".PERSON.ALT_PHONE.EXTENSION", "")
     ' JS: this procedure exitsub is not defined anywhere, will comment out for now. 
     'call exitsub
'elseif ucase$(trim$(codedisplaykey(reltn_cd)))<> "SELF" then

'Call InvokeSearch("person.emc.person.person_id")

   end if
end if


