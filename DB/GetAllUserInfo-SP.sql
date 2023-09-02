CREATE PROCEDURE GetAllUserInfo
   @NationalNumber INT
AS
BEGIN
    SELECT
        Users.ID AS UserID,
        Users.Username,
        Users.NationalNumber,
        Users.Email,
        Users.Phone,
        Users.IsActive
    FROM Users  
    WHERE Users.NationalNumber = @NationalNumber;
END;
