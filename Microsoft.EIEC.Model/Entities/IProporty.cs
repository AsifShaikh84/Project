using System.Collections.Generic;

namespace Microsoft.EIEC.Model.Entities
{
    public interface ITranspose
    {
        IList<FieldValueTranspose> TransposeToFieldValue();
    }
}
