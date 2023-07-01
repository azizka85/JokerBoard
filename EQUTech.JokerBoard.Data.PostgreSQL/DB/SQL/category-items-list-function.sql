create or replace function category_items_list()
returns setof vw_category_items 
language sql as 
$$
	select *
	from vw_category_items;
$$;