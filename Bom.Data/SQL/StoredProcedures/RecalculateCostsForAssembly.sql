CREATE PROCEDURE RecalculateCostsForAssembly
	@partId int,
	@TotalCost decimal(25,13) OUTPUT
AS

Declare @subassembliesId int
Declare @subPartId int
Declare @costContribution decimal(25,13)
Declare @ownCost decimal(25,13)
Declare @CurrCost decimal(25,13)
Declare @isTaken int

Declare curP cursor local For
	select s.Id, SubassemblyId, CostContribution, p.OwnCost  
	from Subassemblies s
	inner join Parts p on p.Id = s.SubassemblyId
	where AssemblyId = @partId

OPEN curP 
set @TotalCost = 0
Fetch Next From curP Into @subassembliesId, @subPartId, @costContribution, @ownCost

-- loop on the all sub-parts of the next level
While @@Fetch_Status = 0 Begin
    -- for each subPartId of the next level, recursively count its cost, considering all subassemblies 
	exec dbo.RecalculateCostsForAssembly @subPartId, @CurrCost output
	if @CurrCost = 0
	begin 
	    -- cost of the subPartId sub-tree is 0, consider sub-part's own cost
		set @CurrCost = @ownCost * @costContribution
	end
	set @TotalCost = @TotalCost + (@CurrCost * @costContribution)
	
	update Subassemblies set InheritedCost = @CurrCost
		where Id = @subassembliesId

Fetch Next From curP Into @subassembliesId, @subPartId, @costContribution, @ownCost
End -- End of Fetch

Close curP
Deallocate curP
update Parts set ComponentsCost = @TotalCost
	where Id = @partId

-- consider own cost of current part (root for the sub-parts tree in the Fetch loop)
select @TotalCost = ComponentsCost  + OwnCost from Parts where Id = @partId

return @TotalCost

