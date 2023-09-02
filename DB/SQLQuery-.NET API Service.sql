USE PSSEmployeesSalaries;
CREATE TABLE Users (
  ID INT PRIMARY KEY, 
  Username VARCHAR(255), 
  NationalNumber INT, 
  Email VARCHAR(255), 
  Phone VARCHAR(20), 
  IsActive BIT, 
  );
SELECT 
  * 
FROM 
  Users;
---------------------------------------------------------
---------------------------------------------------------
CREATE TABLE Salaries (
  ID INT PRIMARY KEY, 
  Year INT, 
  Month VARCHAR(20), 
  Salary INT, 
  UserID INT, 
  FOREIGN KEY (UserID) REFERENCES Users(ID)
);
SELECT 
  * 
FROM 
  Salaries;
---------------------------------------------------------
---------------------------------------------------------
USE PSSEmployeesSalaries;
INSERT INTO Users (
  ID, Username, NationalNumber, Email, 
  Phone
) 
VALUES 
  (
    1, 'Huthaifa Altiti', 123456789, 'huthaifa@example.com', 
    '123-456-7890'
  ), 
  (
    2, 'Qusai Albawaizeh', 987654321, 
    'qusai@example.com', '987-654-3210'
  ), 
  (
    3, 'Bassam Alnabulsi', 456789123, 
    'bassam@example.com', '456-789-1230'
  ), 
  (
    4, 'Lina Ahmad', 789123456, 'lina@example.com', 
    '789-123-4560'
  ), 
  (
    5, 'Mohammed Hassan', 654321987, 'mohammed@example.com', 
    '654-321-9870'
  );
UPDATE 
  Users 
SET 
  IsActive = 1;
UPDATE 
  Users 
SET 
  IsActive = 0 
WHERE 
  ID IN (3, 5);
INSERT INTO Salaries (ID, Year, Month, Salary, UserID) 
VALUES 
  -- Salaries for Huthaifa Altiti (User ID: 1)
  (1, 2023, 'January', 300, 1), 
  (2, 2023, 'February', 300, 1), 
  (3, 2023, 'March', 300, 1), 
  (4, 2023, 'April', 300, 1), 
  (5, 2023, 'May', 300, 1), 
  -- Salaries for Qusai Albawaizeh (User ID: 2)
  (6, 2023, 'January', 1800, 2), 
  (7, 2023, 'February', 1900, 2), 
  (8, 2023, 'March', 1800, 2), 
  (9, 2023, 'April', 1800, 2), 
  (10, 2023, 'May', 1800, 2), 
  -- Salaries for Bassam Alnabulsi (User ID: 3)
  (11, 2023, 'January', 400, 3), 
  (12, 2023, 'February', 405, 3), 
  (13, 2023, 'March', 400, 3), 
  (14, 2023, 'April', 400, 3), 
  (15, 2023, 'May', 400, 3), 
  -- Salaries for Lina Ahmad (User ID: 4)
  (16, 2023, 'January', 500, 4), 
  (17, 2023, 'February', 500, 4), 
  (18, 2023, 'March', 510, 4), 
  (19, 2023, 'April', 500, 4), 
  (20, 2023, 'May', 480, 4), 
  -- Salaries for Mohammed Hassan (User ID: 5)
  (21, 2023, 'January', 700, 5), 
  (22, 2023, 'February', 720, 5), 
  (23, 2023, 'March', 650, 5), 
  (24, 2023, 'April', 450, 5), 
  (25, 2023, 'May', 800, 5);
SELECT 
  * 
FROM 
  Salaries;
SELECT 
  Users.ID AS UserID, 
  Users.Username, 
  Users.NationalNumber, 
  Users.Email, 
  Users.Phone, 
  Salaries.Year, 
  Salaries.Month, 
  Salaries.Salary 
FROM 
  Users 
  INNER JOIN Salaries ON Users.ID = Salaries.UserID;
SELECT 
  Users.ID AS UserID, 
  Users.Username, 
  Users.NationalNumber, 
  Users.Email, 
  Users.Phone, 
  Users.IsActive 
FROM 
  Users 
WHERE 
  Users.NationalNumber = 123456789;
SELECT 
  * 
FROM 
  Users;
SELECT 
  * 
FROM 
  Salaries;
