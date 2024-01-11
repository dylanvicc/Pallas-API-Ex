An example backend developed in .NET Core. Foundation for the Pallas mobile application's alpha backend. Supports login authorization via JWT, Swagger UI, & Swagger Authorization.

A service architecture is implemented to run asynchronously alongside entity framework to handle SQL server database transactions.

Example database structure is as follows.

RegisteredUsers

| Username | Password | 
| :---:   | :---: |

Inventory

| Index | Metric | Description | Quantity | 
| :---:   | :---: | :---: | :---: |

Password hashing is implemented. Example uses SHA1. However, the service could be modified to support any algorithm.

Resulting authorization endpoint.

````c#
[HttpPost]
[Route("login")]
public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
{
    if (credentials.Username == null || credentials.Password == null)
      return BadRequest();

    var exists = await _loginService.ValidateLoginAsync(credentials);

    if (exists == false)
      return Unauthorized();

    var token = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), credentials);

    if (token == null)
      return BadRequest();

    HttpContext.Session.SetString("Token", token);
    return new JsonResult(token);
}
````
