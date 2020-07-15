using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Models;

namespace AdvertApi.Controllers 
{
    [ApiController]
    [Route("adverts/v1")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public AdvertController(IAdvertStorageService advertStorageService)
        {
            this._advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model) 
        {
            string recordId;
            try 
            {
                 recordId = await _advertStorageService.Add(model);

            } 
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception e) 
            {
                return StatusCode(500, e.Message);
            }
            return StatusCode(201, new CreateAdvertResponse{ Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]   
        public async Task<IActionResult> Confirm(ConfirmAvertModel model) 
        {
            try 
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception e) 
            {
                return StatusCode(500, e.Message);
            }
            return new OkResult();
        }
    }
}