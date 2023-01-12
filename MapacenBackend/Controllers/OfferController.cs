﻿using MapacenBackend.Entities;
using MapacenBackend.Models.CommentDtos;
using MapacenBackend.Models.OfferDtos;
using MapacenBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapacenBackend.Controllers
{
    [Route("api/offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _service;

        public OfferController(IOfferService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<int> AddOffer([FromBody] CreateOfferDto dto)
        {
            var offerId = _service.AddOffer(dto);
            return Created($"api/offer/{offerId}", offerId);
        }

        [HttpGet]
        public ActionResult<OffersWithTotalCount> GetOffers(int countyId, string? productName, int? categoryId, int? pageSize, int? pageNumber)
        {
            return Ok(_service.GetOffers(countyId, productName, categoryId, pageSize, pageNumber));
        }

        [HttpGet("comments/{offerId}")]
        public ActionResult<IEnumerable<CommentDto>> GetAllComments([FromRoute] int offerId)
        {
            return Ok(_service.GetAllComments(offerId));
        }

        [HttpGet("comments")]
        public ActionResult<IEnumerable<CommentDto>> GetAllComments([FromQuery] int offerId, [FromQuery] int userId)
        {
            return Ok(_service.GetAllComments(offerId, userId));
        }

        [HttpPost("favourites")]
        public ActionResult AddOfferToFavorites([FromQuery] int offerId, [FromQuery] int favouritesId)
        {
            _service.AddOfferToFavourites(offerId, favouritesId);
            return Ok();
        }

        [HttpGet("favourites/{favouritesId}")]
        public ActionResult<OffersWithTotalCount> GetFavouritesOffers(int favouritesId, int pageSize, int pageNumber)
        {
            return Ok(_service.GetFavouritesOffers(favouritesId, pageSize, pageNumber));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _service.DeleteOffer(id);
            return Ok();
        }
    }
}
