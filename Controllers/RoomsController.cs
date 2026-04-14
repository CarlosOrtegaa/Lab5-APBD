using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAll(
            [FromQuery] int? minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? activeOnly)
        {
            IEnumerable<Room> rooms = AppData.Rooms;

            if (minCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
            }

            if (hasProjector.HasValue)
            {
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
            }

            if (activeOnly == true)
            {
                rooms = rooms.Where(r => r.IsActive);
            }

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetById(int id)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public ActionResult<IEnumerable<Room>> GetByBuildingCode(string buildingCode)
        {
            var rooms = AppData.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(rooms);
        }

        [HttpPost]
        public ActionResult<Room> Create(Room room)
        {
            var nextId = AppData.Rooms.Count == 0 ? 1 : AppData.Rooms.Max(r => r.Id) + 1;
            room.Id = nextId;

            AppData.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Room updatedRoom)
        {
            var existingRoom = AppData.Rooms.FirstOrDefault(r => r.Id == id);

            if (existingRoom == null)
            {
                return NotFound();
            }

            existingRoom.Name = updatedRoom.Name;
            existingRoom.BuildingCode = updatedRoom.BuildingCode;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.Capacity = updatedRoom.Capacity;
            existingRoom.HasProjector = updatedRoom.HasProjector;
            existingRoom.IsActive = updatedRoom.IsActive;

            return Ok(existingRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            var hasRelatedReservations = AppData.Reservations.Any(r => r.RoomId == id);

            if (hasRelatedReservations)
            {
                return Conflict("Cannot delete a room with related reservations.");
            }

            AppData.Rooms.Remove(room);

            return NoContent();
        }
    }
}