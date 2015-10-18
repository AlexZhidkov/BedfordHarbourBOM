
CREATE PROCEDURE RecalculateRecurse
	@partId INT,
	@ProductsDemand DECIMAL(25,13),
	@ProductsCount INT OUTPUT,
	@TotalCost DECIMAL(25,13) OUTPUT
AS
SET NOCOUNT ON

DECLARE @subassembliesId INT
DECLARE @subPartId INT
DECLARE @costContribution DECIMAL(25,13)
DECLARE @demand INT
DECLARE @count INT
DECLARE @CurrDemand DECIMAL(25,13)
DECLARE @CurrCapability INT
DECLARE @MinProductsCount INT

--from costsforassembly
DECLARE @ownCost DECIMAL(25,13)
DECLARE @CurrCost DECIMAL(25,13)
DECLARE @isTaken INT

--list of subparts
DECLARE curP CURSOR LOCAL FOR
	SELECT s.Id, s.SubassemblyId, s.CostContribution, p.Count, p.OwnCost
	FROM Subassemblies s
		INNER JOIN Parts p ON p.Id = s.SubassemblyId
	WHERE AssemblyId = @partId

SET @MinProductsCount = 999999999
SET @ProductsCount = 0
SET @TotalCost = 0

OPEN curP
FETCH NEXT FROM curP INTO @subassembliesId, @subPartId, @costContribution, @count, @ownCost

WHILE @@FETCH_STATUS = 0 
BEGIN
	SET @CurrDemand = @ProductsDemand * @costContribution

	EXEC dbo.RecalculateRecurse @subPartId, @CurrDemand, @CurrCapability OUTPUT, @CurrCost OUTPUT

	SET @costContribution = IIF(@costContribution = 0, 1, @costContribution)
	SET @ProductsCount = (@count + @CurrCapability) / @costContribution
	SET @MinProductsCount = IIF(@ProductsCount>@MinProductsCount, @MinProductsCount, @ProductsCount)

	IF @CurrCost = 0 -- cost of the subPartId sub-tree is 0, consider sub-part's own cost
		SET @CurrCost = @ownCost * @costContribution

	SET @TotalCost = @TotalCost + (@CurrCost * @costContribution)
	
	UPDATE Subassemblies 
	SET
		Capability = @ProductsCount,
		Demand = @CurrDemand,
		InheritedCost = @CurrCost
	WHERE Id = @subassembliesId

FETCH NEXT FROM curP INTO @subassembliesId, @subPartId, @costContribution, @count, @ownCost
END -- End of Fetch

CLOSE curP
DEALLOCATE curP

SET @MinProductsCount = IIF(@MinProductsCount = 999999999, 0, @MinProductsCount)

UPDATE Parts 
SET
	Capability = @MinProductsCount,
	Demand = Demand + CEILING(@ProductsDemand),
	ComponentsCost = @TotalCost
WHERE Id = @partId

-- consider own cost of current part (root for the sub-parts tree in the Fetch loop)
SELECT @TotalCost = ComponentsCost  + OwnCost 
FROM Parts 
WHERE Id = @partId