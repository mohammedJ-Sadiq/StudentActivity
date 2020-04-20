
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
    event.preventDefault();
    bootbox.dialog({
        title: "Confitmation Message!",
        message: "Are You Sure You Want to Delete This Program?", title: "Confirmation Message",
        buttons: {
            main: { label: '<i class="fa fa-times"></i> Cancel', callback: function () { return true; } },
            success: { label: '<i class="fa fa-check"></i> Confirm', callback: function () { $('#DeleteStuProgram').submit(); } }
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
