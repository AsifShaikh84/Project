using System;
using System.ComponentModel;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public enum DBOperationType
    {
        New,
        Update,
        Delete
    }

    public enum EIECJobStatus
    {
        NotIdleOrSuspended,
        Executing, 
        WaitingForThread, 
        BetweenRetries, 
        Idle, 
        Suspended, 
        WaitingForStepToFinish, 
        PerformingCompletionActions  
    }

    public enum EIECJobRunStatus
    {
        [Description("Error Failed")] 
        Error_Failed ,
        Succeeded,
        Retry,
        Cancelled,
        [Description("In Progress")] 
        In_Progress,
        [Description("Status Unknown")] 
        Status_Unknown
        
    }

    public enum EntityActivity
    {
        Include,
        Exclude
    }

   
}
