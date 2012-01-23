--1) Switch Context
use master;

--2) Drop SampleDB
IF db_id('SampleDB') IS NOT NULL
	drop database SampleDB;

--3) Restore FileList
RESTORE FILELISTONLY 
FROM DISK = $(bakPath);

--4) Restore DB
RESTORE DATABASE SampleDB
FROM DISK = $(bakPath)
	 WITH MOVE 'SampleDB'	 TO $(mdfPath),
		  MOVE 'SampleDB_log' TO $(ldfPath);

--5) Modify DB logic name
--ALTER DATABASE SampleDB
--MODIFY FILE
--(NAME = Sample, NEWNAME='SampleDB')

----6) Modify DB Log logic name
--ALTER DATABASE SampleDB
--MODIFY FILE
--(NAME = Sample_log, NEWNAME='SampleDB_log')

--7) Drop admin Login
--If Exists (select 'admin' from syslogins where name = 'admin' and dbname = 'SampleDB')
If Exists (select * from syslogins where name = 'admin')
    DROP LOGIN admin

--8) Create Login
if not Exists (select 1 from syslogins where name='admin')
    CREATE LOGIN admin WITH PASSWORD = 'admin',    CHECK_POLICY = OFF, CHECK_EXPIRATION=OFF, DEFAULT_DATABASE=[SampleDB];
    --EXEC sys.sp_addsrvrolemember @loginame = N'admin', @rolename = N'sysadmin';

--9) Grant DB Access
--if not Exists (select 1 from syslogins where name='admin')
    exec sp_grantdbaccess 'admin', 'SampleDB';

print 'Database restored successfully...'