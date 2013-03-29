using System;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public class FieldValueTranspose
    {
        public string FieldName { get; set; }
        public object Value { get; set; }
        public bool OverrideAllowed { get; set; }

        public FieldValueTranspose(string fieldName, object value, bool overrideAllowed)
        {
            FieldName = fieldName;
            Value = value != null ? (value.GetType() == typeof(DateTime) ? Convert.ToDateTime(value).ToShortDateString() : value) : value;
            OverrideAllowed = overrideAllowed;
        }
    }
}
