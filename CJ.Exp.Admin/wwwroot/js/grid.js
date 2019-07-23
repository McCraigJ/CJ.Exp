function editGridRow(id) {
  alert(id);
}

function deleteGridRow(id) {
  alert(id);
}

var Grid = function () {
  var my = this;

  

  my.Initialise = function (grid, fields) {
    var $grid = $(grid);

    //function isObject(val) {
    //  if (val === null) { return false; }
    //  return ((typeof val === 'function') || (typeof val === 'object'));
    //}

    function getCellHtml(field)
    {
      var cell = "<td class='jsgrid-cell' style='width: " + field.width + "px;'>";

      return cell;
    }

    function testEdit() {
      alert("edit clicked");
    }

    if ($grid.length > 0) {
      //var clients = [
      //  { "Name": "Otto Clay", "Age": 25, "Country": 1, "Address": "Ap #897-1459 Quam Avenue", "Married": false },
      //  { "Name": "Connor Johnston", "Age": 45, "Country": 2, "Address": "Ap #370-4647 Dis Av.", "Married": true },
      //  { "Name": "Lacey Hess", "Age": 29, "Country": 3, "Address": "Ap #365-8835 Integer St.", "Married": false },
      //  { "Name": "Timothy Henson", "Age": 56, "Country": 1, "Address": "911-5143 Luctus Ave", "Married": true },
      //  { "Name": "Ramona Benton", "Age": 32, "Country": 3, "Address": "Ap #614-689 Vehicula Street", "Married": false }
      //];

      //var countries = [
      //  { Name: "United States", Id: 1 },
      //  { Name: "Canada", Id: 2 },
      //  { Name: "United Kingdom", Id: 3 }
      //];

      $grid.jsGrid({
        width: "100%",
        height: "400px",

        autoload: true,
        //sorting: true,
        paging: true,
        pageLoading: true,
        pageSize: 3,
        pageIndex: 1,
        rowRenderer: function (item, itemIndex) {
          var rowHtml = "<tr class='jsgrid-row'>";
          for (var i = 0; i < this.fields.length; i++) {
            var field = this.fields[i];
            //rowHtml += "<td class='jsgrid-cell' style='width: " + field.width + "px;'>";


            var val = item[field.name];
            if (field.nestedName !== undefined) {
              // only 1 level of nesting is supported
              //var nestedFieldName = field.name.split(".")[1];
              val = item[field.name][field.nestedName];
            }
            switch (field.type) {
              case "date":
                var dt = new Date(val);
                rowHtml += getCellHtml(field) + dt.toLocaleDateString() + "</td>";
                break;
              case "currency":
                var cur = parseFloat(val);
                rowHtml += getCellHtml(field) + cur.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + "</td>";

                break;
              case "control":
                rowHtml += "<td class='jsgrid-cell jsgrid-control-field jsgrid-align-center' style='width: 50px;'>" +
                  "<input class='jsgrid-button jsgrid-edit-button' type='button' title='Edit' onClick='javascript: editGridRow(\"" + item.id + "\" );'>" +
                  "<input class='jsgrid-button jsgrid-delete-button' type='button' title='Delete'  onClick='javascript: deleteGridRow(\"" + item.id + "\" );'>" +
                  "</td>";
                break;
              default:
                rowHtml += getCellHtml(field) + val + "</td>";
                break;
            }

          }
          rowHtml += "</ tr>";
          return $(rowHtml);
        },
        onEdit: function(item) {
          alert(item);
        },

        //<tr class="jsgrid-row">
        //<td class="jsgrid-cell" style="width: 150px;">2019-06-13T23:00:00Z</td>
        //<td class="jsgrid-cell" style="width: 150px;">Groceries</td>
        //<td class="jsgrid-cell jsgrid-align-right" style="width: 150px;">1000</td>
        //<td class="jsgrid-cell jsgrid-control-field jsgrid-align-center" style="width: 50px;">
        //  <input class="jsgrid-button jsgrid-edit-button" type="button" title="Edit">
        //    <input class="jsgrid-button jsgrid-delete-button" type="button" title="Delete">
        //  </td>
        //</tr>

        //data: countries,

        fields: fields,

        controller: {
          loadData: function (filter) {

            var d = $.Deferred();

            $.ajax({
              url: "GetExpensesData",
              data: filter,
              dataType: "json"
            }).done(function(response) {
              d.resolve({
                data: response.gridRows,
                itemsCount: response.totalRecordCount
              });
            });

            return d.promise();
          }
        }
      });

      

    }
  };
};