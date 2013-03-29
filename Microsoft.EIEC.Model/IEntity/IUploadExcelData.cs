using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface IUploadExcelData
    {
        string StoredProcedure { get; }
        DataTable ExcelDataTable { get; }
        XElement ExcelDataXML { get; }
        bool UploadData();
    }
}
