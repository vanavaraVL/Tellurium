using FarmManagement.Models.Dto;
using FarmManagement.Models.Responses;
using FarmManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FarmerController : ControllerBase
{
    private readonly IFarmerService _farmerService;
    private readonly ILogger<FarmerController> _logger;

    public FarmerController(ILogger<FarmerController> logger, IFarmerService farmerService)
    {
        _farmerService = farmerService ?? throw new ArgumentNullException(nameof(farmerService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Route("{name}")]
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseResultDto<bool>), StatusCodes.Status200OK)]
    [EnableCors]
    public async Task<IActionResult> Delete(string name)
    {
        var result = await _farmerService.DeleteItem(name);

        return Ok(result);
    }

    [Route("{name}")]
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseResultDto<AnimalDto>), StatusCodes.Status200OK)]
    [EnableCors]
    public async Task<IActionResult> Edit([FromBody] AnimalDto animalDto, string name)
    {
        var result = await _farmerService.EditItem(animalDto, name);

        return Ok(result);
    }

    [Route("")]
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseResultDto<AnimalDto>), StatusCodes.Status200OK)]
    [EnableCors]
    public async Task<IActionResult> Create([FromBody] AnimalDto animalDto)
    {
        var result = await _farmerService.CreateNewItem(animalDto);

        return Ok(result);
    }

    [Route("")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseResultDto<IReadOnlyList<AnimalDto>>), StatusCodes.Status200OK)]
    [EnableCors]
    public async Task<IActionResult> GetAll()
    {
        var result = await _farmerService.GetAll();

        return Ok(result);
    }

    [Route("{name}")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseResultDto<AnimalDto>), StatusCodes.Status200OK)]
    [EnableCors]
    public async Task<IActionResult> GetById(string name)
    {
        var result = await _farmerService.GetByName(name);

        return Ok(result);
    }
}