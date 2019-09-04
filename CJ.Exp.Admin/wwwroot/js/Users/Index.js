$(document).ready(function () {
  var pageOptions = new PageOptions();
  
  var grid = new Grid();

  var fields = [
    { name: "email", title: "Email", type: "string", width: 150 },
    { name: "firstName", title: "First Name", type: "string", width: 150 },
    { name: "lastName", title: "Last Name", type: "string", width: 150 },
    { type: "control", hasEdit: true, hasDelete: true }
    ];

  var currentPage = pageOptions.GetPageOption("CurrentPage", "int");

  grid.Initialise($("#jsGrid"), fields, "/Users/GetUsersData", currentPage, "Users");
});