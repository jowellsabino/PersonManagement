'hide additional provider type field
   call hideprompt("person.encounter.doc_01.encntr_prsnl_r_cd")

'pull CHB MRN into person comment field
   dim alias_mrn as string
   dim person_id as double
   person_id = cdbl(getvalue("person.person_id"))


   alias_mrn = ccl("select pa.alias from person_alias pa where pa.active_ind=1 and pa.alias_pool_cd= 3110551 and pa.person_id = " & cstr(person_id), TRUE)

   call setvalue("person.user_defined.newpersoncom",cstr(alias_mrn))