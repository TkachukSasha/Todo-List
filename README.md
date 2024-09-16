# Todo-List

# User API Documentation

This README provides information on how to interact with the User API, specifically for retrieving and creating user records.

## Base URL
http://localhost:5000/api

## Endpoints

### 1. **GET /users/v1**

This endpoint retrieves a list of users, with pagination options available.

- **Method**: `GET`
- **URL**: `/users/v1`
  
#### Query Parameters:
- `page`: Specifies the page number (default is 1).
- `results`: The number of results to retrieve per page (default is 10).

#### Example Request:
```http
GET http://localhost:5000/api/users/v1?page=1&results=10
Accept: application/json

#### Example Response:
{
  "empty": false,
  "currentPage": 1,
  "resultsPerPage": 10,
  "totalPages": 1,
  "totalResults": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 1. **POST /users/v1**

This endpoint creates a new user with the provided email address.

- **Method**: `POST`
- **URL**: `/users/v1`

#### Request Body:
```http
POST http://localhost:5000/api/users/v1
Content-Type: application/json

{
  "email" : "tkachukopersonal@gmail.com"
}
```

# To-Do API Documentation

This README provides detailed instructions on how to interact with the To-Do API, including endpoints for managing to-do lists, sharing tasks, and handling related operations.

## Base URL
http://localhost:5000/api

## Endpoints

### 1. **POST /todos/v1**

This endpoint creates a new to-do list for a specific user.

- **Method**: `POST`
- **URL**: `/todos/v1`

#### Request Body:
- `ownerId`: The unique identifier of the user who owns the to-do list.
- `todoLists`: A list of to-do items, each containing:
  - `name`: The name of the to-do list.
  - `priority`: The priority of the task (e.g., 1 for high priority).

#### Example Request:
```http
POST http://localhost:5000/api/todos/v1
Content-Type: application/json

{
  "ownerId" : "8873be91-7e26-4908-8198-36ccba05bda9",
  "todoLists" : [
    {
      "name" : "First",
      "priority" : 1
    }
  ]
}
```

### 2. **POST /todos/v1/shares**

This endpoint shares a task between users.

- **Method**: `POST`
- **URL**: `/todos/v1/shares`

#### Example Request:
```http
POST http://localhost:5000/api/todos/v1/shares
Content-Type: application/json

{
  "taskId" : "206e4fb4-3dba-461e-8aaf-3cd7578d79ca",
  "ownerId" : "6619651f-ecd4-4a60-92c9-d7ad778c99b6",
  "userId" : "206e4fb4-3dba-461e-8aaf-3cd7578d79ca"
}
```

### 3. **PUT /todos/v1**
This endpoint updates an existing task.

- **Method**: `POST`
- **URL**: `/todos/v1/shares`

#### Example Request:
```http
PUT http://localhost:5000/api/todos/v1
Content-Type: application/json

{
  "id" : "206e4fb4-3dba-461e-8aaf-3cd7578d79ca",
  "ownerId" : "6619651f-ecd4-4a60-92c9-d7ad778c99b6",
  "name" : "Change to 2",
  "priority" : 2
}
```

4. **GET /todos/v1/single**

This endpoint retrieves a single task based on its ID and the user ID.

- **Method**: `GET`
- **URL**: `/todos/v1/single`

### Example Request:
```http
GET http://localhost:5000/api/todos/v1/single?id=6619651f-ecd4-4a60-92c9-d7ad778c99b6&userId=206e4fb4-3dba-461e-8aaf-3cd7578d79ca
Accept: application/json
```

5. **GET /todos/v1**

This endpoint retrieves a paginated and sorted list of to-do tasks.

- **Method**: `GET`
- **URL**: `/todos/v1`

Query Parameters:
- sortColumn: Specifies the column to sort by (e.g., Name).
- sortOrder: Specifies the sorting order (asc for ascending or desc for descending).
- page: Specifies the page number to retrieve (default is 1).
- results: The number of results to display per page (default is 10).

### Example Request:
```http
GET http://localhost:5000/api/todos/v1?sortColumn=Name&sortOrder=desc&page=1&results=10
Accept: application/json
```

6. **GET /todos/v1/shares**

This endpoint retrieves shared tasks based on task ID, owner ID, and user ID.

- **Method**: `GET`
- **URL**: `/todos/v1/shares`

Query Parameters:
- taskId: The unique identifier of the task.
- ownerId: The ID of the task owner.
- userId: The ID of the user.

### Example Request:
```http
GET http://localhost:5000/api/todos/v1/shares?taskId=fdsfs&ownerId=6619651f-ecd4-4a60-92c9-d7ad778c99b6&userId=206e4fb4-3dba-461e-8aaf-3cd7578d79ca
Accept: application/json
```

7. **DELETE /todos/v1/shares**

This endpoint removes a shared task.

- **Method**: `GET`
- **URL**: `/todos/v1/shares`

Query Parameters:
- taskId: The unique identifier of the task.
- ownerId: The ID of the task owner.
- userId: The ID of the user.

### Example Request:
```http
DELETE http://localhost:5000/api/todos/v1/shares
Accept: application/json

{
  "taskId" : "",
  "ownerId": "",
  "userId" : ""
}
```

8. **DELETE /todos/v1**

This endpoint deletes a task.

- **Method**: `GET`
- **URL**: `/todos/v1`

Query Parameters:
- id: The unique identifier of the task.
- ownerId: The ID of the task owner.

### Example Request:
```http
DELETE http://localhost:5000/api/todos/v1/shares
Accept: application/json

{
  "id" : "",
  "ownerId" : ""
}
```
