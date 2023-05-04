let signbtn = document.querySelector(".sign");

function validate2() {
  let email = document.querySelector(".email");
  let password = document.querySelector(".pass");
  let copassword = document.querySelector(".copass");
  var validRegex =
    /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
  if (!email.value.match(validRegex)) {
    alert("Invalid email address!");
  } else if (password.value.length < 8) {
    alert("Password should be more than 8 characters!");
  } else if (password.value != copassword.value) {
    alert("Password and confirm password are't the same!");
  } else if (SSN.value == "" || age.value == "" || phone.value == "") {
    alert("Field is empty");
  }
}

signbtn.addEventListener("click", validate2);
