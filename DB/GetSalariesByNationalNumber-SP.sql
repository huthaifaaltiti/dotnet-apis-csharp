CREATE PROCEDURE GetSalariesByNationalNumber
    @NationalNumber INT
AS
BEGIN
    SELECT Salaries.Salary
    FROM Users
    INNER JOIN Salaries ON Users.ID = Salaries.UserID
    WHERE Users.NationalNumber = @NationalNumber;
END;
