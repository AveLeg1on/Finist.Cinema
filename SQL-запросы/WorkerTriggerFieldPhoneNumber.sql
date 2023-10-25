CREATE TRIGGER trg_Worker_Telephone_Check
ON Worker
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE Telephone NOT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
    BEGIN
        RAISERROR('Incorrect telephone format', 16, 1)
        ROLLBACK
    END
END
--Триггер, который запрещает добавление записи с неправильным номером телефона