using AnimalReviewApp.Dto;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimalReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : Controller
    {
        private readonly IAnimalInterface _animalRepository;
        private readonly IMapper _mapper;

        public AnimalController(IAnimalInterface animalRepository, IMapper mapper)
        {
            _animalRepository = animalRepository;
            _mapper = mapper;

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
    }
}
