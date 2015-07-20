--ToDo
--alter table Subassemblies add Capability int
--alter table Parts add Capability int

CREATE PROCEDURE CalculateCapability
	@partId int,
	@ProductsCount int OUTPUT
AS

Declare @subassembliesId int
Declare @subPartId int
Declare @costContribution decimal(25,13)
Declare @count int
Declare @CurrCapabitilty int
Declare @MinProductsCount int

Declare curP cursor local For
	select s.Id, SubassemblyId, CostContribution, p.Count
	from Subassemblies s
	inner join Parts p on p.Id = s.SubassemblyId
	where AssemblyId = @partId

OPEN curP 
set @MinProductsCount = 999999999
set @ProductsCount = 0
Fetch Next From curP Into @subassembliesId, @subPartId, @costContribution, @count

While @@Fetch_Status = 0 Begin
	exec dbo.CalculateCapability @subPartId, @CurrCapabitilty output
	set @ProductsCount = (@count + @CurrCapabitilty) / @costContribution
	set @MinProductsCount = IIF(@ProductsCount>@MinProductsCount, @MinProductsCount, @ProductsCount)
	
	update Subassemblies set Capability = @ProductsCount
		where Id = @subassembliesId

Fetch Next From curP Into @subassembliesId, @subPartId, @costContribution, @count
End -- End of Fetch

Close curP
Deallocate curP

set @MinProductsCount = IIF(@MinProductsCount = 999999999, 0, @MinProductsCount)
update Parts set Capability = @MinProductsCount
	where Id = @partId

return @ProductsCount

