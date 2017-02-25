$(document).ready(function(){
  $("form#new-client").submit(function(event){
    document.getElementById("book-date").defaultValue = "1800-01-01";
  });
  $("form#update").submit(function(event){
    document.getElementById("client-date").defaultValue = "1800-01-01";
  });
});
