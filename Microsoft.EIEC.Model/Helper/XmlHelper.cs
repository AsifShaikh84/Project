using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Data;
using System.Xml.Linq;

namespace Microsoft.EIEC.Model.Helper
{
    public static class XmlHelper
    {
        private static readonly string XML_FORMAT = "<data></data>";
        private static readonly string ROW_NODE_NAME = "row";
        private static readonly string ROW_COLLECTION_NODE_NAME = "rows";

        #region Public Members
        public static XmlDocument ConvertToXml<T>(IList<T> coll, string worksheetname)
        {
            List<string> columnList = new List<string>();
            if (coll.Count > 0)
            {
                columnList.AddRange(from info in coll[0].GetType().GetProperties() where info.CanRead select info.Name);
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XML_FORMAT);
            var uniqueIDNode = xmlDoc.CreateElement("WorksheetName");
            uniqueIDNode.SetAttribute("WorksheetName", worksheetname);
            
            if (xmlDoc.DocumentElement != null) 
                xmlDoc.DocumentElement.AppendChild(uniqueIDNode);

            var rws = xmlDoc.CreateElement(ROW_COLLECTION_NODE_NAME);
            if (xmlDoc.DocumentElement != null) 
                xmlDoc.DocumentElement.AppendChild(rws);

            foreach (var t in coll)
            {
                var rowNode = xmlDoc.CreateElement(ROW_NODE_NAME);
                T currentRow = t;
                foreach (string t1 in columnList)
                {
                    string attributeName = "";
                    attributeName = t1.Replace(" ", "");
                    PropertyInfo info = currentRow.GetType().GetProperty(attributeName);
                    object o = info.GetValue(currentRow, null);
                    if (o != null)
                        rowNode.SetAttribute(attributeName, o.ToString());
                }
                rowNode.SetAttribute("ErrorMessage", "");
                rws.AppendChild(rowNode);
            }

            var statusNode = xmlDoc.CreateElement("UploadStatus");
            statusNode.SetAttribute("UploadStatus", "");
            statusNode.SetAttribute("ErrorMessage", "");
            if (xmlDoc.DocumentElement != null) 
                xmlDoc.DocumentElement.AppendChild(statusNode);

            return xmlDoc;
        }

        public static XElement ConverDataTableToXElemet(DataTable dataTableToConvert, string tableName)
        {
            dataTableToConvert.TableName = tableName;

            var overridColumnList = new XElement(tableName);

            using (XmlWriter xmlWriter = overridColumnList.CreateWriter())
            {
                dataTableToConvert.WriteXml(xmlWriter, System.Data.XmlWriteMode.WriteSchema, true);
            }

            return overridColumnList;
        }

        public static DataSet ConverXElemetToDataSet(XElement xElementToConvert)
        {
            DataSet dsResult = null;

            try
            {
                var innerXml = new StringBuilder();

                foreach (XNode node in xElementToConvert.Nodes())
                {
                    innerXml.Append(node.ToString());
                }

                dsResult = new DataSet();
                using (StringReader reader = new StringReader(innerXml.ToString()))
                {
                    dsResult.ReadXml(XmlReader.Create(reader));
                }

                return dsResult;
            }
            catch (Exception)
            {
            }

            return dsResult;
        }


        #endregion




    }
}
