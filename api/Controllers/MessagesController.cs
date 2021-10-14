using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers {
    [Authorize]
    [ServiceFilter (typeof (LogUserActivity))]
    [Route ("api/users/{userId}/[controller]")]
    public class MessagesController : Controller {
        private readonly IMapper _mapper;
        private readonly IDatingRepository _repo;
        public MessagesController (IDatingRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet ("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage (int userId, int id) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var messageFromRepo = await _repo.GetMessage (id);
            if (messageFromRepo == null) return NotFound ();
            return Ok (messageFromRepo);
        }

        [HttpGet ("thread/{id}")]
        public async Task<IActionResult> GetMessageThread (int userId, int id) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();

            var messagesFromRepo = await _repo.GetMessageThread (userId, id);

            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>> (messagesFromRepo);

            return Ok (messageThread);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser (int userId, MessageParams messageParams) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var messagesFromRepo = await _repo.GetMessagesForUser (messageParams);
            var messages = _mapper.Map<IEnumerable<MessageToReturnDto>> (messagesFromRepo);
            Response.AddPagination (messagesFromRepo.Currentpage,
                messagesFromRepo.PageSize,
                messagesFromRepo.TotalCount,
                messagesFromRepo.TotalPages);
            return Ok (messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreatMessage (int userId, [FromBody] MessageForCreationDto messageForCreationDto) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);

            if (currentUserId != userId) return Unauthorized ();
            messageForCreationDto.SenderId = currentUserId;
            var recipient = await _repo.GetUser (messageForCreationDto.RecipientId);
            var sender = await _repo.GetUser (messageForCreationDto.SenderId);

            if (recipient == null) return BadRequest ("Cannot find the recipient");

            var message = _mapper.Map<Message> (messageForCreationDto); // get the message from the body
            _repo.Add (message);
            var messageToReturn = _mapper.Map<MessageToReturnDto> (message);

            if (await _repo.SaveAll ()) return CreatedAtRoute ("GetMessage", new { id = message.Id }, messageToReturn);
            throw new Exception ("Creating the message failed on save");

        }

        [HttpPost ("{id}")]
        public async Task<IActionResult> DeleteMessage (int id, int userId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();

            var messageFromRepo = await _repo.GetMessage (id);
            if (messageFromRepo.SenderId == userId) messageFromRepo.SenderDeleted = true;
            if (messageFromRepo.RecipientId == userId) messageFromRepo.RecipientDeleted = true;
            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted) _repo.Delete (messageFromRepo);
            if (await _repo.SaveAll ()) return NoContent ();
            throw new Exception ("Message could not be deleted");
        }

        [HttpPost ("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead (int userId, int id) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var message = await _repo.GetMessage (id);

            if (message.RecipientId != userId) return BadRequest ("Failed to mark message as read");
            message.IsRead = true;
            message.DateRead = DateTime.UtcNow;
            await _repo.SaveAll ();
            return NoContent ();
        }
    }

}