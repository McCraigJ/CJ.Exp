var adminUser = 
{
  user: "admin",
  pwd: "P@ssw0rd1",
  roles: [ { role: "userAdminAnyDatabase", db: "admin" }, "readWriteAnyDatabase" ]
};

var accessUser = 
{
  user: "webuser",
  pwd: "Password123",
  roles: [
         { role: "readWrite", db: "exp" }	 
      ]
};