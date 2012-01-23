echo off
echo Backuping Database...
echo on

SET RarDir=..\..\Tools\rar
SET DbDir=%cd%\

%RarDir%\unrar x -y SampleDb.rar

sqlcmd -S localhost\SqlExpress -i "%cd%\queries\BackupDb.sql" -v bakPath="'%cd%\SampleDb.bak'"

echo Packing SampleDb.bak...

SET RarDir=%cd%\..\..\Tools\rar
echo on
%RarDir%\rar a -y SampleDb.rar SampleDb.bak

del %cd%\SampleDb.bak

echo off
echo Database backed up successfully!

pause