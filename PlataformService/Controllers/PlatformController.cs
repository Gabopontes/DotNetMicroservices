using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlataformService.Data;
using PlataformService.Dtos;
using PlataformService.Models;

namespace PlataformService.Controllers
{
    [Route("api/platform")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreatDto platformCreatDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreatDto);

            _repository.CreatePlatForm(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);


        }

    }
}
