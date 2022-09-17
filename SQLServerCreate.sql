CREATE TABLE userinfo (
  Id INT NOT NULL PRIMARY KEY,
  FirstName VARCHAR(45) NOT NULL,
  LastName VARCHAR(45) NOT NULL,
  Username VARCHAR(45) NOT NULL,
  Password VARCHAR(45) NOT NULL,
  EnrollmentDate datetime NOT NULL
);