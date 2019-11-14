# CJ.Exp

Core
- No EF
- No Mapping except for other Core things
- Business Logic (services) - CJ.Exp.BusinessLogic
- Data Interfaces - CJ.Exp.DataInterfaces
- Domain Interfaces - CJ.Exp.DomainInterfaces	
- Service Models - CJ.Exp.ServiceModels


Infrastructure
- Data Access - CJ.Exp.Data.EF
- Auth Provider - CJ.Exp.Auth.EFIdentity

UI
- Web 
- API


MongoDb Reference
https://www.nuget.org/packages/AspNetCore.Identity.MongoDbCore/

API Logout
- Store tokens in the database. 
- When creating a token, save to db.
- Tokens not stored against user.
- When validating against a token, check that it exists in the database or in cache
- Cache tokens in memory?
- When a user logs out, delete the record (and remove from cache?)

API Refresh tokens
- Short-lived tokens - e.g. 0.5 hours
- Longer-lived refresh token - e.g. 3 days - Between 0.5 hours and 3 days, using a refresh token will create a new one, which will expire in another 3 days, so sliding time
- Stored in the database against user (user can only be logged in once per browser)
- On Login, token is created. Refresh token is also created and stored in db against user
- When token expires, [authorize] will fail and be a 401 with a specific header or object value (e.g. "Token-Expired": "true") will be returned to the client
- The client will look that type of error and will then call a Refresh endpoint on the api providing the their expired token and the refresh token
- The api will validate the token without the expiry and if valid check if the refresh token matches the one stored in the db
- If matches, generate a new token, store in the in AuthTokens table like normal login, store a new refresh token in the database
