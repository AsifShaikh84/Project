using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.EIEC.Model.Entities
{
    interface IBaseContext
    {
        List<T> GetData<T>();
    }
}
