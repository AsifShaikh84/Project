using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface IScenario
    {
        void Next();
        void Previous();
    }
}
