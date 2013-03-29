using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Threading;

namespace Microsoft.EIEC.Model.DAL.DataIssue
{
    public class CommonDataIssue : BaseDataIssue
    {
        public override IList<T> GetData<T>(string issueStatusCode = null)
        {
            throw new NotImplementedException();
        }

        public override IList<T> GetDetails<T>(string keyField)
        {
            throw new NotImplementedException();
        }

    }
}
