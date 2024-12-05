using asp.net_core_web_api_reference_project.Data;
using asp.net_core_web_api_reference_project.DTO;
using asp.net_core_web_api_reference_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_core_web_api_reference_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get All Regions
        //Get: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
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
            var regionsDomain = dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute] Guid id)
        {   
            //step1. Get Data from Database - domain models

          //  var region = dbContext.Regions.Find(id);
            var regionDomain = dbContext.Regions.FirstOrDefault(   r => r.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto) // we will use AddRegionRequestDto inside the controller as a parameter 
        {                                                                               // because we want information comming from the client
            //Map or Convert Dto to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            //Use Domain model to create region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

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

    }
    


}                                                               
