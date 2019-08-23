var Grid = function () {
  var my = this;
  var $grid;
  var formAccess = {};
  
  my.Initialise = function (grid, fields, dataUrl, currentPage) {
    $grid = $(grid);
    formAccess = {
      $gridForm: $("#grid-form"),
      $editValueInput: $("#editValue"),
      $currentPageInput: $("#currentPage")
    };
    if (currentPage > 0 === false) {
      currentPage = 1;
    }
    
    function getCellHtml(field)
    {
      var cell = "<td class='jsgrid-cell' style='width: " + field.width + "px;'>";

      return cell;
    }
    
    if ($grid.length > 0) {

      $grid.jsGrid({
        width: "100%",
        height: "400px",

        autoload: true,
        //sorting: true,
        paging: true,
        pageLoading: true,
        pageSize: 3,
        pageIndex: currentPage,
        rowRenderer: function (item, itemIndex) {
          var rowHtml = "<tr class='jsgrid-row' data-id='" + item.id + "'>";
          for (var i = 0; i < this.fields.length; i++) {
            var field = this.fields[i];
            
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
                  "<input class='jsgrid-button jsgrid-edit-button' type='button' title='Edit'>" +
                  "<input class='jsgrid-button jsgrid-delete-button' type='button' title='Delete'>" +
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
        
        fields: fields,

        controller: {
          loadData: function (filter) {

            var d = $.Deferred();

            $.ajax({
              url: dataUrl,
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

      $grid.on("click",
        ".jsgrid-button",
        function () {
          var $button = $(this);
          var id = $(this).closest("tr").attr("data-id");
          if ($button.hasClass("jsgrid-edit-button")) {
            submitGrid(id, 'Edit');
          } else {
            submitGrid(id, 'Delete');
          }
          
        });

    }

    function submitGrid(id, formAction) {
      var pageIndex = $grid.jsGrid("option", "pageIndex");

      formAccess.$currentPageInput.val(pageIndex);
      formAccess.$editValueInput.val(id);

      formAccess.$gridForm.attr("action", formAction);
      formAccess.$gridForm.submit();
    }
  };
};