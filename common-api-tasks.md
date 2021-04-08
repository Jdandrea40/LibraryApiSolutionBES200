# Some Common things You Want to do with My Api

## Books

You can get a list of books

```
GET http://localhost:1337/books
```

Will give you something like:

```
HTTP/1.1 200 OK
Transfer-Encoding: chunked
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/10.0
X-Powered-By: ASP.NET
Date: Thu, 08 Apr 2021 14:35:00 GMT
Connection: close

{
  "data": [
    {
      "id": 1,
      "title": "Walden",
      "author": "Thoreau",
      "genre": "Philosophy"
    },
    {
      "id": 2,
      "title": "Nature",
      "author": "Emerson",
      "genre": "Philosophy"
    },
    {
      "id": 5,
      "title": "Microservices and BBQ",
      "author": "Jimmy Bogard",
      "genre": "Programming"
    },
    {
      "id": 6,
      "title": "Microservices and BBQ - Part 2 - BRISKETS ALL THE WAY",
      "author": "Jimmy Bogard",
      "genre": "Programming"
    },
    {
      "id": 7,
      "title": "Some Book",
      "author": "Bob Smith"
    }
  ]
}
```

## Adding A Book Reservation

#### Send A Reservation

```
POST http://localhost:1337/reservations
Content-Type: application/json

{
    "for": "Sean",
    "books": "1,2,3,4,5,6"
}
```

Response:
```
HTTP/1.1 201 Created
Transfer-Encoding: chunked
Content-Type: application/json; charset=utf-8
Location: http://localhost:1337/reservations/7
Server: Microsoft-IIS/10.0
X-Powered-By: ASP.NET
Date: Thu, 08 Apr 2021 14:37:25 GMT
Connection: close

{
  "id": 7,
  "for": "Sean",
  "bookIds": "1,2,3,4,5,6",
  "status": "Pending"
}
```