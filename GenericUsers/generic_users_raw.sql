SELECT pr.person_id
     , pr.name_full_formatted
     , pr.username
     , cvpos.display AS User_Position
     , eau.directory_ind AS LDAP
     , pr.active_ind
     , pr.end_effective_dt_tm
     , cvcc.display AS Contributor_System
     , prup.name_full_formatted AS Updated_By 
     , pr.updt_dt_tm
     , prcr.name_full_formatted AS Created_By
     , pr.create_dt_tm
     , eau.password_hash
     , eau.password_change_dt_tm
     , eau.password_lifetime
     , pr.*
     , eau.*
 FROM v500.prsnl pr
 LEFT JOIN code_value cvpos 
        ON cvpos.code_value = pr.position_cd
 LEFT JOIN code_value cvcc 
        ON cvcc.code_value = pr.contributor_system_cd
 LEFT JOIN v500.prsnl prup
        ON prup.person_id = pr.updt_id
 LEFT JOIN v500.prsnl prcr
        ON prcr.person_id = pr.create_prsnl_id
INNER JOIN v500.ea_user eau
        ON eau.username = pr.username
   /*   and eau.directory_ind != -1) ;; since authview default is direcoty = Y, directory_ind = 0 is also LDAP */
WHERE pr.name_last_key LIKE 'CHB%' OR pr.name_last_key LIKE 'BCH%'
  AND pr.name_first_key is not null
  AND pr.person_id > 0
ORDER BY pr.contributor_system_cd
       , eau.directory_ind DESC
       , pr.name_last_key
       , pr.name_first_key
;