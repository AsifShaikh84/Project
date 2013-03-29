using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class AdjustmentDetails
    {
        [DataMember]
        public IList<AdjustmentAccruals> AdjustmentAccruals { get; set; }

        [DataMember]
        public IList<TaskRequest> IncentiveAdjustment { get; set; }

    }
}
