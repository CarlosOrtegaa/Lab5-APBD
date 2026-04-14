using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll(
            [FromQuery] DateOnly? date,
            [FromQuery] string? status,
            [FromQuery] int? roomId)
        {
            IEnumerable<Reservation> reservations = AppData.Reservations;

            if (date.HasValue)
            {
                reservations = reservations.Where(r => r.Date == date.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                reservations = reservations.Where(r =>
                    r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (roomId.HasValue)
            {
                reservations = reservations.Where(r => r.RoomId == roomId.Value);
            }

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetById(int id)
        {
            var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

            if (room == null)
            {
                return NotFound("Room does not exist.");
            }

            if (!room.IsActive)
            {
                return Conflict("Cannot create a reservation for an inactive room.");
            }

            var hasOverlap = AppData.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date == reservation.Date &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime);

            if (hasOverlap)
            {
                return Conflict("Reservation overlaps with an existing reservation.");
            }

            var nextId = AppData.Reservations.Count == 0
                ? 1
                : AppData.Reservations.Max(r => r.Id) + 1;

            reservation.Id = nextId;
            AppData.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation updatedReservation)
        {
            var existingReservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

            if (existingReservation == null)
            {
                return NotFound();
            }

            var room = AppData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

            if (room == null)
            {
                return NotFound("Room does not exist.");
            }

            if (!room.IsActive)
            {
                return Conflict("Cannot assign a reservation to an inactive room.");
            }

            var hasOverlap = AppData.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == updatedReservation.RoomId &&
                r.Date == updatedReservation.Date &&
                updatedReservation.StartTime < r.EndTime &&
                updatedReservation.EndTime > r.StartTime);

            if (hasOverlap)
            {
                return Conflict("Reservation overlaps with an existing reservation.");
            }

            existingReservation.RoomId = updatedReservation.RoomId;
            existingReservation.OrganizerName = updatedReservation.OrganizerName;
            existingReservation.Topic = updatedReservation.Topic;
            existingReservation.Date = updatedReservation.Date;
            existingReservation.StartTime = updatedReservation.StartTime;
            existingReservation.EndTime = updatedReservation.EndTime;
            existingReservation.Status = updatedReservation.Status;

            return Ok(existingReservation);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            AppData.Reservations.Remove(reservation);

            return NoContent();
        }
    }
}