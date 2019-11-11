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
