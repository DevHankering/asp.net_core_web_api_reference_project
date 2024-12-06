using asp.net_core_web_api_reference_project.Data;
using asp.net_core_web_api_reference_project.DTO;
using asp.net_core_web_api_reference_project.Models;
using asp.net_core_web_api_reference_project.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_reference_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }
        //Get All Regions
        //Get: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //this way, we can give data to our api without using database

            /*var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(), // creates new GUID
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://Images.1"
                },

                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "wellington region",
                    Code = "wlg",
                    RegionImageUrl = "https://Images.2"
                }
            };
            */


            //using a database
            //step1. Get Data from database - domain models
            //var regionsDomain = await dbContext.Regions.ToListAsync();   //>> replaced by Repository
            var regionsDomain = await regionRepository.GetAllAsync();

            //step2. Map(convert) Domain model to Dtos
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain) {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,                          // This one way for creating the Dto
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }


            //step3. return Dtos

            return Ok(regionsDto);
        }


        // get single region (get region by id)
        // get: https://localhost:postnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //step1. Get Data from Database - domain models

            //  var region = dbContext.Regions.Find(id);
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(   r => r.Id == id);   //>> replaced by repository
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null) {
                return NotFound();
            }

            //step2. Map(convert) Domain Models to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,                          // This second way for creating the Dto
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            //step2: return DTO back to client
            return Ok(regionDto);
        }

        //POST to create new region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) // we will use AddRegionRequestDto inside the controller as a parameter 
        {                                                                               // because we want information comming from the client
            //Map or Convert Dto to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            //Use Domain model to create region
            //await dbContext.Regions.AddAsync(regionDomainModel);  // replaced by repository
            //await dbContext.SaveChangesAsync();      // replaced by repository
           regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain model back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };


            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }


        //update region
        //Put: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //check if region exists
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);  //>> replaced by repository

            //map dto to domain model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            };

           regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //if (regionDomainModel == null)        //>> replaced by repository
            //{                                      //>> replaced by repository
            //    return NotFound();                //>> replaced by repository
            //}                                     //>> replaced by repository

            //Map DTO to Domain model
            //regionDomainModel.Code = updateRegionRequestDto.Code;            //>> replaced by repository
            //regionDomainModel.Name = updateRegionRequestDto.Name;                          //>> replaced by repository
            //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;              //>> replaced by repository

            //await dbContext.SaveChangesAsync();                   //>> replaced by repository

            //Convert Domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
               
        }


        //Delete Region
        //DELETE: https://localhost:postnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {   
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);  //>>replaced by repository
            //if(regionDomainModel == null)             //>>replaced by repository
            //{                              //>>replaced by repository
            //return NotFound();           //>>replaced by repository
            //}                                   //>>replaced by repository

            //Delete Region
            //dbContext.Regions.Remove(regionDomainModel);         //>>replaced by repository
            //await dbContext.SaveChangesAsync();         //>>replaced by repository

            //as an option, you can return deleted region back, so for that
            //Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            // and now return the regionDto instead of Ok();
            return Ok(regionDto);
        }

    }
    


}                                                               
