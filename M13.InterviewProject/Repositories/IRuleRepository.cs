namespace InterviewProject.Repositories
{
	public interface IRuleRepository
	{
		string GetRule(string site);

		void AddOrUpdateRule(string site, string rule);

		bool IsRuleExists(string site);

		bool DeleteRule(string site);
	}
}
