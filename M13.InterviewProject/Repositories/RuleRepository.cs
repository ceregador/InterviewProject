using System.Collections.Concurrent;

namespace InterviewProject.Repositories
{
	public sealed class RuleRepository : IRuleRepository
	{
		private readonly ConcurrentDictionary<string, string> _rules;

		public RuleRepository()
		{
			_rules = new ConcurrentDictionary<string, string>();
		}

		public string GetRule(string site)
		{
			if (_rules.TryGetValue(site, out string rule))
				return rule;

			return null;
		}

		public void AddOrUpdateRule(string site, string rule)
		{
			_rules.AddOrUpdate(site, rule, (key, value) => rule);
		}

		public bool IsRuleExists(string site)
		{
			return _rules.ContainsKey(site);
		}

		public bool DeleteRule(string site)
		{
			return _rules.TryRemove(site, out string removed);
		}
	}
}
