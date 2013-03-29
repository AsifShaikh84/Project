using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    [DataContract]
    public class CalculationRuleQuery 
    {
        [DataMember]
        public int CalculationRuleQueryId { get; set; }

        [DataMember]
        public string CalculationRuleQueryCode { get; set; }

        [DataMember]
        public string QueryDescription { get; set; }

        [DataMember]
        public string Denominators { get; set; }

        [DataMember]
        public string ValueColumn_LC { get; set; }

        [DataMember]
        public string ValueColumn_CD { get; set; }

        [DataMember]
        public string QualifiedDataSource { get; set; }

        [DataMember]
        public string WhereClause { get; set; }

        [DataMember]
        public string GroupByClause { get; set; }

        [DataMember]
        public string HavingClause { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsTimeBound { get; set; }

        [DataMember]
        public bool IsPreprocess { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        public CalculationRuleQuery()
        {

        }

        public CalculationRuleQuery(DataRow dr)
        {
            CalculationRuleQueryId = Convert.ToInt32(dr["CalculationRuleQueryId"]);
            CalculationRuleQueryCode = dr["CalculationRuleQueryCode"].ToString();
            QueryDescription = dr["QueryDescription"].ToString();
            Denominators = dr["Denominators"].ToString();
            ValueColumn_LC = dr["ValueColumn_LC"].ToString();
            ValueColumn_CD = dr["ValueColumn_CD"].ToString();
            QualifiedDataSource = dr["QualifiedDataSource"].ToString();
            WhereClause = dr["WhereClause"].ToString();
            GroupByClause = dr["GroupByClause"].ToString();
            HavingClause = dr["HavingClause"].ToString();
            IsActive = Convert.ToBoolean(dr["IsActive"]);
            IsTimeBound = Convert.ToBoolean(dr["IsTimeBound"]);
            IsPreprocess = Convert.ToBoolean(dr["IsPreprocess"]);
            ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
            ModifiedBy = dr["ModifiedBy"].ToString();
        }

        public string FormQuery()
        {
            string query = string.Empty;

            query = " SELECT  TOP 10 " + Denominators + ", " +
                    ValueColumn_LC + " AS Value_LC," +
                    ValueColumn_CD + " AS Value_CD" +
                    " FROM     " + QualifiedDataSource;

            if (!string.IsNullOrWhiteSpace(WhereClause) &&
                WhereClause != "n/a")
                query += " WHERE " + WhereClause;

            if (!string.IsNullOrWhiteSpace(GroupByClause) &&
                GroupByClause != "n/a")
                query += " GROUP BY " + GroupByClause;

            if (!string.IsNullOrWhiteSpace(HavingClause) &&
                HavingClause != "n/a")
                query += " HAVING   " + HavingClause;

            return query;
        }

        [DataMember]
        public bool IsNull
        {
            get
            {
                if (string.IsNullOrEmpty(QueryDescription))
                    return true;

                if (string.IsNullOrEmpty(Denominators))
                    return true;

                if (string.IsNullOrEmpty(ValueColumn_LC))
                    return true;

                if (string.IsNullOrEmpty(ValueColumn_CD))
                    return true;

                if (string.IsNullOrEmpty(QualifiedDataSource))
                    return true;

                return false;
            }
            set
            {
                
            }
        }
    }
}
