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
