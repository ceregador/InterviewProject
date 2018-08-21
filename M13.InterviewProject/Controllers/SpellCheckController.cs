using System.Linq;
using System.Threading.Tasks;
using InterviewProject.Repositories;
using InterviewProject.SpellCheck;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.Controllers
{
	/// <summary>
	/// sample usage:
	/// 1) view errors in text: http://localhost:56660/api/spell/errors?page=yandex.ru
	/// 2) view errors count in text: http://localhost:56660/api/spell/errorscount?page=yandex.ru
	/// </summary>
	[Route("api/spell")]
	public sealed class SpellCheckController : Controller
	{
		private readonly IWebAgent _agent;
		private readonly IRuleRepository _ruleRepository;
		private readonly ISpellChecker _spellChecker;

		public SpellCheckController(
			IWebAgent agent,
			IRuleRepository ruleRepository,
			ISpellChecker spellChecker)
		{
			_agent = agent;
			_ruleRepository = ruleRepository;
			_spellChecker = spellChecker;
		}

		/// <summary>
		/// Проверить текст страницы по заданному адресу и получить список слов с ошибками
		/// </summary>
		[HttpGet("errors")]
		public async Task<IActionResult> GetWordsWithErrors(string page)
		{
			var text = await ApplyRule(page);
			var errors = await _spellChecker.Check(text);

			return Ok(errors.Select(e => e.Word));
		}

		/// <summary>
		/// Проверить текст страницы по заданному адресу и получить количество слов с ошибками
		/// </summary>
		[HttpGet("errorscount")]
		public async Task<IActionResult> GetErrorsCount(string page)
		{
			var text = await ApplyRule(page);
			var errors = await _spellChecker.Check(text);

			return Ok(errors.Count);
		}

		private async Task<string> ApplyRule(string pageAddress)
		{
			// ToDo: implement handling of a case when the rule is NULL.
			var rule = _ruleRepository.GetRule(pageAddress);

			var pageContent = await _agent.GetContent($"http://{pageAddress}");

			return ContentFilter.Apply(pageContent, rule);
		}
	}
}