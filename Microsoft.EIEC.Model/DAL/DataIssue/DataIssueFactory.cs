namespace Microsoft.EIEC.Model.DAL.DataIssue
{
    public static class DataIssueFactory
    {

        public static BaseDataIssue GetDataIssueType(DataIssueTypes dataIssueType)
        {
            try
            {
                switch (dataIssueType)
                {
                    case DataIssueTypes.EnrollmentDataIssueType:
                        return new EnrollmentDataIssue();

                    case DataIssueTypes.IncentiveRequestDataIssueType:
                        return new IncentiveRequestDataIssue();

                    case DataIssueTypes.InvoiceDataIssueType:
                        return new InvoiceDataIssue();

                    default:
                        return null;
                }
            }
            catch (System.Exception)
            {
                throw;
            }   
        }   
    }
}
