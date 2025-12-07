JokeApi (dotnet)
================
? This README covers **full setup, token generation, endpoints, Swagger, and tests**.

Prerequisites
-------------

*   .NET 8 SDK installed (`dotnet --version`)
*   (Optional) Visual Studio 2026 or VS Code

Run locally
-----------

1.  **Clone the repository**
  
        git clone https://github.com/Ouiame-engineer/joke-api-dotnet
        cd JokeApi
    
2.  **Restore dependencies**
    
        dotnet restore
    
3.  **Run the API**
    
        dotnet run
    
4.  **Swagger UI**
    
    Open your browser at: https://localhost:7063/swagger/index.html
    
    You can see all endpoints and test them interactively.
    

Using the API
-------------

1.  **Get JWT token**
    
    GET `/api/auth/token`
    
    Copy the `token` from the response.
    
2.  **Call endpoints with JWT**
    
    Add header:
    
        Authorization: Bearer <the-generated-token>
    
3.  **Fetch jokes**
    
    GET `/api/jokes/{count}`
    
    Example: `/api/jokes/5`
    
4.  **Sort numbers**
    
    GET `/api/numbers?numbers=5,2,10,3&order=0`
    
    `order=0` ? ascending, `order=1` ? descending
    
    Response contains sorted numbers and total sum.
    

Tests
-----

Run all unit tests:

    dotnet test
