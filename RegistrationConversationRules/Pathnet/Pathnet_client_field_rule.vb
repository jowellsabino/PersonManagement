'envoke rule only when adding encounter, not modifying an existing encounter


dim encntr_id as double
encntr_id = getvalue("person.encounter.encntr_id")
if encntr_id = 0 then

'get financial classes from codeset 354
dim orgid as double
dim orgname as string
dim CL as double
dim SF as double
dim QA as double
dim LM as double
dim foundation as string

CL = meaningcode(354, "CL")
SF = meaningcode(354, "SF")
QA = meaningcode(354, "QA")
LM = meaningcode(354, "LM")

'get client organization_id
orgid = getvalue(currentprompt)
orgname = ccl("select o.org_name_key from organization o where o.organization_id = " & orgid, TRUE)
foundation = Instr(1, orgname, "FOUNDATION") 

'assign appropriate financial class for Dana Farber orgs and org names starting CL, SF, QA, LM
if orgname = "DFCIPEDIATRICS" or orgname = "DFCIADULTS" then
  call setvalue("person.encounter.financial_class_cd", cstr(CL))
elseif orgname > "CL" and orgname < "CM" then
  call setvalue("person.encounter.financial_class_cd", cstr(CL))
elseif orgname > "SF" and orgname < "SG" then
  call setvalue("person.encounter.financial_class_cd", cstr(SF))
elseif orgname > "QA" and orgname < "QB" then
  call setvalue("person.encounter.financial_class_cd", cstr(QA))
elseif orgname > "LM" and orgname < "LN" then
  call setvalue("person.encounter.financial_class_cd", cstr(LM))
end if

'if organization is Childrens Hospital Boston, Psych, or a Foundation do the following:
'assign med service of Outpatient Lab
'require FIN
'require MRN
'require location

'Change: 06/19/13 H. Stevens, CHB name change, CHG 158
if orgname = "BOSTONCHILDRENSHOSPITAL" or orgname = "BOSTONCHILDRENSHOSPITALPSYCHIATRIC" or foundation > 0 then
  call setvalue("person.encounter.med_service_cd","3110824")
  call requireprompt("person.encounter.finnbr.alias")
  call requireprompt("person.mrn.alias")
  call requireprompt("person.encounter.loc_nurse_unit_cd")

' Gajens Change: 9/13/2007 for Change Request: CHG000000002267
'if organization is BWH do the following:
'require MRN
elseif orgname = "CLBRIGHAMWOMENSHOSPITAL" then
  call requireprompt("person.mrn.alias")

' Gajens Change: 9/13/2007 for Change Request: CHG000000002267
'if organization is BWH NICU do the following:
'require MRN
elseif orgname = "CLBRIGHAMWOMENSNICU" then
  call requireprompt("person.mrn.alias")

' Change: 12/05/2012 K. OByrne for Change Request: CHG000000053781
'if organization is BWH CF do the following:
'require MRN
elseif orgname = "CLBRIGHAMWOMENSHOSPITALCF" then
  call requireprompt("person.mrn.alias")

' Change: 12/05/2012 K. OByrne for Change Request: CHG000000053781
'if organization is BMC do the following:
'require MRN
elseif orgname = "CLBOSTONMEDICALCENTER" then
  call requireprompt("person.mrn.alias")

'if organization is DFCI Pediatrics do the following:
'require MRN
elseif orgname = "DFCIPEDIATRICS" then
  call requireprompt("person.mrn.alias")

' GS: CHG000000003409
elseif orgname = "DFCIADULTS" then
  call requireprompt("person.mrn.alias")
  
'if organization is Martha Elliot do the following:
'assign med service of MEHC Lab only
'require FIN
'require MRN
elseif orgname="MARTHAELLIOTHEALTHCENTER" then
  call setvalue("person.encounter.med_service_cd","3110825")
  call requireprompt("person.encounter.finnbr.alias")
  call requireprompt("person.mrn.alias")

'if organization is Occ Health then require an MRN
elseif orgname="CLOCCUPATIONALHEALTH" then
  call requireprompt("person.mrn.alias")

'if organization is Cl Occupational Health Associate Personnel then require an MRN
elseif orgname="CLOCCUPATIONALHEALTHASSOCIATEPERSONNEL" then
  call requireprompt("person.mrn.alias")

'if organization is LM 62050 GPP Stdy then require an MRN
elseif orgname = "LM62050GPPSTUDY" then
  call requireprompt("person.mrn.alias")

'otherwise...if organization is not Childrens Hospital Boston, Martha Elliot, Psych, nor a Foundation do the following:
'hide the FIN field
'set the patient type to Client
else
  call hideprompt("person.encounter.finnbr.alias")
  call setvalue("person.encounter.encntr_type_cd", "3724054")
end if

end if