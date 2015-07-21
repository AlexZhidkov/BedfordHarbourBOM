
CREATE PROCEDURE RecalculateRecurse
	@partId int,
	@ProductsDemand decimal(25,13),
	@ProductsCount int OUTPUT
AS

Declare @subassembliesId int
Declare @subPartId int
Declare @costContribution decimal(25,13)
Declare @demand int
Declare @count int
Declare @CurrDemand decimal(25,13)
Declare @CurrCapabitilty int
Declare @MinProductsCount int

Declare curP cursor local For
	select s.Id, SubassemblyId, CostContribution, p.Count
	from Subassemblies s
	inner join Parts p on p.Id = s.SubassemblyId
	where AssemblyId = @partId

set @MinProductsCount = 999999999
set @ProductsCount = 0
OPEN curP 
Fetch Next From curP Into @subassembliesId, @subPartId, @costContribution, @count

While @@Fetch_Status = 0 Begin
	set @CurrDemand = @ProductsDemand * @costContribution

	exec dbo.RecalculateRecurse @subPartId, @CurrDemand, @CurrCapabitilty output

	set @costContribution = IIF(@costContribution = 0, 1, @costContribution)
	set @ProductsCount = (@count + @CurrCapabitilty) / @costContribution
	set @MinProductsCount = IIF(@ProductsCount>@MinProductsCount, @MinProductsCount, @ProductsCount)
	
	update Subassemblies set Capability = @ProductsCount, Demand = @CurrDemand
		where Id = @subassembliesId

Fetch Next From curP Into @subassembliesId, @subPartId, @costContribution, @count
End -- End of Fetch

Close curP
Deallocate curP

set @MinProductsCount = IIF(@MinProductsCount = 999999999, 0, @MinProductsCount)
update Parts set Capability = @MinProductsCount, Demand = Demand + CEILING(@ProductsDemand)
	where Id = @partId

return @ProductsCount

