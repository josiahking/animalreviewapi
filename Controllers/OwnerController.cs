using AnimalReviewApp.Dto;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;
using AnimalReviewApp.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimalReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerInterface _ownerRepository;
        private readonly IMapper _mapper;
        private readonly IAnimalInterface _animalRepository;

        public OwnerController(IOwnerInterface ownerRepository, IMapper mapper, IAnimalInterface animalRepository)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{ownerId}/animal")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimalByOwner(int ownerId)
        {
            if(!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<List<AnimalDto>>(_ownerRepository.GetAnimalByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("animal/{animalId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Animal>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerOfAnimal(int animalId)
        {
            if(!_animalRepository.AnimalExists(animalId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnerOfAnimal(animalId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto createOwner)
        {
            if (createOwner == null)
            {
                return BadRequest(ModelState);
            }
            var owner = _ownerRepository.GetOwners()
                .Where(c => c.LastName.Trim().ToUpper() == createOwner.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (owner != null)
            {
                ModelState.AddModelError("", "Category exists");
                return BadRequest(ModelState);
            }
            var ownerMap = _mapper.Map<Owner>(createOwner);
            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while creating owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
            {
                return BadRequest(ModelState);
            }
            if (ownerId != updatedOwner.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!_ownerRepository.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
