create database FutbolDB;

use FutbolDB;

create table Teams_tbl
(
	TeamId int identity(1,1) not null,
	TeamName varchar(25) not null,
	TeamCountry varchar(25) not null,
	TeamFlag varchar(1100),
	TeamJersey varchar(1100),
	constraint pk_teamId primary key(TeamId),
	constraint unk_teamName unique(TeamName),
	constraint chk_teamName check (len(TeamName) > 3),
	constraint chk_teamCountry check (len(TeamCountry) > 3),
);


select * from Teams_tbl;
exec sp_rename 'Teams_tbl.TeamFlag','TeamLogo';
update Teams_tbl set TeamFlag='https://images.mlssoccer.com/image/upload/t_q-best/v1614970752/assets/logos/1897-houston-logo_mwdpxd.png' where TeamId=10;
select Teams_tbl.TeamId,TeamName,TeamState, Players_tbl.PlayerId,PlayerName,PlayerPosition,PlayerImage,JerseyNumber from Teams_tbl,Players_tbl where TeamName='Charlotte FC' and Teams_tbl.TeamId=Players_tbl.PlayerTeamId
select TeamId from Teams_tbl where TeamName='FC Dallas'


create table Players_tbl
(
	PlayerId int identity(1,1) not null,
	PlayerName varchar(25) not null,
	PlayerPosition varchar(15) not null,
	PlayerTeamId int not null,
	PlayerImage varchar(1100),
	constraint pk_playerId primary key(PlayerId),
	constraint unk_playerName unique(PlayerName),
	constraint fk_playerTeamId foreign key(PlayerTeamId) references Teams_tbl
);
insert into Players_tbl values('Harrison Afful','Defender',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/wleqeoq1smrc9cfnhyj2.png',25)
insert into Players_tbl values('Jordy Alcívar','Midfielder',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/cftk4zto4buekq5qyizr.png',8)
insert into Players_tbl values('Adam Armour','Defender',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/kqintmvcwtp2mgwccjnp.png',3)
insert into Players_tbl values('Benjamin Bender','Midfielder',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/yrry0020wkwznhgvrsns.png',15)
insert into Players_tbl values('Brandt Bronico','Midfielder',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/cpfxz7iwg2bo4hwayucd.png',13)
insert into Players_tbl values('Guzmán Corujo','Defender',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/yrrqrd8ojpdrw1kzajzo.png',4)
insert into Players_tbl values('Alan Franco','Midfielder',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/eozeqxt3nfxpjd6a5nqz.png',21)
insert into Players_tbl values('Christian Fuchs','Defender',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/g4w1ppugouqiacsn69dv.png',22)
insert into Players_tbl values('McKinze Gaines','Forward',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/xtpqvnppaf944jcctsrf.png',17)
insert into Players_tbl values('Christopher Hegardt','Midfielder',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/fr9t5yjevw7gtpjuqypn.png',19)
insert into Players_tbl values('Kristijan Kahlina','Goalkeeper',(select TeamId from Teams_tbl where TeamName='Charlotte FC'),'https://images.mlssoccer.com/image/private/t_editorial_squared_6_desktop/f_png/prd-league/lthbtnyw73eesb1qiibz.png',1)

select Teams_tbl.TeamId,TeamName,TeamState, Players_tbl.PlayerId,PlayerName,PlayerPosition,PlayerImage,JerseyNumber from Teams_tbl,Players_tbl where PlayerName='Thiago Almada' and Teams_tbl.TeamId=Players_tbl.PlayerTeamId
select Teams_tbl.TeamId,TeamName,TeamState, Players_tbl.PlayerId,PlayerName,PlayerPosition,PlayerImage,JerseyNumber from Teams_tbl,Players_tbl where PlayerPosition='Goalkeeper' and Teams_tbl.TeamId=Players_tbl.PlayerTeamId order by Teams_tbl.TeamId

update Players_tbl set PlayerTeamId=(select TeamId from Teams_tbl where TeamName='Atlanta United') where PlayerName='Thiago Almada'
alter table Players_tbl add JerseyNumber int
update Players_tbl set JerseyNumber=16 where PlayerId=2
select * from Players_tbl;

