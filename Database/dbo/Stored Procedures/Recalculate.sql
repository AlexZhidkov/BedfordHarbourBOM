CREATE PROCEDURE Recalculate
	@partId INT,
	@ProductsDemand DECIMAL(25,13),
	@ProductsCount INT OUT
AS

SET NOCOUNT ON

DECLARE @TotalCost DECIMAL(25,13)

UPDATE Subassemblies SET Capability = 0, Demand = 0
UPDATE Parts SET Capability = 0, Demand = 0

EXEC dbo.RecalculateRecurse @partId, @ProductsDemand, @ProductsCount OUT, @TotalCost OUT