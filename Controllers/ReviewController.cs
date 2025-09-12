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
    public class ReviewController : Controller
    {
        private readonly IReviewInterface _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IAnimalInterface _animalRepository;
        private readonly IReviewerInterface _reviewerRepository;

        public ReviewController(IReviewInterface reviewRepository, IMapper mapper, IAnimalInterface animalRepository, IReviewerInterface reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpGet("animal/{animalId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewForAnimal(int animalId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAnimal(animalId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int animalId,[FromBody] ReviewDto reviewCreate)
        {
            if(reviewCreate == null)
            {
                return BadRequest(ModelState);
            }
            var review = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Animal = _animalRepository.GetAnimal(animalId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if(!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateReviewer(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewId != updatedReview.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.ReviewerExists(reviewId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var review = _reviewRepository.GetReview(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }

            return NoContent();
        }
    }
}
