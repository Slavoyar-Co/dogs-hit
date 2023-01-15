CREATE TABLE "user" (
	id serial primary key,
	name varchar(50),
	email varchar(50),
	login varchar(50),
	password varchar(50),
	imagePath varchar,
	mapColor int
);

CREATE TABLE dog(
	id serial primary key,
	name varchar(50),
	breed varchar(50),
	age int, 
	imagePath varchar,
	owner int,
	foreign key (owner) references "user"(id)
);

CREATE TABLE territory(
	id serial primary key,
	lattitude float,
	longitude float, 
	claimedBy int,
	foreign key (claimedBy) references dog(id)
);


CREATE TABLE territoryHistory(
	id serial primary key,
	imagePath varchar,
	claimTime date,
	territoryId int,
	claimerId int,
	foreign key (claimerID) references "user"(id)
	
);


