# Pokemon
Rokemon API which enables fetching Pokemon descriptions with the ability to translate descriptiuons also.

# Side note
For characteristics the characteristic calls ID value does not match the ID returned from the same pokemon using the species call therefore the flavour text from species was used instead

# For producation:
- Cache both requests species call and translation to try and reduce response time.
- Add counters and dashboards to see just how much time each call is used
- Counters for exceptions & returns 5xx errors to better see where internal errors lie
- Add a full test set up for integration tests to fully verify use case & integrations tests in smoke environment
- logging should really be cleaned up with exceptions


# How to run the solution:
- dotnetcore: https://dotnet.microsoft.com/download
- Git: https://git-scm.com/download
- Clone this repo
- cd into main repo which contains project tlayer\WebApplication1
- dotnet run

Use postman to target the following
URL https://localhost:5001/Pokemon/{pokemonname}   - standard
URL https://localhost:5001/Pokemon/translated/{pokemonname} - Translated

Alternaively this API has been stood up in azure and is available from the following URL:
https://webapplication120210829113138.azurewebsites.net/Pokemon/{pokemonname}   - standard
https://webapplication120210829113138.azurewebsites.net/Pokemon/translated/{pokemonname} - Translated

# Tests
Project contains unit tests which can be run from Visual studio test explorer