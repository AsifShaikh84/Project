using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public class QueryRule
    {
        #region Private Variable

        string _queryClause = string.Empty;
        string _ruleValue = string.Empty;
        string _valueString = string.Empty;
        int _id = 0;


        #endregion

        #region Property

        public int Sequence { get; set; }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string QueryClause
        {
            get { return _queryClause; }
            set { _queryClause = value; }
        }

        public string ValueString
        {
            get { return _valueString; }
            set { _valueString = value; }
        }

        #endregion

        public QueryRule(string queryClause, string ruleValue, string valuString)
        {
            this._valueString = valuString;
            //this._queryClause = string.Format("WHERE {0} AND {1} IN ", queryClause, valuString);
            this._queryClause = queryClause;
            this._ruleValue = ruleValue;
        }
    }
}
