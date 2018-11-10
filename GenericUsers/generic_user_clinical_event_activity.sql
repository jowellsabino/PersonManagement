/* clinical event activity */
select pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 ,  (case
        when p.NAME_LAST_KEY like 'SYSTEM%'
		     or 
			 p.NAME_LAST_KEY like 'TEST%'
			 or 
			 p.NAME_LAST_KEY like 'SYTEM%'
        then 'TEST'
        else '**REAL**'                                 
        end) as TEST_OR_REAL 
       , count(*)
--      , p.NAME_FULL_FORMATTED
--	    , cep.ACTION_DT_TM
--      , cep.*
from ads_v500_stage.ADMIN.ce_event_prsnl cep
join ads_v500_stage.ADMIN.person p
  on p.PERSON_ID = cep.PERSON_ID
join ads_v500_stage.ADMIN.prsnl pruser
  on pruser.PERSON_ID = cep.ACTION_PRSNL_ID
  and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
where  cep.PROXY_PRSNL_ID = 0.0
and cep.valid_until_dt_tm > now() 
and cep.ACTION_STATUS_CD > 0.0
/* and cep.ACTION_DT_TM > now() - 3650 */
-- and cep.ACTION_PRSNL_ID = 5841539
group by pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;