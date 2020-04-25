//--------------------------------------------------------------------------------------------------
    //Delete Program Confirmation Messages
//--------------------------------------------------------------------------------------------------


$(document).on("click", ".DeleteProgramConfirmationEng", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Delete This Program?",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' Confirm', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i>' + ' Cancel', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

$(document).on("click", ".DeleteProgramConfirmationAr", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد",
        message: "هل انت متاكد انك تريد حذف هذا البرنامج؟",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' تأكيد', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i> الغاء', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

//--------------------------------------------------------------------------------------------------
        //Register Program Confirmation Messages
//--------------------------------------------------------------------------------------------------


$(document).on("click", ".RegisterProgramConfirmationEng", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Register in This Program?",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' Confirm', className: 'btn-primary' },
            cancel: { label: '<i class="fa fa-times"></i>' + ' Cancel', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

$(document).on("click", ".RegisterProgramConfirmationAr", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد",
        message: "هل انت متاكد انك تريد التسجيل في هذا البرنامج؟",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' تأكيد', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i> الغاء', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

//--------------------------------------------------------------------------------------------------
        //Leave Club Confirmation Messages
//--------------------------------------------------------------------------------------------------


$(document).on("click", ".LeaveClubConfirmationEng", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Leave This Club?",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' Confirm', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i>' + ' Cancel', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

$(document).on("click", ".LeaveClubConfirmationAr", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد",
        message: "هل انت متاكد انك تريد مغادرة هذا النادي؟",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' تأكيد', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i> الغاء', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});


//--------------------------------------------------------------------------------------------------
        //Delete Program By Admin Confirmation Messages
//--------------------------------------------------------------------------------------------------


$(document).on("click", ".DeleteProgramByAdminConfirmationEng", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Delete This Program?",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' Confirm', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i>' + ' Cancel', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

$(document).on("click", ".DeleteProgramByAdminConfirmationAr", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد",
        message: "هل انت متاكد انك تريد حذف هذا البرنامج؟",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' تأكيد', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i> الغاء', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

//--------------------------------------------------------------------------------------------------
        //Approve Program By Admin Confirmation Messages
//--------------------------------------------------------------------------------------------------


$(document).on("click", ".ApproveProgramConfirmationEng", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Approve This Program?",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' Confirm', className: 'btn-primary' },
            cancel: { label: '<i class="fa fa-times"></i>' + ' Cancel', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

$(document).on("click", ".ApproveProgramConfirmationAr", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد",
        message: "هل انت متاكد انك تريد الموافقة هذا البرنامج؟",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' تأكيد', className: 'btn-primary' },
            cancel: { label: '<i class="fa fa-times"></i> الغاء', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

//--------------------------------------------------------------------------------------------------
        //Reject Program By Admin Confirmation Messages
//--------------------------------------------------------------------------------------------------


$(document).on("click", ".RejectProgramConfirmationEng", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Reject This Program?",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' Confirm', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i>' + ' Cancel', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

$(document).on("click", ".RejectProgramConfirmationAr", function (e) {
    var link = $(this).attr("href"); // "get" the intended link from the button and save it in a var 
    e.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد",
        message: "هل انت متاكد انك تريد رفض هذا البرنامج؟",
        buttons: {
            confirm: { label: '<i class="fa fa-check"></i>' + ' تأكيد', className: 'btn-danger' },
            cancel: { label: '<i class="fa fa-times"></i> الغاء', className: 'btn-default' }
        },
        callback: function (result) {
            if (result) {
                document.location.href = link;  // Return to the button to Continue its job
            }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });;
});

//-------------------------------End----------------------------------


//--------------------------------------------------------------------------------------------------
        // From Now The Code Are Not Used In The Website.
//--------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------
//Alert message
function AlertBootBoxEng() {
    event.preventDefault();
    bootbox.alert({
        message: "This is an alert with a callback!",
        callback: function () {
            console.log('This was logged in the callback!');
        }
    }).find('.modal-content').css({ 'text-align': 'center' });
}


//------------------------------------------------------------------------------------
        //InComplete Alerts
//------------------------------------------------------------------------------------


//Mosh Code half correct

//$("#programs").on("click", function () {
//    var button = $(this);
//    var studentId = item.StudentId;
//    var programId = item.ProgramId;
//    box.confirm("Are u sure"), function (result) {
//        if (result) {
//            $.ajax({
//                url: "/api/Program/DeleteStuPrg",
//                data: { studentId, programId},
//                method: "DELETE",
//                success: function () {
//                    button.parents("tr").remove();
//                }
//            });
//        }
//    }
//});


//bootbox.setDefaults({ 'locale': 'ar' });

function SaveAlertBootBoxEng() {
    event.preventDefault();
    bootbox.confirm({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Save The Changes?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Cancel'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Confirm'
            }
        },
        callback: function (result) {
            $('#myForm').submit();

        }
    }).find('.modal-content').css({ 'text-align': 'center' });
}

function SaveAlertBootBoxAr() {
    event.preventDefault();
    bootbox.confirm({
        title: "رسالة تأكيد!",
        message: "هل انت متاكد انك تريد حفظ التغييرات؟",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> الغاء'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> تأكيد'
            }
        },
        callback: function (result) {
            $('#myForm').submit();

        }
    }).find('.modal-content').css({ 'text-align': 'center' });
}

//---------------------------------
function DeletePAlertBootBoxEn() {

    bootbox.dialog({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Delete This Program?", title: "Confirmation Message",
        buttons: {
            main: { label: '<i class="fa fa-times"></i> Cancel', callback: function () { return true; } },
            success: { label: '<i class="fa fa-check"></i> Confirm', callback: function () { return false; } }
            //success: { label: '<i class="fa fa-check"></i> Confirm', callback: function () { $("#input").attr("width", "500")$('#DeleteStuProgram').submit(); } }

        }

    }).find('.modal-content').css({ 'text-align': 'center' });
}

function DeletePAlertBootBoxAr() {
    event.preventDefault();
    bootbox.dialog({
        title: "Confitmation Message!",
        message: "هل انت متاكد انك تريد حذف هذا البرنامج؟", title: "رسالة تأكيد!",
        buttons: {
            main: { label: '<i class="fa fa-times"></i> الغاء', callback: function () { return true; } },
            success: { label: '<i class="fa fa-check"></i> تأكيد', callback: function () { $('#DeleteStuProgram').submit(); } }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });
}

//---------------------------------
//RegirsterProgramAlerts
//---------------------------------
function RegirsterPAlertBootBoxEn(programId) {
    event.preventDefault();
    bootbox.dialog({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Regirster in This Program?", title: "Confirmation Message",
        buttons: {
            main: { label: '<i class="fa fa-times"></i> Cancel', callback: function () { return true; } },
            success: { label: '<i class="fa fa-check"></i> Confirm', callback: function () { document.getElementById(programId).submit(); } }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });
}

function RegirsterPAlertBootBoxAr() {
    event.preventDefault();
    bootbox.dialog({
        title: "Confitmation Message!",
        message: "هل انت متاكد انك تريد التسجيل في هذا البرنامج؟", title: "رسالة تأكيد!",
        buttons: {
            main: { label: '<i class="fa fa-times"></i> الغاء', callback: function () { return true; } },
            success: { label: '<i class="fa fa-check"></i> تأكيد', callback: function () { $('#RegirsterStuProgram').submit(); } }
        }
    }).find('.modal-content').css({ 'text-align': 'center' });
}

//---------------------------------

function SaveAlertBootBoxEng1() {
    event.preventDefault();
    bootbox.confirm({
        size: "small", message: "Are you sure to save changes?", callback: function (confirmed) {
            if (confirmed) {
                bootbox.alert("Successfully Saved");
                $('#myForm').submit();
            } else {
                bootbox.alert("Cancelled");
            }
        }
    });
}

function SaveAlertBootBoxAr1() {
    event.preventDefault();
    bootbox.confirm({
        size: "small", message: "هل انت متاكد انك تريد حفظ التغييرات؟", callback: function (confirmed) {
            if (confirmed) {
                bootbox.alert("تم حفظ التغييرات بنجاح");
                $('#myForm').submit();
            } else {
                bootbox.alert("Cancelled");
            }
        }
    });
}

$(function () {
    $('#SaveAlert').click(function () {
        event.preventDefault();
        bootbox.dialog({
            message: "Do you want to continue ?", title: "Management",
            buttons: {
                main: { label: "Cancel", className: "btn btn-default", callback: function () { return true; } },
                success: { label: "Continuo", className: "btn btn-default", callback: function () { $('#myForm').submit(); } }
            }
        });
    });
});