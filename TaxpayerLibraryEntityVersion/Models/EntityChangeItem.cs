
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TaxpayerLibraryEntityVersion.Models;

/// <summary>
/// Container for use with looking at a <see cref="PropertyEntry{TEntity,TProperty}"/> which has a state of modified
/// </summary>
public class EntityChangeItem
{
    public string PropertyName { get; set; }
    public object OriginalValue { get; set; }
    public object CurrentValue { get; set; }

    public override string ToString() => $"{PropertyName} | {OriginalValue} | {CurrentValue}";

}