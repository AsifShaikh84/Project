using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data.Linq;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
	public class CalculationRuleContext
	{
        public IList<RulesConfiguration> GetCalculationRules(int scenarioId, int calculationRuleId = 0)
        {
            IList<RulesConfiguration> ocRulesConfiguration;

            using (var getPartnerData = new DataContext(GlobalParameters.ModelConnectionString))
            {
                try
                {
                    var result = getPartnerData.ExecuteQuery<RulesConfiguration>("exec dbo.Get_ConfigurationRules @CalculationRuleId ={0}, @ScenarioId={1}",
                        calculationRuleId, scenarioId).ToList<RulesConfiguration>();

                    ocRulesConfiguration = result.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return ocRulesConfiguration;
        }
	}
}
