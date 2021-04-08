Skip to content
Why GitHub? 
Team
Enterprise
Explore 
Marketplace
Pricing 
Search

Sign in
Sign up
BackEndServices200
/
LibraryApiSolutionBes200-April2021
Template
generated from BES100/LibrarySolutionMarch2021
10
Code
Issues
Pull requests
Actions
Projects
Security
Insights
some docs added and docker-compose.yml updated

 master
@JeffryGonzalez
JeffryGonzalez committed 2 minutes ago 
1 parent d908973 commit 88a74478c2f7cdfd3eb3fefc0d5bf2ef5b468e6e
Showing  with 98 additions and 11 deletions.
 88  common-api-tasks.md 
@@ -0,0 +1,88 @@
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
 21  docker-compose.yml 
@@ -1,14 +1,13 @@
version: "3.7"
services:
  api:
    image: jeffrygonzalez/libraryapi:latest
    depends_on:
      - sql
  cache:
    image: redis:latest
    ports: 
      - 8080:80
    environment:
      - ConnectionStrings__Library=server=sql;database=Library_Prod;user=sa;password=TokyoJoe138!
  sql:
    image: jeffrygonzalez/librarysql-mar-2021:latest
    ports:
      - 1433:1433 
      - 6379:6379
  messaging:
    image: rabbitmq:management
    ports: 
      - 15671:15671
      - 15672:15672
      - 5672:5672

0 comments on commit 88a7447
Please sign in to comment.
© 2021 GitHub, Inc.
Terms
Privacy
Security
Status
Docs
Contact GitHub
Pricing
API
Training
Blog
About
Loading complete