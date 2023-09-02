CREATE PROCEDURE GetAllUsersInfo
AS
BEGIN
    SELECT
        Users.ID AS UserID,
        Users.Username,
        Users.NationalNumber,
        Users.Email,
        Users.Phone,
        Users.IsActive
    FROM
        Users;
END;
