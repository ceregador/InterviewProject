using System.Threading.Tasks;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.Controllers
{
	/// <summary>
	/// sample usage:
	/// 1) check xpath rule '//ol' for site yandex.ru: http://localhost:56660/api/rule/add?site=yandex.ru&rule=%2f%2fol
	/// 2) check rule is saved: http://localhost:56660/api/rule/get?site=yandex.ru
	/// 3) view text parsed by rule: http://localhost:56660/api/rule/test?page=yandex.ru
	/// 4) delete the rule: http://localhost:56660/api/rule/delete?site=yandex.ru
	/// </summary>
	[Route("api/rule")]
	public sealed class RuleController : Controller
	{
		private readonly IRuleRepository _ruleRepository;
		private readonly IWebAgent _agent;

		public RuleController(IRuleRepository ruleRepository, IWebAgent agent)
		{
			_ruleRepository = ruleRepository;
			_agent = agent;
		}

		[HttpPost("add")]
		public IActionResult Add(string site, string rule)
		{
			_ruleRepository.AddOrUpdateRule(site, rule);

			return Ok();
		}

		[HttpGet("get")]
		public IActionResult Get(string site)
		{
			var rule = _ruleRepository.GetRule(site);

			if(rule == null)
				return NotFound();

			return Ok(rule);
		}

		[HttpGet("test")]
		public async Task<IActionResult> Test(string page, string rule = null)
		{
			if (string.IsNullOrWhiteSpace(page))
				return BadRequest($"The '{nameof(page)}' parameter is mandatory.");

			if (string.IsNullOrWhiteSpace(rule))
			{
				rule = _ruleRepository.GetRule(page);

				if (rule == null)
					return NotFound($"Rule for site '{page}' not found.");
			}

			var pageContent = await _agent.GetContent($"http://{page}");
			var textResult = ContentFilter.Apply(pageContent, rule);

			return new ObjectResult(textResult);
		}

		[HttpDelete("delete")]
		public IActionResult Delete(string site)
		{
			if (!_ruleRepository.DeleteRule(site))
				return NotFound();

			return Ok();
		}
	}
}
