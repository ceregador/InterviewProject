using InterviewProject.Controllers;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests
{
	public sealed class RuleControllerTests
	{
		readonly string site = "yandex.ru";
		readonly string rule = "//ol";

		[Fact]
		public void Add_Calls_AddOrUpdateRule_From_RuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();

			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Add(site, rule);
			Assert.IsType<OkResult>(result);

			repository.Verify(mock => mock.AddOrUpdateRule(site, rule), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public void Get_Calls_GetRule_From_RuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.GetRule(site)).Returns(rule);

			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Get(site);
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(rule, okResult.Value);

			repository.Verify(mock => mock.GetRule(site), Times.Once);
			webAgent.Verify(mock => mock.GetContent(It.IsAny<string>()), Times.Never);
		}

		/*
		 * Далее проверяем следующие сценарии:
		 * 1. Метод контроллера Get должен возвращать 404, если соответствующее правило не найдено.
		 * 2. Метод контроллера Test должен возвращать 400, если не передали параметр page.
		 * 3. Метод контроллера Test должен возвращать 404, если правило для переданной page не найдено.
		 * 4. Метод контроллера Test должен возвращать ожидаемый текстовый результат.
		 * 5. Метод контроллера Delete должен возвращать 404, если соответствующее правило не найдено.
		 * 6. Метод контроллера Delete должен обращаться к методу репозитория DeleteRule и возвращать 200.
		*/
	}
}
