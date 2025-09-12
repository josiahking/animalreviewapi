using AnimalReviewApp.Dto;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;
using AnimalReviewApp.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnimalReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : Controller
    {
        private readonly IAnimalInterface _animalRepository;
        private readonly IMapper _mapper;
        private readonly IReviewInterface _reviewRepository;

        public AnimalController(IAnimalInterface animalRepository, IMapper mapper, IReviewInterface reviewRepository)
        {
            _animalRepository = animalRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Animal>))]
        public IActionResult GetAnimals()
        {
            var animals = _mapper.Map<List<AnimalDto>>(_animalRepository.GetAnimals());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(animals);
        }

        [HttpGet("{animalId}")]
        [ProducesResponseType(200, Type = typeof(Animal))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimal(int animalId)
        {
            if (!_animalRepository.AnimalExists(animalId))
            {
                return NotFound();
            }
            var animal = _mapper.Map<AnimalDto>(_animalRepository.GetAnimal(animalId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(animal);
        }

        [HttpGet("{animalId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimalRating(int animalId)
        {
            if(!_animalRepository.AnimalExists(animalId))
            {
                return NotFound();
            }

            var rating = _animalRepository.GetAnimalRating(animalId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] AnimalDto animalCreate )
        {
            if(animalCreate == null)
            {
                return BadRequest(ModelState);
            }
            var animal = _animalRepository.GetAnimals()
                .Where(a => a.Name.Trim().ToUpper() == animalCreate.Name.ToUpper())
                .FirstOrDefault();
            if(animal != null)
            {
                ModelState.AddModelError("", "Animal already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animalMap = _mapper.Map<Animal>(animalCreate);
            if(!_animalRepository.CreateAnimal(ownerId, categoryId, animalMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{animalId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateOwner(
            int animalId,
            [FromQuery] int ownerId,
            [FromQuery] int categoryId,
            [FromBody] AnimalDto updatedAnimal)
        {
            if (updatedAnimal == null)
            {
                return BadRequest(ModelState);
            }
            if (animalId != updatedAnimal.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_animalRepository.AnimalExists(animalId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var animalMap = _mapper.Map<Animal>(updatedAnimal);

            if (!_animalRepository.UpdateAnimal(ownerId, categoryId, animalMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{animalId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int animalId)
        {
            if (!_animalRepository.AnimalExists(animalId))
            {
                return NotFound();
            }

            var animal = _animalRepository.GetAnimal(animalId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviews = _reviewRepository.GetReviewsOfAnimal(animalId);

            if(!_reviewRepository.DeleteReviews(reviews.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong while deleting reviews for animal");
            }

            if (!_animalRepository.DeleteAnimal(animal))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
