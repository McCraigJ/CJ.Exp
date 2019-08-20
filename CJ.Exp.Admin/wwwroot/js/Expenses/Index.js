$(document).ready(function () {
  var pageOptions = new PageOptions();
  
  var grid = new Grid();

  var fields = [
    { name: "expenseDate", title: "Date", type: "date", width: 150 },
    { name: "expenseType", nestedName: "expenseType", title: "Expense Type", type: "string", width: 150 },
    { name: "expenseValue", title: "Amount", type: "currency", width: 150 },
    { type: "control", hasEdit: true, hasDelete: true }
    ];

  var currentPage = pageOptions.GetPageOption("CurrentPage", "int");

  grid.Initialise($("#jsGrid"), fields, currentPage);
});