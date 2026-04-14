using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data
{
    public static class AppData
    {
        public static List<Room> Rooms { get; } = new()
        {
            new Room { Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true },
            new Room { Id = 3, Name = "Room 12", BuildingCode = "A", Floor = 1, Capacity = 10, HasProjector = false, IsActive = true },
            new Room { Id = 4, Name = "Conference 1", BuildingCode = "C", Floor = 3, Capacity = 30, HasProjector = true, IsActive = false },
            new Room { Id = 5, Name = "Room 22", BuildingCode = "B", Floor = 2, Capacity = 15, HasProjector = false, IsActive = true }
        };

        public static List<Reservation> Reservations { get; } = new()
        {
            new Reservation
            {
                Id = 1,
                RoomId = 2,
                OrganizerName = "Anna Kowalska",
                Topic = "REST Workshop",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(12, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 2,
                RoomId = 1,
                OrganizerName = "John Smith",
                Topic = "C# Basics",
                Date = new DateOnly(2026, 5, 11),
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(11, 0),
                Status = "planned"
            },
            new Reservation
            {
                Id = 3,
                RoomId = 3,
                OrganizerName = "Maria Nowak",
                Topic = "LINQ Workshop",
                Date = new DateOnly(2026, 5, 12),
                StartTime = new TimeOnly(8, 30),
                EndTime = new TimeOnly(10, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 4,
                RoomId = 5,
                OrganizerName = "Carlos Ortega",
                Topic = "ASP.NET Core Practice",
                Date = new DateOnly(2026, 5, 13),
                StartTime = new TimeOnly(13, 0),
                EndTime = new TimeOnly(15, 0),
                Status = "planned"
            }
        };
    }
}