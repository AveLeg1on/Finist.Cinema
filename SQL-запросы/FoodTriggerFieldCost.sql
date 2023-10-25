CREATE TRIGGER trg_Food_Cost
ON Food
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS(SELECT * FROM inserted WHERE Cost <= 0)
    BEGIN
        RAISERROR('Cost must be greater than zero', 16, 1);
        ROLLBACK TRANSACTION;
    END
END
--Триггер на проверку корректности поля Cost