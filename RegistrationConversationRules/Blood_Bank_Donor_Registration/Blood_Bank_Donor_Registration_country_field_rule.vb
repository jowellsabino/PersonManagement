'*Sets the patient's home phone to the USA format (###-###-####)
'*if USA or Canada is selected as the country

dim parent  as string
dim country as string
dim usa     as double
dim freetxt as double

'Get the display value for the country
parent  = parentfieldname(parentfieldname(currentprompt))
country = ucase$(trim$(codedisplaykey(currentprompt)))
usa     = meaningcode(281,"US")
freetxt = meaningcode(281,"FREETEXT")

'Toggle prompts according to country
select case country
  case "CANADA"
    call setvalue(parent & ".home_phone.phone_format_cd",usa)
    call setvalue(parent & ".bus_phone.phone_format_cd",usa)
    call requireprompt(PARENT & ".HOME_ADDRESS.ZIPCODE")
    call requireprompt(PARENT & ".HOME_ADDRESS.CITY")
    call requireprompt(PARENT & ".HOME_ADDRESS.STATE_CD")
    call requireprompt(PARENT & ".HOME_ADDRESS.COUNTY_CD")
    call requireprompt(PARENT & ".HOME_PHONE.PHONE_NUM")

  case "USA"
    call setvalue(parent & ".home_phone.phone_format_cd",usa)
    call setvalue(parent & ".bus_phone.phone_format_cd",usa)
    call requireprompt(PARENT & ".HOME_ADDRESS.ZIPCODE")
    call requireprompt(PARENT & ".HOME_ADDRESS.CITY")
    call requireprompt(PARENT & ".HOME_ADDRESS.STATE_CD")
    call requireprompt(PARENT & ".HOME_ADDRESS.COUNTY_CD")
    call requireprompt(PARENT & ".HOME_PHONE.PHONE_NUM")

  case else
    call setvalue(parent & ".home_phone.phone_format_cd",freetxt)
    call setvalue(parent & ".bus_phone.phone_format_cd",freetxt)
    call unrequireprompt(PARENT & ".HOME_ADDRESS.ZIPCODE")
    call unrequireprompt(PARENT & ".HOME_ADDRESS.STATE_CD")
    call unrequireprompt(PARENT & ".HOME_ADDRESS.COUNTY_CD")
 
end select