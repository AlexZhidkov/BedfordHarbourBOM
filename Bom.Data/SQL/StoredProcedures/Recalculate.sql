CREATE PROCEDURE Recalculate
	@partId int,
	@ProductsDemand decimal(25,13),
	@ProductsCount int OUTPUT
AS

update Subassemblies set Capability = 0, Demand = 0
update Parts set Capability = 0, Demand = 0

exec dbo.RecalculateRecurse @partId, @ProductsDemand, @ProductsCount output
