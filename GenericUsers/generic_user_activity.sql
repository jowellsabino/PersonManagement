
select * 
from clinical_event
where event_id = 472563841
;
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
  --   , count(*)
       , p.NAME_FULL_FORMATTED
	   , cep.ACTION_DT_TM
       , cep.*
from ce_event_prsnl cep
join person p
  on p.PERSON_ID = cep.PERSON_ID
join prsnl pruser
  on pruser.PERSON_ID = cep.ACTION_PRSNL_ID
  and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
where  cep.PROXY_PRSNL_ID = 0.0
and cep.valid_until_dt_tm > now() 
and cep.ACTION_STATUS_CD > 0.0
/* and cep.ACTION_DT_TM > now() - 3650 */
and cep.ACTION_PRSNL_ID = 5841539
--group by pruser.PERSON_ID
--     , pruser.NAME_FULL_FORMATTED
--	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;

/* clinical event activity, by proxy */
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
from ce_event_prsnl cep
join person p
  on p.PERSON_ID = cep.PERSON_ID
join prsnl pruser
  on pruser.PERSON_ID = cep.PROXY_PRSNL_ID
  and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
where  cep.ACTION_PRSNL_ID > 0.0
and cep.valid_until_dt_tm > now() 
and cep.ACTION_STATUS_CD > 0.0
/* and cep.ACTION_DT_TM > now() - 3650 */
group by pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;

/* task activity */
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
from task_activity_assignment taa
join task_activity ta
  on ta.task_id = taa.task_id
join person p
  on p.PERSON_ID = ta.PERSON_ID
join prsnl pruser
  on pruser.PERSON_ID = taa.assign_PRSNL_ID
  and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
where  taa.PROXY_PRSNL_ID = 0.0
and taa.beg_eff_dt_tm > now() 
and taa.TASK_STATUS_CD > 0.0
/* and cep.ACTION_DT_TM > now() - 3650 */
group by pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;


/* task activity, by proxy */
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
from task_activity_assignment taa
join task_activity ta
  on ta.task_id = taa.task_id
join person p
  on p.PERSON_ID = ta.PERSON_ID
join prsnl pruser
  on pruser.PERSON_ID = taa.proxy_PRSNL_ID
  and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
where  taa.ASSIGN_PRSNL_ID > 0.0
and taa.beg_eff_dt_tm > now() 
and taa.TASK_STATUS_CD > 0.0
/* and cep.ACTION_DT_TM > now() - 3650 */
group by pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;

/* order activity */
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
from order_action oa
join orders o
  on o.ORDER_ID = oa.ORDER_ID
join person p
  on p.PERSON_ID = o.PERSON_ID
join prsnl pruser
  on pruser.PERSON_ID = oa.ACTION_PERSONNEL_ID 
 and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
group by pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;

/*powerplan activity */
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
from pathway_action pa
join pathway pp
  on pp.pathway_ID = pa.pathway_ID
join person p
  on p.PERSON_ID = pp.PERSON_ID
join prsnl pruser
  on pruser.PERSON_ID = pa.ACTION_PRSNL_ID 
 and (pruser.name_last_key like 'CHB%'
	   or
	   pruser.name_last_key like 'BCH%')
--where pa.ACTION_DT_TM > now() - 365
group by pruser.PERSON_ID
     , pruser.NAME_FULL_FORMATTED
	 , TEST_OR_REAL 
order by TEST_OR_REAL,pruser.NAME_FULL_FORMATTED
;

/*
select p.NAME_FULL_FORMATTED
     , p.CONTRIBUTOR_SYSTEM_CD
     , pruser.NAME_FULL_FORMATTED
     , cep.*
from ce_event_prsnl cep
join person p
  on p.PERSON_ID = cep.PERSON_ID
 and p.NAME_LAST_KEY not like 'SYSTEM%'
 and p.NAME_LAST_KEY not like 'TEST%'
 and p.NAME_LAST_KEY not like 'SYTEM%'
join prsnl pruser
  on pruser.PERSON_ID = cep.ACTION_PRSNL_ID
where cep.PROXY_PRSNL_ID = 0.0
and cep.valid_until_dt_tm > now() 
and cep.ACTION_STATUS_CD > 0.0
-- and cep.ACTION_DT_TM > now() - 365 
and cep.ACTION_PRSNL_ID in (5903548,1087859,9104053)
;

select *
from person
where person_id = 14659266
;

*/