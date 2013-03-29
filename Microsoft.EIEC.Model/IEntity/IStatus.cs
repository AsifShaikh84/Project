using System;
using System.Linq;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface IStatus
    {
        int StatusId { get; set; }

        string StatusCode { get; set; }
    }
}
