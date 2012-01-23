BACKUP DATABASE SampleDB
TO DISK = $(bakPath)
WITH NOFORMAT, INIT,  NAME = N'SampleDB', SKIP, NOREWIND, NOUNLOAD,  STATS = 10

print 'Database backed-up successfully...'
