using asp.net_core_web_api_reference_project.CustomActionFilters;
using asp.net_core_web_api_reference_project.Data;
using asp.net_core_web_api_reference_project.DTO;
using asp.net_core_web_api_reference_project.Models;
using asp.net_core_web_api_reference_project.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace asp.net_core_web_api_reference_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
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
            //var regionsDto = new List<RegionDto>();  //>>replaced by automapper
            //foreach (var region in regionsDomain) {  //>>replaced by automapper
            //    regionsDto.Add(new RegionDto()       //>>replaced by automapper
            //    {                                    //>>replaced by automapper
            //        Id = region.Id,                          // This is one way for creating the Dto     //>>replaced by automapper
            //        Name = region.Name,              //>>replaced by automapper
            //        Code = region.Code,              //>>replaced by automapper
            //        RegionImageUrl = region.RegionImageUrl,     //>>replaced by automapper
            //    });                                  //>>replaced by automapper
            //}                //>>replaced by automapper

            var regionsDto = mapper.Map< List < RegionDto >>(regionsDomain); // here we are using automapper and regionDomain is the source and regionDto is destination

            //step3. return Dtos

            return Ok(regionsDto);   // you can do directly auto mapping in Ok response, this will make the core even more cleaner
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
            //var regionDto = new RegionDto            //>>replaced by automapper
            //{                                       //>>replaced by automapper
            //    Id = regionDomain.Id,                           // This second way for creating the Dto        //>>replaced by automapper
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,         //>>replaced by automapper
            //    RegionImageUrl = regionDomain.RegionImageUrl,              //>>replaced by automapper
            //};       //>>replaced by automapper  

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            //step2: return DTO back to client
            return Ok(regionDto);
        }

        //POST to create new region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel] // this is ValidateModelAttribute that we just created
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) // we will use AddRegionRequestDto inside the controller as a parameter 
        {                                                                               // because we want information comming from the client

            //if (ModelState.IsValid) // ModelState.IsValid returns an boolean value, it checks if AddRegionRequestDto is valid model or not.   //>> replaced by [ValidateModel}
            //{  //>>replaced by [ValidateModel]
                //Map or Convert Dto to domain model
                //var regionDomainModel = new Region       //>>replaced by automapper
                //{                                        //>>replaced by automapper
                //    Code = addRegionRequestDto.Code,     //>>replaced by automapper
                //    Name = addRegionRequestDto.Name,     //>>replaced by automapper
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,    //>>replaced by automapper
                //};          // replaced by automapper

                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


                //Use Domain model to create region
                //await dbContext.Regions.AddAsync(regionDomainModel);  // replaced by repository
                //await dbContext.SaveChangesAsync();      // replaced by repository
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain model back to Dto
                //var regionDto = new RegionDto     //>>replaced by automapper
                //{                                  //>>replaced by automapper
                //    Id = regionDomainModel.Id,      //>>replaced by automapper
                //    Name = regionDomainModel.Name,    //>>replaced by automapper
                //    Code = regionDomainModel.Code,     //>>replaced by automapper
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,    //>>replaced by automapper
                //};       //>>replaced by automapper



                var regionDto = mapper.Map<RegionDto>(regionDomainModel);


                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

            //} else   //>>replaced by [ValidateModel]
            //{       //>>replaced by [ValidateModel]
            //    return BadRequest(ModelState);    //>>replaced by [ValidateModel]
            //}

        }


        //update region
        //Put: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [ValidateModel]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //if (ModelState.IsValid)      //>>replaced by [ValidateModel]
            //{      //>>replaced by [ValidateModel]
            //check if region exists
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);  //>> replaced by repository

            //map dto to domain model
            //var regionDomainModel = new Region               //>>replaced by automapper
            //{                                                 //>>replaced by automapper
            //    Code = updateRegionRequestDto.Code,            //>>replaced by automapper
            //    Name = updateRegionRequestDto.Name,             //>>replaced by automapper
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl,        //>>replaced by automapper
            //};                                                      //>>replaced by automapper

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
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
                //var regionDto = new RegionDto      //>>replaced by dto
                //{                                 //>>replaced by dto
                //    Id = regionDomainModel.Id,     //>>replaced by dto
                //    Code = regionDomainModel.Code,    //>>replaced by dto
                //    Name = regionDomainModel.Name,     //>>replaced by dto
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,    //>>replaced by dto
                //};                        //>>replaced by dto

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDto);
            //}     //>>replaced by [ValidateModel]
            //else      //>>replaced by [ValidateModel]
            //{        //>>replaced by [ValidateModel]
            //    return BadRequest(ModelState);     //>>replaced by [ValidateModel]
            //}      //>>replaced by [ValidateModel]



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
            //var regionDto = new RegionDto           //>>replaced by dto
            //{                                      //>>replaced by dto
            //    Id = regionDomainModel.Id,           //>>replaced by dto
            //    Code = regionDomainModel.Code,      //>>replaced by dto
            //    Name = regionDomainModel.Name,       //>>replaced by dto
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,      //>>replaced by dto
            //};                                                 //>>replaced by dto

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // and now return the regionDto instead of Ok();        
            return Ok(regionDto);
        }

    }
    


}                                                               
