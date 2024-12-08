using asp.net_core_web_api_reference_project.CustomActionFilters;
using asp.net_core_web_api_reference_project.DTO;
using asp.net_core_web_api_reference_project.Models;
using asp.net_core_web_api_reference_project.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_core_web_api_reference_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //Create Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //if (ModelState.IsValid)      //>>replaced by [ValidateModel]
            //{                            //>>replaced by [ValidateModel]
                //Map DTO to Domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);
                //Map Domain model to dto and return it
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //}                          //>>replaced by [ValidateModel]

            //return BadRequest(ModelState);          //>>replaced by [ValidateModel]

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
            //Map domain model to Dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));  // we are using here List because the api can have multiple records

        }


        //Get Walk By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //map domainModel to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //update walk by Id
        [HttpPut]
        [ValidateModel]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //if (ModelState.IsValid)
            //{
                //map dto to domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                };
                //map Domain model to Dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //}

            //return BadRequest(ModelState);

        }

        //Delete a walk by Id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if(deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain model to Dto
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel)); 
        }
        
        
    }
}
