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
        private readonly ICountryInterface _countryRepository;

        public OwnerController(
            IOwnerInterface ownerRepository, 
            IMapper mapper, 
            IAnimalInterface animalRepository, 
            ICountryInterface countryRepository
            )
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
            _countryRepository = countryRepository;
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
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto createOwner)
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
                ModelState.AddModelError("", "Owner exists");
                return BadRequest(ModelState);
            }
            var country = _countryRepository.GetCountry(countryId);
            var ownerMap = _mapper.Map<Owner>(createOwner);
            ownerMap.Country = country;
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

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var owner = _ownerRepository.GetOwner(ownerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_ownerRepository.DeleteOwner(owner))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
