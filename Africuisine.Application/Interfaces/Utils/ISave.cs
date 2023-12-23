using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Africuisine.Application.Interfaces.Utils
{
    public interface ISave
    {
        void GenerateBaseModelData(IEnumerable<EntityEntry> entries, CancellationToken cancellationToken = default);
    }
}