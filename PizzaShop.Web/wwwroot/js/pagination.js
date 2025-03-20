$(document).ready(function () {
    let pageSize = $("#items").val();
    let pageNumber = 1;
    let sortColumn = "name"
    let searchFilter = $('#searchFun').val();
    let sortOrder = "asc";
    // let CurrentPage =1;
    $
    $(document).on('click', '.name', function () {
        sortColumn = "name"
        sortOrder = (sortOrder === "asc") ? "dsc" : "asc";
        console.log(sortColumn, sortOrder);
        loadUsers();
    });
    $(document).on('click', '.role', function () {
        sortColumn = "role"
        sortOrder = (sortOrder === "asc") ? "dsc" : "asc";
        console.log(sortOrder);
        console.log(sortColumn);
        loadUsers();
    });
    $(document).on('keyup', '#searchFun', function () {
        searchFilter = $(this).val();
        console.log("Key pressed: ", $(this).val()); // Debugging
        console.log("Search Filter:", searchFilter);
        pageNumber = 1;
        loadUsers();
    });
    $(document).on('change', '#PageSize', function () {
        pageSize = $(this).val();
        console.log(pageSize);
        pageNumber = 1;
        console.log(pageNumber);
        loadUsers();
    });
    $(document).on('click', '#next', function () {
        pageNumber = pageNumber + 1;
        console.log("arjun");
        loadUsers();
    });
    $(document).on('click', '#previous', function () {
        pageNumber = pageNumber - 1;
        loadUsers();
    });

    function loadUsers() {
        console.log("Arjun")
        $.ajax({
            url: "/SuperAdmin/UserListPartialView",
            type: 'GET',
            data: {
            pageSize: pageSize,
            CurrentPage: pageNumber,
            searchFilter: searchFilter,
            sortOrder: sortOrder,
            sortColumn: sortColumn,
            
        },
            success: function (data) {
                $('#userListContainer').html(data);
            },
            error: function (xhr, status, error) {
                console.log("error occured", error)
            }
    })
}
});