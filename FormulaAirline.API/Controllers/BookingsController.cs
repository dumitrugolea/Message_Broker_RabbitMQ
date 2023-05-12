using FormulaAirlane.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirlane.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly IMessageProducer _messageProducer;

    //In-Memory db
    public static readonly List<Booking> _bookings = new();

    public BookingsController(
        ILogger<BookingsController> logger,
        IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        
        _bookings.Add(newBooking);
        _messageProducer.SendingMessage<Booking>(newBooking);
        return Ok();
    }
}