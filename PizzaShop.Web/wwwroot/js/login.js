document.addEventListener("DOMContentLoaded", function () {
    let access = true;
    let pass = document.getElementById("Password");
    let eye = document.getElementById("eye");
    let resetPass = document.getElementById("ConfirmPassword");
    let resetEye = document.getElementById("resetEye");
    console.log("arjun");
    console.log(pass);
    console.log(resetPass);

    if (eye && pass) {
        eye.addEventListener("click", () => {
            if (access) {
                pass.type = "text";
                access = false;
                eye.classList.remove("fa-eye-slash");
                eye.classList.add("fa-eye");
            } else {
                pass.type = "password";
                access = true;
                eye.classList.remove("fa-eye");
                eye.classList.add("fa-eye-slash");
            }
        });
    }
        if (resetEye && resetPass) {
            console.log("Arjun")
            resetEye.addEventListener("click", () => {
                if (access) {
                    resetPass.type = "text";
                    access = false;
                    resetEye.classList.remove("fa-eye-slash");
                    resetEye.classList.add("fa-eye");
                } else {
                    resetPass.type = "password";
                    access = true;
                    resetEye.classList.remove("fa-eye");
                    resetEye.classList.add("fa-eye-slash");
                }
            });
    }
});
