using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlataformService.Data;
using PlataformService.Dtos;
using PlataformService.Models;
using PlataformService.SyncDataServices.Http;

namespace PlataformService.Controllers
{
    [Route("api/platform")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClients _commandDataClient;

        public PlatformController(IPlatformRepo repository, IMapper mapper, ICommandDataClients commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");

            var platformItens = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItens));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            if (platformItem != null) 
            {
            
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreatDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreatDto);

            _repository.CreatePlatForm(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);


            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchornously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);


        }

    }
}
