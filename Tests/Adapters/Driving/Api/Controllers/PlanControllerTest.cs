using API.Controllers;
using Application.Services;
using Domain.Dto.Result;
using Infra.DataBase.Repository;
using Microsoft.AspNetCore.Mvc;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driving.Api.Controllers
{
    public class PlanControllerTest : DataBaseMockTest
    {
        private PlanController _planController;

        [SetUp]
        public async Task SetUp()
        {
            var context = GetContext();
            var planRepository = new PlanRepository(context);
            var regionTaxRepository = new RegionTaxRepository(context);

            var planService = new PlanService(planRepository, regionTaxRepository);
            _planController = new PlanController(planService);

            var plan = new PlanDto()
            {
                Minutes = 30,
                Name = "Plan 1",
            }.ToEntity();
            var plan2 = new PlanDto()
            {
                Minutes = 60,
                Name = "Plan 2",
            }.ToEntity();

            await planRepository.AddAsync(plan);
            await planRepository.AddAsync(plan2);
        }

        [Test]
        public async Task GetAll_ReturnListPlanDto()
        {
            var actionResult = await _planController.GetAll();

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<List<PlanDto>>>());
            Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());

            var result = actionResult.Result as OkObjectResult;
            var resultValue = result.Value as List<PlanDto>;
            Assert.That(resultValue, Has.Count.EqualTo(2));
        }
    }
}