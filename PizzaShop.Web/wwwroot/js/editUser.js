$(document).ready(function () {
  let editUserFormData;
  $(document).on("click", "#EditUserSubmitBtn", function (e) {
    e.preventDefault();

    let edituserForm = $("#editUserForm");
    $.validator.unobtrusive.parse(edituserForm);

    editUserFormData = new FormData(edituserForm[0]);
    console.log(editUserFormData);

    if (edituserForm.valid()) {
        showLoader();
      EditUser();
    }
  });

  function EditUser() {
    $.ajax({
      url: "/SuperAdmin/EditUserPost",
      type: "POST",
      data: editUserFormData,
      processData: false,
      contentType: false,
      success: function (data) {
        hideLoader();
        window.location.href = data.redirectUrl;
      },
      error: function (xhr) {
        hideLoader();
        var errorResponse = JSON.parse(xhr.responseText);
        console.log(errorResponse);
        window.location.href = errorResponse.redirectUrl;
      },
    });
  }
});
const fileTag = document.getElementById("actual-file-btn");
const showTag = document.getElementById("Update-tag");

fileTag.addEventListener('change',()=>
    {
        let fileName = fileTag.files[0].name;
        showTag.innerHTML = fileName;
    });