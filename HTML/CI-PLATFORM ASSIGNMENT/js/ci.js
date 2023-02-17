var elements = document.getElementsByClassName("card");

// Declare a loop variable
var i;
function listview() {
    for (i = 0; i < elements.length; i++) {
      elements[i].style.width = "100%";
    }
  }
  function gridView() {
    for (i = 0; i < elements.length; i++) {
      elements[i].style.width = "32%";
    }
  }

  
  