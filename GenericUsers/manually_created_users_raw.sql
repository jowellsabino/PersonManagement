/* Raw data, w/o regard for active_ind, end_effecive_dt_tm or position_cd*/
SELECT pr.person_id
     , pr.name_full_formatted
     , pr.username 
     , cvpos.display AS position_user 
     , eau.directory_ind AS LDAP
     , pr.active_ind
     , pr.end_effective_dt_tm
     , prup.name_full_formatted AS Updated_By
     , pr.updt_dt_tm
     , prcr.name_full_formatted AS Created_By
     , pr.create_dt_tm 
     , pr.updt_task 
FROM v500.prsnl pr
LEFT   
JOIN code_value cvpos
  ON cvpos.code_value = pr.position_cd  
 AND cvpos.code_set = 88   
  
JOIN prsnl prup
  ON prup.person_id = pr.updt_id
 
JOIN prsnl prcr
  ON prcr.person_id = pr.create_prsnl_id
 
JOIN ea_user eau
  ON eau.username = pr.username
 /* and eau.directory_ind != -1) ;; since authview default is direcoty = Y, directory_ind = 0 is also LDAP */
 
WHERE pr.position_cd > 0
  AND pr.contributor_system_cd = 0
  AND pr.active_ind = 1
  /* AND pr.end_effective_dt_tm > sysdate */
  AND pr.updt_id > 0
  AND pr.create_prsnl_id > 0 
ORDER BY pr.contributor_system_cd,eau.directory_ind desc, pr.name_last_key, pr.name_first_key