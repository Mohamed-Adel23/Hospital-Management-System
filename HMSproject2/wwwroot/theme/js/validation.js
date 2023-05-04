let lgnbtn = document.querySelector(".lgn");

function validate() {
  let email = document.querySelector(".email");
  let password = document.querySelector(".pass");
  let copassword = document.querySelector(".copass");
  let patient = document.getElementById("patient");
  let doctor = document.getElementById("doctor");
  let nurse = document.getElementById("nurse");
  var validRegex =
    /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
  if (!email.value.match(validRegex)) {
    alert("Invalid email address!");
  } else if (password.value.length < 8) {
    alert("Password should be more than 8 characters!");
  } else if (!patient.checked && !doctor.checked && !nurse.checked) {
    alert("Please slelct who you are!");
  }
}

lgnbtn.addEventListener("click", validate);
