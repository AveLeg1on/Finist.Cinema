CREATE TRIGGER BudgetCheck
ON Movies
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE Budget < 0)
    BEGIN
        RAISERROR ('Budget cannot be negative.', 16, 1)
        ROLLBACK TRANSACTION
    END
END