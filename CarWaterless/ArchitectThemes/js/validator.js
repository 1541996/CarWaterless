(function(){
  'use strict';
  
  $(document).ready(function(){

  	let form = $('.bootstrap-form');

  	// On form submit take action, like an AJAX call
    $(form).submit(function(e){

        if (this.checkValidity() == false) {

            Init();

            $(this).addClass('was-validated');
            e.preventDefault();
            e.stopPropagation();
        }

    });

    function Init()
    {
        var action = $("#hdAction").val();

        

        switch (action) {
            case "PortofDischarge":
                {
                    if ($("#Type").val() == $("#hdBorder").val()) {
                        if ($("#SakhanId").val() == "") {
                            $("#SakhanMessage").addClass("d-block");
                        }
                    }
                }
            case "Township":
                {
                    if ($("#StateId").val() == "") {
                        $("#StateMessage").addClass("d-block");
                    }

                    break;
                }
            case "CardRegistrationFees":
                {
                    if ($("#CardType").val() == "") {
                        $("#CardTypeMessage").addClass("d-block");
                    }

                    break;
                }
            case "CarModelYear":
                {
                    if ($("#CarModelYearGroupId").val() == "") {
                        $("#CarModelYearGroupMessage").addClass("d-block");
                    }

                    break;
                }
            case "CarSubBrand":
                {
                    if ($("#CarBrandId").val() == "") {
                        $("#CarBrandMessage").addClass("d-block");
                    }

                    break;
                }
            case "CarDetail":
                {
                    if ($("#CarModelYearGroupId").val() == "") {
                        $("#CarModelYearGroupMessage").addClass("d-block");
                    }

                    if ($("#CarModelYearId").val() == "") {
                        $("#CarModelYearMessage").addClass("d-block");
                    }

                    if ($("#CarGroupId").val() == "") {
                        $("#CarGroupMessage").addClass("d-block");
                    }

                    if ($("#CarBrandId").val() == "") {
                        $("#CarBrandMessage").addClass("d-block");
                    }

                    if ($("#CarSubBrandId").val() == "") {
                        $("#CarSubBrandMessage").addClass("d-block");
                    }

                    if ($("#CountryId").val() == "") {
                        $("#CountryMessage").addClass("d-block");
                    }

                    if ($("#CarEnginePowerId").val() == "") {
                        $("#CarEnginePowerMessage").addClass("d-block");
                    }

                    break;
                }
            case "OGASection":
                {
                    if ($("#OGADepartmentId").val() == "") {
                        $("#OGADepartmentMessage").addClass("d-block");
                    }

                    break;
                }
            case "Users":
                {
                    if ($("#UserType").val() == "") {
                        $("#UserTypeMessage").addClass("d-block");
                    }

                    break;
                }
            case "DocumentTypes":
                {
                    if ($("#FormType").val() == "") {
                        $("#FormTypeMessage").addClass("d-block");
                    }

                    break;
                }
        }


    }

    // On every :input focusout validate if empty
      $(':input').blur(function () {

        let fieldType = this.type;
    	switch(fieldType){
    		case 'text': 
    		case 'password':
            case 'textarea':
                validateText($(this));
                break;
    		case 'email':
                validateEmail($(this));
                break;
    		case 'checkbox':
    			validateCheckBox($(this));
    			break;
            case 'select-one':
    			validateSelectOne($(this));
    			break;
    		case 'select-multiple':
    			validateSelectMultiple($(this));
    			break;
    		default:
	    		break;
    	}
	});


	// On every :input focusin remove existing validation messages if any
    $(':input').click(function(){

    	$(this).removeClass('is-valid is-invalid');

	});

    // On every :input focusin remove existing validation messages if any
    $(':input').keydown(function(){

        $(this).removeClass('is-valid is-invalid');

    });

	// Reset form and remove validation messages
    $(':reset').click(function(){
        $(':input, :checked').removeClass('is-valid is-invalid');
    	$(form).removeClass('was-validated');
    });

  });

    // Validate Text and password
    function validateText(thisObj) {
        let fieldValue = thisObj.val();

       

        if (action == "PaThaKa") {
            $(thisObj).removeClass('is-valid');
            $(thisObj).removeClass('is-invalid');
        }
        else {
            if (fieldValue.length > 0) {
                $(thisObj).addClass('is-valid');

            } else {
                $(thisObj).addClass('is-invalid');
            }
        }
    }

    // Validate Email
    function validateEmail(thisObj) {
        let fieldValue = thisObj.val();
        let pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

        if(pattern.test(fieldValue)) {
            $(thisObj).addClass('is-valid');
        } else {
            $(thisObj).addClass('is-invalid');
        }
    }

    // Validate CheckBox
    function validateCheckBox(thisObj) {
         
        if($(':checkbox:checked').length > 0) {
            $(thisObj).addClass('is-valid');
        } else {
            $(thisObj).addClass('is-invalid');
        }
    }

    // Validate Select One Tag
    function validateSelectOne(thisObj) {

        let fieldValue = thisObj.val();

       
        if (fieldValue != null) {
            $(thisObj).addClass('is-valid');
            
        } else {
            
            $(thisObj).addClass('is-invalid');
        }
    }

    // Validate Select Multiple Tag
    function validateSelectMultiple(thisObj) {

        let fieldValue = thisObj.val();
        
        if(fieldValue.length > 0) {
            $(thisObj).addClass('is-valid');
        } else {
            $(thisObj).addClass('is-invalid');
        }
    }

})();

$(document).ready(function () {

    var action = $("#hdAction").val();

    $(".decimal").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $(".numeric").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    switch (action) {
        case "PortofDischarge":
            {
                $("#SakhanId").change(function () {

                    var id = $("#SakhanId").val();

                    if (id == "") {
                        $("#SakhanMessage").addClass("d-block");
                    }
                    else {
                        $("#SakhanMessage").removeClass("d-block");
                    }


                });

                break;
            }
        case "Township":
            {
                 $("#StateId").change(function () {

                    var id = $("#StateId").val();

                     if (id =="") {
                        $("#StateMessage").addClass("d-block");
                    }
                    else {
                        $("#StateMessage").removeClass("d-block");
                    }


                });

                break;
            }
        case "CardRegistrationFees":
            {
                $("#CardType").change(function () {

                    var id = $("#CardType").val();

                    if (id == "") {
                        $("#CardTypeMessage").addClass("d-block");
                    }
                    else {
                        $("#CardTypeMessage").removeClass("d-block");
                    }


                });

                break;
            }
        case "CarModelYear":
            {
                $("#CarModelYearGroupId").change(function () {

                    var id = $("#CarModelYearGroupId").val();

                    if (id == "") {
                        $("#CarModelYearGroupMessage").addClass("d-block");
                    }
                    else {
                        $("#CarModelYearGroupMessage").removeClass("d-block");
                    }


                });

                break;
            }
        case "CarSubBrand":
            {
                $("#CarBrandId").change(function () {

                    var id = $("#CarBrandId").val();

                    if (id == "") {
                        $("#CarBrandMessage").addClass("d-block");
                    }
                    else {
                        $("#CarBrandMessage").removeClass("d-block");
                    }

                });

                break;
            }
        case "CarDetail":
            {
                $("#CarModelYearGroupId").change(function () {

                    var id = $("#CarModelYearGroupId").val();

                    if (id == "") {
                        $("#CarModelYearGroupMessage").addClass("d-block");
                    }
                    else {
                        $("#CarModelYearGroupMessage").removeClass("d-block");
                    }

                });

                $("#CarModelYearId").change(function () {

                    var id = $("#CarModelYearId").val();

                    if (id == "") {
                        $("#CarModelYearMessage").addClass("d-block");
                    }
                    else {
                        $("#CarModelYearMessage").removeClass("d-block");
                    }

                });

                $("#CarGroupId").change(function () {

                    var id = $("#CarGroupId").val();

                    if (id == "") {
                        $("#CarGroupMessage").addClass("d-block");
                    }
                    else {
                        $("#CarGroupMessage").removeClass("d-block");
                    }

                });

                $("#CarBrandId").change(function () {

                    var id = $("#CarBrandId").val();

                    if (id == "") {
                        $("#CarBrandMessage").addClass("d-block");
                    }
                    else {
                        $("#CarBrandMessage").removeClass("d-block");
                    }

                });

                $("#CarSubBrandId").change(function () {

                    var id = $("#CarSubBrandId").val();

                    if (id == "") {
                        $("#CarSubBrandMessage").addClass("d-block");
                    }
                    else {
                        $("#CarSubBrandMessage").removeClass("d-block");
                    }

                });

                $("#CountryId").change(function () {

                    var id = $("#CountryId").val();

                    if (id == "") {
                        $("#CountryMessage").addClass("d-block");
                    }
                    else {
                        $("#CountryMessage").removeClass("d-block");
                    }

                });

                $("#CarEnginePowerId").change(function () {

                    var id = $("#CarEnginePowerId").val();

                    if (id == "") {
                        $("#CarEnginePowerMessage").addClass("d-block");
                    }
                    else {
                        $("#CarEnginePowerMessage").removeClass("d-block");
                    }

                });

                break;
            }
        case "OGASection":
            {
                $("#OGADepartmentId").change(function () {

                    var id = $("#OGADepartmentId").val();

                    if (id == "") {
                        $("#OGADepartmentMessage").addClass("d-block");
                    }
                    else {
                        $("#OGADepartmentMessage").removeClass("d-block");
                    }

                });

                break;
            }
        case "Users":
            {
                $("#UserType").change(function () {

                    var id = $("#UserType").val();

                    if (id == "") {
                        $("#UserTypeMessage").addClass("d-block");
                    }
                    else {
                        $("#UserTypeMessage").removeClass("d-block");
                    }

                });

                break;
            }
        case "DocumentTypes":
            {
                $("#FormType").change(function () {

                    var id = $("#FormType").val();

                    if (id == "") {
                        $("#FormTypeMessage").addClass("d-block");
                    }
                    else {
                        $("#FormTypeMessage").removeClass("d-block");
                    }

                });

                break;
            }
    }

   

});
