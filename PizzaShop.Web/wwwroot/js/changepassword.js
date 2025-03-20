document.addEventListener("DOMContentLoaded", function () {
  let access = true;
  let Password = document.getElementById("Password");
  let New_Pasword = document.getElementById("NewPassword");
  let Confirm_New_Password = document.getElementById("ConfirmNewPassword");
  let eyeP = document.getElementById("eyeP");
  let eyeNP = document.getElementById("eyeNP");
  let eyeCNP = document.getElementById("eyeCNP");
  console.log("arjun");
  console.log(New_Pasword+"aerjun");
  console.log(Confirm_New_Password + "arjun")

  if (eyeP && Password) {
    eyeP.addEventListener("click", () => {
      if (access) {
        Password.type = "text";
        access = false;
        eyeP.classList.remove("fa-eye-slash");
        eyeP.classList.add("fa-eye");
      } else {
        Password.type = "password";
        access = true;
        eyeP.classList.remove("fa-eye");
        eyeP.classList.add("fa-eye-slash");
      }
    });
  }
  if (eyeNP && New_Pasword) {
    eyeNP.addEventListener("click", () => {
      if (access) {
        New_Pasword.type = "text";
        access = false;
        eyeNP.classList.remove("fa-eye-slash");
        eyeNP.classList.add("fa-eye");
      } else {
        New_Pasword.type = "password";
        access = true;
        eyeNP.classList.remove("fa-eye");
        eyeNP.classList.add("fa-eye-slash");
      }
    });
  }
  if (eyeCNP && Confirm_New_Password) {
    eyeCNP.addEventListener("click", () => {
      if (access) {
        Confirm_New_Password.type = "text";
        access = false;
        eyeCNP.classList.remove("fa-eye-slash");
        eyeCNP.classList.add("fa-eye");
      } else {
        Confirm_New_Password.type = "password";
        access = true;
        eyeCNP.classList.remove("fa-eye");
        eyeCNP.classList.add("fa-eye-slash");
      }
    });
  }
});
