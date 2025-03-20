$('document').ready(function(){
    $('#items').change(function(){
        var items = $(this).val();
        $.ajax({
            url:"/user/GetUserList",
            data:{items:items},
            success:function(data){
                
            }
        })
    })
})