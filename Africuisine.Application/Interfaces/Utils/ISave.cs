using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface ISave
{
    int GenerateBaseModelData(IEnumerable<EntityEntry> entries, CancellationToken cancellationToken = default);
}