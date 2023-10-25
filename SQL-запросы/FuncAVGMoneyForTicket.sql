CREATE FUNCTION dbo.GetAverageTicketPrice ()
RETURNS int
AS
BEGIN
    DECLARE @avgPrice int
    SELECT @avgPrice = AVG(TotalSum)
    FROM dbo.Ticket

    RETURN @avgPrice
END