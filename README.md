\# TrainingCenterApi



\## What does the application do?

The application is an ASP.NET Core Web API that manages rooms and reservations for a training center using in-memory data.



\## How is data passed in the API?

\- Route parameters (e.g. `/api/rooms/{id}`, `/building/{buildingCode}`)

\- Query string (e.g. `?minCapacity=20`)

\- Request body in JSON (POST, PUT)



\## What validation is implemented?

\- Required fields: Name, BuildingCode, OrganizerName, Topic

\- Capacity must be greater than 0

\- EndTime must be later than StartTime



\## What HTTP status codes are used?

\- 200 OK – successful read/update

\- 201 Created – resource created

\- 204 No Content – deleted

\- 404 Not Found – resource not found

\- 409 Conflict – business rule violation



\## Business rules

\- Reservation requires existing room

\- Room must be active

\- Reservations cannot overlap

\- Room cannot be deleted if it has reservations

