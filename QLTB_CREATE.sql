create database QuanLyTrangThietBi
go

use QuanLyTrangThietBi
go

create table ACCOUNT
(
	accountId int identity primary key,
	username varchar(50) not null,
	password varchar(1000) not null,
	role int not null, --0: USER, 1: ADMIN
	fullName nvarchar(50),
	class varchar(50),
	faculty nvarchar(50)
)
go

create table ZONE
(
	zoneId nvarchar(50) primary key,
	zoneName nvarchar(50)
)

create table ROOM
(
	roomId nvarchar(50) primary key, --F110
	zoneId nvarchar(50) foreign key references ZONE (zoneId) on delete cascade on update cascade,
	roomFunction nvarchar(100)
)
go

create table EQUIPMENT
(
	equipmentId nvarchar(50) primary key,
	roomId nvarchar(50) foreign key  references ROOM (roomId) on delete cascade on update cascade not null,
	equipmentName nvarchar(50),
	dateOfInstallation date,
	company nvarchar(50)
)
go

create table STATUS
(
	statusId nvarchar(50) primary key,
	equipmentId nvarchar(50) foreign key references EQUIPMENT (equipmentId) on delete cascade on update cascade not null,
	equipmentStatus nvarchar(50)
)
go

create table REPORT
(
	reportId int identity primary key,
	accountId int foreign key references ACCOUNT (accountId) not null,
	roomId nvarchar(50) foreign key references ROOM (roomId) not null,
	equipmentId nvarchar(50) foreign key references EQUIPMENT (equipmentId) not null,
	statusId nvarchar(50) foreign key references STATUS (statusId) not null,
	note ntext,
	reportStatus int default 0, --0: ch?a ???c nh?n tin, 1: ch?a x? l�, 2: ?� x? l�, 3: b�o c�o sai
	reportedDate datetime default getDate(),
	isEdit bit default 1 --0: kh�ng ???c ch?nh s?a, 1: ???c ph�p ch?nh s?a
)
go

create table RESPONSE
(
	responseId int identity primary key,
	accountId int foreign key references ACCOUNT (accountId) not null,
	reportId int foreign key references REPORT (reportId) not null,
	message ntext not null,
	responseType int not null, --1: ?� nh?n tin, 2: ?� x? l�, 3: th�ng tin c?a b�o c�o sai
	responsedDate datetime default getDate()
)
go

--trigger for insert 1 record to RESPONSE table
create trigger trigger_response_insert
on RESPONSE
for insert
as
begin
	declare @responseId int
	declare @reportId int
	declare @responseType int
	select @responseId = responseId, @reportId = reportId, @responseType = responseType from inserted
	if @responseType = 1
		begin
			update REPORT set reportStatus = 1 where reportId = @reportId
			update REPORT set isEdit = 0 where reportId = @reportId
		end
	else if @responseType = 2
		begin
			update REPORT set reportStatus = 2 where reportId = @reportId
			update REPORT set isEdit = 0 where reportId = @reportId
		end
	else if @responseType = 3
		begin
			update REPORT set reportStatus = 3 where reportId = @reportId
			update REPORT set isEdit = 0 where reportId = @reportId
		end
	else
		rollback transaction
end