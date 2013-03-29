using System;
using System.Linq;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface IAlgorithm
    {
        string AlgorithmId { get; set; }
        string AlgorithmCode { get; set; }
        string AlgorithmName { get; set; }
        string AlgorithmDescription { get; set; }
        int TemplateId { get; set; }
        int CalculationModeId { get; set; }
        int IncentiveTypeId { get; set; }
        bool IsRateInDollar { get; set; }
        bool IsActive { get; set; }    }
}
