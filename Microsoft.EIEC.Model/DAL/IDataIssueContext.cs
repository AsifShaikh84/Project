using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.Entities;

namespace Microsoft.EIEC.Model.DAL
{
    public interface IDataIssueContext
    {
        string KeyFieldName { get; }
        IEnumerable<object> GetData(string issueStatusCode = null);
        IEnumerable<object> GetDetails(string keyfield);
        IEnumerable<object> GetChangeHistory(string keyfield, string fieldName);
        string  SaveChangeRequest(ChangeRequest cr);
    }
}
