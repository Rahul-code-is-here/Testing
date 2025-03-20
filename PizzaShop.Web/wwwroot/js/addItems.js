let selectedModifierGroupForItemArray = [];
$(document).ready(function () {
  // for modifier group
  $(document).on("change", ".ModifierGroup-checkbox", function () {
    let selectedModifierGroupForItemContainer = $(
      "#selectedModifierGroupForItem"
    );
    let ModifierGroupValue = $(this).val();
    console.log(ModifierGroupValue);
    var ModifierGroupText = $(this).next("label").text();
    if ($(this).is(":checked")) {
      // selectedModifierGroupForItemContainer.append(`
      //     <span class="selected-ModifierGroupforItem badge badge-light text-dark  p-2 d-flex align-items-center border" data-value="${ModifierGroupValue}">
      //                   ${ModifierGroupText} <button type="button" class="btn-close btn-close-dark ms-2 remove-ModifierGroupForItem"></button>
      //               </span>
      //   `)
      selectedModifierGroupForItemArray.push({
        ModifierGroupId:ModifierGroupValue,
        MinValue:"",
        MaxValue:""
      })
      console.log("calling ajax");
      ModifierGroupFetch(ModifierGroupValue);
    } else {
      $("#ajaxResponseOfModifierList")
        .find(`.selectedModifier[data-value=${ModifierGroupValue}]`)
        .remove();
        selectedModifierGroupForItemArray = selectedModifierGroupForItemArray.filter(item =>item.ModifierGroupId!=ModifierGroupValue);
      // selectedModifierGroupForItemContainer.find(`.selected-ModifierGroupforItem[data-value=${ModifierGroupValue}]`).remove()
      // selectedModifierGroupForItemArray=selectedModifierGroupForItemArray.filter((item)=>item != ModifierGroupValue)
    }
    console.log(selectedModifierGroupForItemArray);
    // remove item when clicking the close button
    // $(document).on('click','.remove-ModifierGroupForItem',function(){
    //   let badge = $(this).closest('.selected-ModifierGroupforItem');
    //   let value = badge.data("value");
    //   badge.remove()
    //   $(`.ModifierGroup-checkbox[value=${value}]`).prop("checked",false);
    //   selectedModifierGroupForItemArray =selectedModifierGroupForItemArray.filter(item =>item !=value)
    // })
    // handle min selection and max selection
    $(document).on('change','.modifier-min-select',function(){
      let parentModifierDiv = $(this).closest(".selectedModifier");
      let modifierGroupId = parentModifierDiv.data("value");
      let minValue = $(this).val();
      let index = selectedModifierGroupForItemArray.findIndex(item => item.ModifierGroupId == modifierGroupId);
      if (index !== -1) {
        selectedModifierGroupForItemArray[index].MinValue = minValue;
      }
  
      console.log("Updated Array:", selectedModifierGroupForItemArray);
  });
  $(document).on('change','.modifier-max-select',function(){
    let parentModifierDiv = $(this).closest(".selectedModifier");
    let modifierGroupId = parentModifierDiv.data("value");
    let minValue = $(this).val();
    let index = selectedModifierGroupForItemArray.findIndex(item => item.ModifierGroupId == modifierGroupId);
    if (index !== -1) {
      selectedModifierGroupForItemArray[index].MaxValue = minValue;
    }

    console.log("Updated Array:", selectedModifierGroupForItemArray);
});
    })
    // remove SelectedModifierPartailView While Clicking delete button
    $(document).on("click", ".deleteSelectedModifierTrash", function () {
      let partailSeletedModifier = $(this).closest(".selectedModifier");
      let value = partailSeletedModifier.data("value");
      partailSeletedModifier.remove();
      selectedModifierGroupForItemArray = selectedModifierGroupForItemArray.filter(item =>item.ModifierGroupId!=value);
      console.log("dlete called");
      $(`.ModifierGroup-checkbox[value=${value}]`).prop("checked", false);
    });
    // 
  });
  // For adding Items
  $(document).on("click", "#saveblock", function (e) {
    e.preventDefault();
    var formElement = $("#addItems");
    $.validator.unobtrusive.parse(formElement);
    if (formElement.valid()) {
      var formData = new FormData(formElement[0]);
      selectedModifierGroupForItemArray.forEach((item, index) => {
        formData.append(`AddItemsViewModel.ModiferDatas[${index}].ModifierGroupId`, item.ModifierGroupId);
        formData.append(`AddItemsViewModel.ModiferDatas[${index}].MinValue`, item.MinValue);
        formData.append(`AddItemsViewModel.ModiferDatas[${index}].MaxValue`, item.MaxValue);
    });
      showLoader();
      $.ajax({
        url: "/SuperAdmin/AddItemPost",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
          hideLoader();
          window.location.href = result.redirectUrl;
        },
        error: function () {
          hideLoader();
          console.error("An unexpected error occurred.");
        },
      });
    }
  });
  // mass delete and delete
  $(document).on("change", "#selectAllItems", function () {
    if ($(this).is(":checked")) {
      $(".selectItem").prop("checked", true);
    } else {
      $(".selectItem").prop("checked", false);
    }
  });
  $(document).on("change", ".selectItem", function () {
    if ($(".selectItem:checked").length === $(".selectItem").length) {
      $("#selectAllItems").prop("checked", true);
    } else {
      $("#selectAllItems").prop("checked", false);
    }
  });
  let itemIds = [];
  $(document).on("click", "#deleteSelectedItems", function () {
    console.log("arkin");
    itemIds.length = 0; //emptying the array each time
    $(".selectItem:checked").each(function () {
      itemIds.push($(this).attr("data-itemId"));
    });
    console.log(itemIds);
    if (itemIds.length > 0) {
      $("#Delete").modal("show");
    } else {
      toastr.error("No items selected");
    }
  });
  $(document).on("click", ".deleteItem", function () {
    itemIds.length = 0; //emptying the array each time
    let itemId = $(this).attr("data-Delete-itemId");
    itemIds.push(itemId);
  });
  // for confirm delete
  $(document).on("click", "#deleteUserBtn", function () {
    if (itemIds.length != 0) {
      Delete(itemIds);
    }
    console.log("arjun");
  
  
  });
// ajax call to delete Items
function Delete(itemIds) {
  console.log(itemIds);
  $.ajax({
    url: "/SuperAdmin/DeleteItems",
    type: "POST",
    data: {
      itemIds: itemIds,
    },
    success: function (data) {
      window.location.href = data.redirectUrl;
    },
    error: function (xhr, error) {
      var errResponse = JSON.parse(xhr.responseText);
      window.location.href = errResponse.redirectUrl;
      console.log("error:", error);
    },
  });
}

function ModifierGroupFetch(id) {
  console.log("ajax called ");
  $.ajax({
    url: "/SuperAdmin/ModifierList",
    type: "GET",
    data: {
      ModifierGroupId: id,
    },
    success: function (data) {
      $("#ajaxResponseOfModifierList").append(data);
    },
    error: function () {},
  });
}
// // delete Selected Modifier for item
// function DeleteSelectedModifierForItem(){

// }
