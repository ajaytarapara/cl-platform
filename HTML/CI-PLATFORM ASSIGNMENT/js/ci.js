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

let flag=0;
function menubartoggle(){ 
  let p=document.getElementById("menubarimg");
  let q=document.getElementById("menubar");
  if(flag%2==0){
    q.setAttribute("style","display:block");
  }
  else{
    q.setAttribute("style","display:none");
  }
  console.log(flag);
  flag++;
  
}
function closemenu(){
  let p=document.getElementById("closingimg");
  let q=document.getElementById("menubar");
  q.setAttribute("style","display:none");
}

let flagfilter=0;
function filterbartoggle(){
  let p=document.getElementById("closingimg");
  let q=document.getElementById("filterbar");
  if(flagfilter%2==0){
    q.setAttribute("style","display:block");
  }
  else{
    q.setAttribute("style","display:none");
  }
  console.log(q);
  console.log(p);
  flagfilter++;
}
function closefilter(){
  let p=document.getElementById("closingimg");
  let q=document.getElementById("filterbar");
  q.setAttribute("style","display:none");
}