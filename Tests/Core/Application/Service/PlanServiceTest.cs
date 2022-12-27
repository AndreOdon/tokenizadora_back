using Application.Services;
using Domain.Dto.Input;
using Domain.Dto.Result;
using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using NSubstitute;

namespace Tests.Core.Application.Service
{
    [TestFixture]
    public class PlanServiceTest
    {
        private IPlanRepository _planRepository;
        private IRegionTaxRepository _regionTaxRepository;
        private PlanService _planService;

        [SetUp]
        public void SetUp()
        {
            _planRepository = Substitute.For<IPlanRepository>();
            _regionTaxRepository = Substitute.For<IRegionTaxRepository>();
            _planService = new PlanService(_planRepository, _regionTaxRepository);
        }

        [Test]
        public async Task Calc_NotExistingPlan_ReturnWithError()
        {
            var input = new CalcInputDto()
            {
                DestinyId = 1,
                Minutes = 10,
                OriginId = 2,
                PlanId = 1
            };
            _planRepository.GetById(input.PlanId).Returns(Task.FromResult<Plan?>(null));

            var result = await _planService.Calc(input);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<CalcResultDto>());
            Assert.That(result.WithoutPlanValue, Is.Null);
            Assert.That(result.WithPlanValue, Is.Null);
            Assert.IsNotNull(result.Error);
            Assert.That(result.Error, Is.EqualTo("Plano não encontrado"));
            await _planRepository.Received(1).GetById(input.PlanId);
            await _regionTaxRepository.DidNotReceive().GetByOriginDestiny(input.OriginId, input.DestinyId);
        }

        [Test]
        public async Task Calc_NotExistingOriginDestiny_ReturnWithError()
        {
            var input = new CalcInputDto()
            {
                DestinyId = 1,
                Minutes = 10,
                OriginId = 2,
                PlanId = 1
            };

            var planEntity = new PlanDto()
            {
                Name = "Test",
                Minutes = 30
            }.ToEntity();
            _planRepository.GetById(input.PlanId).Returns(Task.FromResult<Plan?>(planEntity));

            _regionTaxRepository.GetByOriginDestiny(input.OriginId, input.DestinyId).Returns(Task.FromResult<RegionTax?>(null));

            var result = await _planService.Calc(input);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<CalcResultDto>());
            Assert.That(result.WithoutPlanValue, Is.Null);
            Assert.That(result.WithPlanValue, Is.Null);
            Assert.IsNotNull(result.Error);
            Assert.That(result.Error, Is.EqualTo("Taxa para origem e destino não encontrada"));
            await _planRepository.Received(1).GetById(input.PlanId);
            await _regionTaxRepository.Received(1).GetByOriginDestiny(input.OriginId, input.DestinyId);
        }

        [TestCase(20, 0, 38)]
        [TestCase(30, 0, 57)]
        [TestCase(40, 20.9, 76)]
        public async Task Calc_ReturnValidData(int minutes, decimal valueWithPlan, decimal valueWithoutPlan)
        {
            var input = new CalcInputDto()
            {
                DestinyId = 1,
                Minutes = minutes,
                OriginId = 2,
                PlanId = 1
            };

            var planEntity = new PlanDto()
            {
                Name = "Test",
                Minutes = 30
            }.ToEntity();
            _planRepository.GetById(input.PlanId).Returns(Task.FromResult<Plan?>(planEntity));

            var regionTaxEntity = new RegionTaxDto()
            {
                DestinyRegionId = 1,
                OriginRegionId = 2,
                Tax = 1.90M
            }.ToEntity();
            _regionTaxRepository.GetByOriginDestiny(input.OriginId, input.DestinyId).Returns(Task.FromResult<RegionTax?>(regionTaxEntity));

            var result = await _planService.Calc(input);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<CalcResultDto>());
            Assert.That(result.WithoutPlanValue, Is.EqualTo(valueWithoutPlan));
            Assert.That(result.WithPlanValue, Is.EqualTo(valueWithPlan));
            Assert.IsNull(result.Error);
            await _planRepository.Received(1).GetById(input.PlanId);
            await _regionTaxRepository.Received(1).GetByOriginDestiny(input.OriginId, input.DestinyId);
        }

        [Test]
        public async Task GetAll_ReturnsListValidPlanDto()
        {
            var planEntity1 = new PlanDto()
            {
                Name = "Test 1",
                Minutes = 30
            }.ToEntity();
            var planEntity2 = new PlanDto()
            {
                Name = "Test 2",
                Minutes = 60
            }.ToEntity();
            var planEntity3 = new PlanDto()
            {
                Name = "Test ",
                Minutes = 120
            }.ToEntity();

            var entityList = new List<Plan>();
            entityList.Add(planEntity1);
            entityList.Add(planEntity2);
            entityList.Add(planEntity3);

            _planRepository.GetAll().Returns(Task.FromResult<List<Plan>>(entityList));

            var result = await _planService.GetAllPlans();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<PlanDto>>());
            Assert.That(result, Has.Count.EqualTo(3));
        }
    }
}