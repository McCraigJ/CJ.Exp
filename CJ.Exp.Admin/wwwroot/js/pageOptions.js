var PageOptions = function() {
  var my = this;

  my.GetPageOption = function(key, type) {
    var val = $("#" + key).val();
    if (type === "int") {
      return parseInt(val);
    } else {
      return val;
    }
  };
};