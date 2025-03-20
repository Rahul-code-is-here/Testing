$(document).ready(function () {
  let existingModifiers = [];
  let itemId = 0;
  $(document).on("click",".editItems", function () {
    itemId = $(this).attr("data-itemId");
    console.log(itemId);
    getItemForEdit();
  });
  // ajax call to get item for editItem
  function getItemForEdit() {
    $.ajax({
      url: "/SuperAdmin/GetItemForEdit",
      type: "GET",
      data: {
        id: itemId,
      },
      success: function (data) {
        $("#editItemId").val(data.items.itemId);
        $("#editItemCategoryId").val(data.items.categoryId);
        $("#editItemName").val(data.items.itemName);
        $("#editItemType").val(data.items.itemType);
        $("#editItemRate").val(data.items.rate);
        $("#editItemQuantity").val(data.items.quantity);
        $("#editItemUnit").val(data.items.unit);
        $("#editItemIsAvailable").prop("checked", data.items.isAvailable);
        $("#editItemDefualt").prop("checked", data.items.isAvailable);
        $("#editItemTaxPercentage").val(data.items.taxPercentage);
        $("#editItemShortCode").val(data.items.shortCode);
        $("#editItemDescription").val(data.items.description);
        existingModifiers= data.items.modiferDatas
        console.log(existingModifiers);
        existingModifiers.forEach((item) => {
          fetchingExistingModifiersOfItemforEdit(item.modifierGroupId,data.items.itemId)
        });
        // checking the chekboxes
        $('.edit-ModifierGroup-checkbox')
      },
      error: function () {
        hideLoader();
      },
    });
  }
  let editForm;
  $(document).on("click", "#editItemSubmit", function (e) {
    e.preventDefault();
    
    console.log($.fn.validate);
    console.log($.validator);
    console.log($.validator.unobtrusive);

    $.validator.unobtrusive.parse("#editItemsForm");
    console.log($("#editItemsForm").valid());
   
    var editFormSelector = $("#editItemsForm");
    editForm = new FormData(editFormSelector[0]);
    console.log(editForm);
    if (editFormSelector.valid()) {
        showLoader();
        editItemPost();
    }
  });
  function editItemPost() {
    console.log("ajax call");
    $.ajax({
      url: "/SuperAdmin/EditItemPost",
      type: "POST",
      data: editForm,
      processData: false, 
      contentType: false,
      success: function (data) {
        hideLoader();
        window.location.href = data.redirectUrl;
      },
      error: function () {
        hideLoader(); 
      },
    });
  }
});

// function ajax call for the fetched data of edit
function fetchingExistingModifiersOfItemforEdit(modifierGroupId,itemId){
    $.ajax({
      url:'/SuperAdmin/FetchingModifierListforEditItem',
      type:'GET',
      data:{
        ModifierId:modifierGroupId,
        ItemId:itemId
      },
      success:function(data){
        $("#edit-ajaxResponseOfModifierList").append(data);
      },
      error:function(xhr,error){

      }
    })
}
// function restoreCheckBoX
