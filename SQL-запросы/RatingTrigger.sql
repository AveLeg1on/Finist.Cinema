CREATE TRIGGER RatingDefault
ON Movies
FOR INSERT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE Rating IS NULL)
    BEGIN
        UPDATE Movies SET Rating = 0 WHERE ID IN (SELECT ID FROM inserted WHERE Rating IS NULL)
    END
END
--Триггер, автоматически устанавливающий рейтинг фильма в 0, если он не указан при добавлении записи: