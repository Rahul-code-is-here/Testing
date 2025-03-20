function setDeleteUserId(element) {
    var email = element.getAttribute("data-email");
    console.log(email);
    var deleteBtn = document.getElementById("deleteUserBtn");
    deleteBtn.href = '@Url.Action("DeleteUser", "SuperAdmin")'+'?email='+email;
    }