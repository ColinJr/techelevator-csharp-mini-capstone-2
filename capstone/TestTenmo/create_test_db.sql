
IF DB_ID('tenmoTesting') IS NOT NULL
BEGIN
	ALTER DATABASE tenmoTesting SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE tenmoTesting;
END

CREATE DATABASE tenmoTesting;
