

create table countries
(
	id integer primary key,
	shortname varchar(2),
	name varchar(150),
	phonecode integer,
	fromtime varchar(50),
	totime varchar(50)
);
insert into countries  values (101, 'IN', 'India', 91,'10am','1pm');
insert into countries  values (11, 'AM', 'Armenia', 374,'10am','1pm');
insert into countries  values (13, 'AU', 'Australia', 61,'1am','10pm');
Commit;