$(document).ready(function () {
  var pageOptions = new PageOptions();
  
  var grid = new Grid();

  var fields = [
    { name: "expenseType", title: "Expense Type", type: "string", width: 150 },
    { type: "control", hasEdit: true, hasDelete: true }
    ];

  var currentPage = pageOptions.GetPageOption("CurrentPage", "int");

  grid.Initialise($("#jsGrid"), fields, "ExpenseTypes/GetExpenseTypesData", currentPage);
});