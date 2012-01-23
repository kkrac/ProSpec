echo off
echo Unpacking SampleDb.rar...

SET RarDir=..\..\Tools\rar
SET DbDir=%cd%\

%RarDir%\unrar x -y SampleDb.rar

echo Database backup file unpacked...

echo Restoring Database...
echo on
sqlcmd -S (local)\SqlExpress -i "%cd%\queries\RestoreDb.sql" -v bakPath="'%DbDir%\SampleDb.bak'" mdfPath="'%DbDir%\SampleDB.mdf'" ldfPath="'%DbDir%\SampleDB_log.ldf'"



sqlcmd -S (local)\SqlExpress -i "%cd%\queries\LinkDbLogin.sql"

pause