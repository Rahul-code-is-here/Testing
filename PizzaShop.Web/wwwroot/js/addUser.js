$(document).ready(function(){
    let addUserFormData;
    $(document).on('click','#addUserSubmitBtn',function(e){
        e.preventDefault();
        let addUserForm = $('#addUserForm');
        $.validator.unobtrusive.parse(addUserForm);
        addUserFormData = new FormData(addUserForm[0]);
        if(addUserForm.valid()){
            showLoader();
            addUser();
        }
    })
    function addUser(){
        $.ajax({
            url:'/SuperAdmin/AddNewUserPost',
            type:'POST',
            data:addUserFormData,
            processData:false,
            contentType:false,
            success:function(data){
                hideLoader();
                console.log(data.redirectUrl)
                window.location.href = data.redirectUrl;
            },
            error:function(xhr,err){
                hideLoader();
                console.log(err);
                var errResponse = JSON.parse(xhr.responseText);
                window.location.href = errResponse.redirectUrl;
            }
        })
    }
})
// for file input
const fileTag = document.getElementById("actual-file-btn");
const showTag = document.getElementById("Update-tag");

fileTag.addEventListener('change',()=>
    {
        let fileName = fileTag.files[0].name;
        showTag.innerHTML = fileName;
    });