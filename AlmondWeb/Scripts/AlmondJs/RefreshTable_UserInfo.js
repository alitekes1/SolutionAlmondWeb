function toastrSuccess(context) {
    toastr.success(context, "İşlem Başarılı.", { closeButton: true, timeOut: 1500 });
}
function toastrFail(context) {
    toastr.warning(context, "İşlem Başarısız.", { closeButton: true, timeOut: 1500 });
}
function toastrInfo(context) {
    toastr.info(context, "Uyarı!", { closeButton: true, timeOut: 1500 })
}
function ReLoadTable(controller, action, refreshdivID) {
    let table = $("#" + refreshdivID);
    $.ajax({
        method: "GET",
        url: '/' + controller + '/' + action,
        success: function (tableData) {
            table.empty(); // Tabloyu temizliyoruz

            table.append(tableData);
        }
    });
}
function InfoUserwithToastr(result, controllerforRefreshTable, actionforRefreshTable, successContext, failContext, refreshdivID = "adminlayout") {
    if (result > 0) {
        ReLoadTable(controllerforRefreshTable, actionforRefreshTable, refreshdivID);
        toastrSuccess(successContext);
    }
    else if (result == 0) {
        ReLoadTable(controllerforRefreshTable, actionforRefreshTable, refreshdivID);
        toastrInfo("Herhangi bir değişlik yapılmadı.");
    }
    else {
        toastrFail(failContext);
    }
}
