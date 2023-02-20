var elements = document.getElementsByClassName("card");
var img=document.getElementsByClassName("card-img-top");
var cardbody=document.getElementsByClassName("card-body")

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
