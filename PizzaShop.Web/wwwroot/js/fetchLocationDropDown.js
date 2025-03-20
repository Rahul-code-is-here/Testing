$(document).ready(function(){
    // $.ajax({
    //     url: "/Dashboard/GetCountries",
    //     type: 'GET',
    //     success: function(data){
    //         $.each(data, function(i, Country) {
    //             $('#countryId').append($('<option>', {
    //                 value: Country.value,
    //                 text: Country.text
    //             }));
    //         });
    //     },
    //     error: function(error) {
    //         console.error("Error fetching countries:", error);
    //         alert("An error occurred while fetching countries. Please try again.");
    //     }
    // });

    // // Second AJAX call to get roles
    // $.ajax({
    //     url: "/Dashboard/GetRoles",
    //     type: 'GET',
    //     success: function(data){
    //         $.each(data, function(i, role) {
    //             $('#RoleId').append($('<option>', {
    //                 value: role.value, 
    //                 text: role.text
    //             }));
    //         });
    //     },
    //     error: function(error) {
    //         console.error("Error fetching roles:", error);
    //         alert("An error occurred while fetching roles. Please try again.");
    //     }
    // });
    $('#CountryId').change(function(){
        var countryId = $(this).val();
        $('#StateId').empty().append('<option value="">-- Select State --</option>');
        $('#CityId').empty().append('<option value="">-- Select City --</option>');

        if (countryId) {
            $.ajax({
                url: "/Dashboard/GetStates",
                type: 'GET',
                data: { countryId: countryId },
                success: function(data) {
                    $.each(data, function(i, state) {
                        $('#StateId').append($('<option>', {
                            value: state.value, 
                            text: state.text
                        }));
                    });
                },
                error: function(error) {
                    console.error("Error fetching states:", error);
                    alert("An error occurred while fetching states. Please try again.");
                }
            });
        }
    });

    $('#StateId').change(function(){
        var stateId = $(this).val(); 
        $('#CityId').empty().append('<option value="">-- Select City --</option>'); 

        if (stateId) {
            $.ajax({
                url: '/Dashboard/GetCities',
                type: 'GET',
                data: { stateId: stateId },
                success: function(data) {
                    $.each(data, function(i, city) {
                        $('#CityId').append($('<option>', {
                            value: city.value,
                            text: city.text
                        }));
                    });
                },
                error: function(error) {
                    console.error("Error fetching cities:", error);
                    alert("An error occurred while fetching cities. Please try again.");
                }
            });
        }
    });
});
