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
		 * ����� ��������� ��������� ��������:
		 * 1. ����� ����������� Get ������ ���������� 404, ���� ��������������� ������� �� �������.
		 * 2. ����� ����������� Test ������ ���������� 400, ���� �� �������� �������� page.
		 * 3. ����� ����������� Test ������ ���������� 404, ���� ������� ��� ���������� page �� �������.
		 * 4. ����� ����������� Test ������ ���������� ��������� ��������� ���������.
		 * 5. ����� ����������� Delete ������ ���������� 404, ���� ��������������� ������� �� �������.
		 * 6. ����� ����������� Delete ������ ���������� � ������ ����������� DeleteRule � ���������� 200.
		*/
	}
}
