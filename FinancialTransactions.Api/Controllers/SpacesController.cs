using Divagando.Domain.Dtos;
using Divagando.Domain.Entities;
using Divagando.Domain.Repositories;
using Divagando.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divagando.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SpacesController : DataController<Space>
    {
        readonly ISpaceService _spaceService;

        public SpacesController(IDivagandoDbRepository divagandoDbRepository, 
                                ISpaceService spaceService) : base(divagandoDbRepository)
        {
            _spaceService = spaceService;
        }

        [HttpPost]
        public IActionResult Post(SpaceDto spaceDto)
        {
            spaceDto.OwnerId = AuthenticatedUserId;
            _spaceService.Create(spaceDto);
            return Ok();
        }

        [HttpPut("{spaceId}/IncrementSearch")]
        public IActionResult IncrementSearch(int spaceId)
        {
            var searchCount = _spaceService.IncrementSearch(spaceId);
            return Ok(searchCount);
        }

        [HttpPut("{spaceId}/Archive")]
        public IActionResult Archive(int spaceId)
        {
            _spaceService.Archive(spaceId);
            return Ok();
        }
    }
}
