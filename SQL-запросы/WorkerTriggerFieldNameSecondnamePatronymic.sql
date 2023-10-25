CREATE TRIGGER CheckWorkerName
ON Worker
AFTER INSERT, UPDATE
AS
BEGIN
  IF EXISTS (SELECT ID FROM inserted WHERE Secondname = Name AND Name = Patronymic)
  BEGIN
    RAISERROR ('Name and surname cannot be the same as patronymic.', 16, 1)
    ROLLBACK TRANSACTION
    RETURN
  END
END
-- триггер провер€ющий  несовпадение имени и фамилии с отчеством