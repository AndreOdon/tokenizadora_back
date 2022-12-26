using API.Controllers;
using Application.Services;
using Domain.Dto.Input;
using Domain.Dto.Result;
using Infra.DataBase.Repository;
using Microsoft.AspNetCore.Mvc;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driving.Api.Controllers
{
    public class SimulatorControllerTest : DataBaseMockTest
    {
        private SimulatorController _simulatorController;
        private int _originId;
        private int _destinyId;
        private int _planId;

        [SetUp]
        public async Task SetUp()
        {
            var context = GetContext();
            var planRepository = new PlanRepository(context);
            var regionRepository = new RegionRepository(context);
            var regionTaxRepository = new RegionTaxRepository(context);

            var planService = new PlanService(planRepository, regionTaxRepository);
            _simulatorController = new SimulatorController(planService);

            var originEntity = new RegionDto()
            {
                Code = "001",
                Name = "Origin",
            }.ToEntity();
            var destinyEntity = new RegionDto()
            {
                Code = "002",
                Name = "Destiny",
            }.ToEntity();

            await regionRepository.AddAsync(originEntity);
            await regionRepository.AddAsync(destinyEntity);

            var regionTaxEntity = new RegionTaxDto()
            {
                DestinyRegionId = destinyEntity.Id,
                OriginRegionId = originEntity.Id,
                Tax = 1.25M
            }.ToEntity();

            await regionTaxRepository.AddAsync(regionTaxEntity);

            var planEntity = new PlanDto()
            {
                Minutes = 10,
                Name = "Test"
            }.ToEntity();

            await planRepository.AddAsync(planEntity);

            _originId = originEntity.Id;
            _destinyId = destinyEntity.Id;
            _planId = planEntity.Id;
        }

        [Test]
        public async Task Simulator_NotExistingPlan_ReturnBadRequest()
        {
            var inputDto = new CalcInputDto()
            {
                PlanId = 50,
                DestinyId = _destinyId,
                OriginId = _originId,
                Minutes = 10,
            };

            var actionResult = await _simulatorController.CalculateSimulation(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<CalcResultDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<BadRequestObjectResult>());

            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.That(badRequest.Value, Is.EqualTo("Plano não encontrado"));
        }

        [Test]
        public async Task Simulator_NotExistingDestiny_ReturnBadRequest()
        {
            var inputDto = new CalcInputDto()
            {
                PlanId = _planId,
                DestinyId = 50,
                OriginId = _originId,
                Minutes = 10,
            };

            var actionResult = await _simulatorController.CalculateSimulation(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<CalcResultDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<BadRequestObjectResult>());

            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.That(badRequest.Value, Is.EqualTo("Taxa para origem e destino não encontrada"));
        }

        [Test]
        public async Task Simulator_NotExistingOrigin_ReturnBadRequest()
        {
            var inputDto = new CalcInputDto()
            {
                PlanId = _planId,
                DestinyId = _destinyId,
                OriginId = 50,
                Minutes = 10,
            };

            var actionResult = await _simulatorController.CalculateSimulation(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<CalcResultDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<BadRequestObjectResult>());

            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.That(badRequest.Value, Is.EqualTo("Taxa para origem e destino não encontrada"));
        }

        [TestCase(5, 0, 6.25)]
        [TestCase(10, 0, 12.5)]
        [TestCase(15, 6.88, 18.75)]
        public async Task Simulator_ReturnsOKWithValues(int minutes, decimal valueWithPlan, decimal valuesWithoutPlan)
        {
            var inputDto = new CalcInputDto()
            {
                PlanId = _planId,
                DestinyId = _destinyId,
                OriginId = _originId,
                Minutes = minutes,
            };

            var actionResult = await _simulatorController.CalculateSimulation(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<CalcResultDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());

            var result = actionResult.Result as OkObjectResult;
            var resultValue = result.Value as CalcResultDto;
            Assert.That(resultValue.WithPlanValue, Is.EqualTo(valueWithPlan));
            Assert.That(resultValue.WithoutPlanValue, Is.EqualTo(valuesWithoutPlan));
        }
    }
}