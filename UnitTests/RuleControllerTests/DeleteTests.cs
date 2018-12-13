using FluentAssertions;
using InterviewProject.Controllers;
using InterviewProject.Repositories;
using InterviewProject.WebAgent;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.RuleControllerTests
{
	public sealed class DeleteTests
	{
		[Fact]
		public void Delete_ReturnsNotFound_ForNotFoundRule()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.DeleteRule(TestResources.Site)).Returns(false);
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Delete(TestResources.Site);

			result.Should().BeOfType<NotFoundResult>();
			repository.Verify(mock => mock.DeleteRule(TestResources.Site), Times.Once);
		}

		[Fact]
		public void Delete_ReturnsOk_Calls_DeleteRule_FromRuleRepository()
		{
			var webAgent = new Mock<IWebAgent>();
			var repository = new Mock<IRuleRepository>();
			repository.Setup(mock => mock.DeleteRule(TestResources.Site)).Returns(true);
			var ruleController = new RuleController(repository.Object, webAgent.Object);

			var result = ruleController.Delete(TestResources.Site);

			result.Should().BeOfType<OkResult>();
			repository.Verify(mock => mock.DeleteRule(TestResources.Site), Times.Once);
		}
	}
}
