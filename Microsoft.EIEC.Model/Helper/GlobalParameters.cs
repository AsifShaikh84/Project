using System.Configuration;

namespace Microsoft.EIEC.Model.Helper
{
    public static class GlobalParameters
    {
     
        public static readonly string OFFLINEDATATEMPLATECASHEKEY = "OfflineDataTemplateCacheKey";

        public static readonly string Menuitemcachekey = "MenuItemDataContextCached";

        public static readonly string Tiledmenuitemcachekey = "MenuItemDataContextforTiles";

        public static readonly string MENUTABLESCHEMAKEY = "MenuTableSchemaKey";

        public static readonly string INCLUDECLAUSECOLL = "IncludeClauseColl";

        public static readonly string WHERECLAUSEQUERY = "WHEREClauseQuery";

        public static readonly string Tiledphotodata = "TiledPhotoData";

        public static readonly string GlobalROC = "UNK";

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public static readonly string ModelConnectionString = string.Empty; //todo: either remove or use it ConfigurationManager.ConnectionStrings["ModelSqlConnectionString"].ConnectionString;

        public static readonly string OLEDBConnectionString = OLEDBConnectionString = ConfigurationManager.ConnectionStrings["OLEDBConnectionString"].ConnectionString;

        #region View State Constants
        public static readonly string ScenarioId = "ScenarioId";
        public static readonly string AlgorithmId = "AlgorithmId";
        public static readonly string AlgorithmRuleId = "AlgorithmRuleId";
        public static readonly string CalculationRuleId = "CalculationRuleId";
        public static readonly string CalculationRuleQueryId = "CalculationRuleQueryId";
        public static readonly string CurrentAlgorithm = "CurrentAlgorithm";
        public static readonly string AlgorithmList = "AlgorithmList";
        public static readonly string AlgorithmRuleList = "AlgorithmRuleList";
        public static readonly string CalculationRuleList = "CalculationRuleList";
        public static readonly string CalculationRuleQueryList = "CalculationRuleQueryList";
        public static readonly string CalculationQueryChangedList = "CalculationQueryChangedList";
        
        public static readonly string KeyField = "keyField";
        public static readonly string KeyValue = "keyValue";
        //public static readonly string KeyTable = "keyTable";
        public static readonly string TemplateId = "TemplateId";
        public static readonly string TableId = "TableId";
        public static readonly string RowId = "RowId";
        public static readonly string SelectedScenario = "SelectedScenario";
        public static readonly string ExternalKey = "ExternalKey";
        public static readonly string EntityROC = "EntityROC";
        public static readonly string FieldName = "FieldName";
        public static readonly string RequestTypeId = "RequestTypeId";

        public static readonly string OverrideColumnText = "OverrideColumnText";
        public static readonly string PaymentChangeSummary = "PaymentChangeSummary";
        public static readonly string IncentivePayments = "IncentivePayments";
        public static readonly string ChangedList = "ChangedList";
        public static readonly string AgreementSearchparameter = "AgreementId";
        public static readonly string InvoiceSearchParameter = "InvoiceDocumentNumber";
        public static readonly string AgreementInternalId = "AgreementInternalId";
        public static readonly string InvoiceInternalId = "InvoiceInternalId";
        public static readonly string IsApproved = "IsApproved";
        public static readonly string LastPage = "LastPage";
        public static readonly string CurrentLoadedMenu = "CurrentLoadedMenu";
        public static readonly string ExcelReportInputParameters = "ExcelReportInputParameters";
        public static readonly string SelectedReport = "SelectedReportName";
        public static readonly string ExcelReportId = "ExcelReportId";
        public static readonly string ExcelReportName = "ExcelReportName";
        public static readonly string AllReportParameters = "AllReportParameters";
        public static readonly string CurrentMonth = "CurrentMonth";

        #endregion

        #region Cached Constants
        public static readonly string OverrideColumn = "OverrideColumn";
        public static readonly string AppUsers = "AppUsers";
        public static readonly string DistinctEmailAliases = "DistinctEmailAliases";
        public static readonly string MetaData = "MetaData";
        public static readonly string CurrentViewPartnerList = "CurrentViewPartnerList";
        public static readonly string PropertyData = "PropertyData";
        public static readonly string AllHistory = "AllHistory";
        #endregion

        #region Session Constants
        public static readonly string CurrentPartnerComplianceSessionData = "CurrentPartnerComplianceSessionData";
        public static readonly string SelectedPartnerPCNs = "SelectedPartnerPCNs";
        public static readonly string SelectedProgramBrandId = "ProgramBrandId";
        public static readonly string SelectedProgramBrandName = "ProgramBrandName";
        public static readonly string UserDisplayName = "UserDisplayName";
        public static readonly string UserImageURL = "UserImageURL";
        public static readonly string RequestAssignmentIsShowOverrideColumn = "ShowOverrideColumns";
        public static readonly string RequestAssigmentTemplateId = "RequestAssigmentTemplateId";
        public static readonly string PersonDetails = "PersonDetails";
        public static readonly string IsNewSession = "IsNewSession";
        
        #endregion

        #region Presenter Constants

        public static readonly string IssueSource = "IssueSource";
        public static readonly string Preview = "Preview";
        public static readonly string PaymentsApproval = "PaymentsApproval";
        public static readonly string CHIPException = "CHIPException";

        #endregion
    }
}
