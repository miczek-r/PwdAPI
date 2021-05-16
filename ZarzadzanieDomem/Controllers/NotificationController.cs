using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {

            IEnumerable<Notification> notifications = _notificationRepository.GetAll();
            return Ok(notifications);
        }
        [HttpGet("UserEmail/{email}")]
        public IActionResult GetByUserEmail(string email)
        {
            IEnumerable<Notification> notifications = _notificationRepository.GetByUserEmail(email);
            if (notifications == null)
            {
                return NotFound("Notifications not found");
            }
            return Ok(notifications);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Notification notification)
        {
            if (notification == null)
            {
                return BadRequest("Notification is empty.");
            }
            else if (notification.Sender == notification.ReceiverEmail)
            {
                return BadRequest("You can't send a notification to yourself");
            }
            _notificationRepository.Create(notification);
            _notificationRepository.Save();
            return CreatedAtRoute("GetById", new { Id = notification.NotificationId }, notification);
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(uint id)
        {
            Notification notification = _notificationRepository.GetById(id);
            if (notification == null)
            {
                return NotFound("Notification not found");
            }
            return Ok(notification);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            Notification notification = _notificationRepository.GetById(id);
            if (notification == null)
            {
                return NotFound("Notification not Found");
            }
            _notificationRepository.Delete(notification);
            _notificationRepository.Save();
            return NoContent();

        }
        [HttpPut("ChangeToSeen/{id}")]
        public IActionResult ChangeToSeen(uint id)
        {
            Notification notification = _notificationRepository.GetById(id);
            if (notification == null)
            {
                return NotFound("Notification not found");
            }
            _notificationRepository.ChangeToSeen(notification);
            _notificationRepository.Save();
            return NoContent();
        }
        //Post

    }
}
