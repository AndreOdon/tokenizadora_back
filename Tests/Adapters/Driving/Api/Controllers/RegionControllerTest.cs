using API.Controllers;
using Application.Services;
using Domain.Dto.Result;
using Infra.DataBase.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driving.Api.Controllers
{
    public class RegionControllerTest : DataBaseMockTest
    {
        private RegionController _regionController;

        [SetUp]
        public async Task SetUp()
        {
            var context = GetContext();
            var regionRepository = new RegionRepository(context);

            var regionService = new RegionService(regionRepository);
            _regionController = new RegionController(regionService);

            var region1 = new RegionDto()
            {
                Code = "001",
                Name = "Region 1",
            }.ToEntity();
            var region2 = new RegionDto()
            {
                Code = "002",
                Name = "Region 2",
            }.ToEntity();

            await regionRepository.AddAsync(region1);
            await regionRepository.AddAsync(region2);
        }

        [Test]
        public async Task GetAll_ReturnListRegionDto()
        {
            var actionResult = await _regionController.GetAll();

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<List<RegionDto>>>());
            Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());

            var result = actionResult.Result as OkObjectResult;
            var resultValue = result.Value as List<RegionDto>;
            Assert.That(resultValue, Has.Count.EqualTo(2));
        }
    }
}
