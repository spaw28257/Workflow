/**
 * Descripción Permite realizar el registro de los datos de la solicitud
 * */
function AddItemSolicitudPago() {
    var vTituloMensaje = ""
    var vMensaje = "";
    var cantidad = 100;
    var anticipo = 0;

    var preciounitario = $("#txtpreciounitario").val().replaceAll(".", "");
    preciounitario = preciounitario.replaceAll(",", "");

    var porcentajeiva = $("#txtporcentajeiva").val().replaceAll(".", "");
    porcentajeiva = porcentajeiva.replaceAll(",", ".");

    var totaliva = $("#txttotaliva").val().replaceAll(".", "");
    totaliva = totaliva.replaceAll(",", "");

    var porcentajeretencion = $("#txtporcentajeretencion")
        .val()
        .replaceAll(".", "");
    porcentajeretencion = porcentajeretencion.replaceAll(",", "");

    var totalretenido = $("#txttotalretenido").val().replaceAll(".", "");
    var totalretenido = totalretenido.replaceAll(",", "");

    var subtotal = $("#txtsubtotal").val().replace(".", "");
    subtotal = subtotal.replaceAll(",", "");

    var total = $("#txttotal").val().replaceAll(".", "");
    total = total.replaceAll(",", "");

    //Validar campos vRequeridos

    if ($("#txtCodigoPlantilla").val() == "") {
        vMensaje = '<div style="color:red;">* Seleccione el código de la plantilla.</div> <br />';
    }

    if ($("#txtNombrePlantilla").val() == "") {
        vMensaje += '<div style="color:red;">* Seleccione el nombre de la plantilla.</div> <br />';
    }

    if ($("#cbogruporubro").val() == "") {
        vMensaje += '<div style="color:red;">* Seleccione el grupo del rubro.</div> <br />';
    }

    if ($("#cborubro").val() == "") {
        vMensaje += '<div style="color:red;">* Seleccione el rubro.</div> <br />';
    }

    if ($("#txtproveedor").val() == "") {
        vMensaje += '<div style="color:red;">* Seleccione el proveedor.</div> <br />';
    }

    if ($("#cboTipoDocumento").val() == "") {
        vMensaje += '<div style="color:red;">* Seleccione el tipo de documento.</div> <br />';
    }

    if ($("#txtdescripcion").val() == "") {
        vMensaje += '<div style="color:red;">* Especifique una breve descripción del documento.</div> <br />';
    }

    if (document.getElementById("txtfecdocumento").value == "") {
        vMensaje += '<div style="color:red;">* Seleccione la fecha del documento.</div> <br />';
    }

    if (document.getElementById("txtfecpago").value == "") {
        vMensaje += '<div style="color:red;">* Seleccione la fecha del pago.</div> <br />';
    }

    if ($("#cboformapago").val() == "0") {
        vMensaje += '<div style="color:red;">* Seleccione la forma de pago.</div> <br />';
    }

    if ($("#txtpreciounitario").val() == "0,00") {
        vMensaje += '<div style="color:red;">* El monto del documento debe ser mayor a cero.</div> <br />';
    }

    var fileInput = document.getElementById("fileAdjuntarArchivo");
    if (fileInput.files.length == 0) {
        vMensaje += '<div style="color:red;">* Seleccione los soportes del pago.</div> <br />';
    }

    //si existe al mensaje se muestra por pantalla
    if (vMensaje.length > 0) {
        vTituloMensaje = "Mensajes de Error";
        $("#TituloMensaje").html("");
        $("#TituloMensaje").html(vTituloMensaje);

        $("#divMensajesError").html("");
        $("#divMensajesError").html(vMensaje);
        $("#modal-lista-errores").modal("show");
    } else {
        //verifica si el checkbox para calcular iva
        if ($("#chkCalculaIva").is(":checked")) {
            var calculariva = true;
        } else {
            var calculariva = false;
        }

        //verifica si el checkbox para calcular si tiene retensión esta activo
        if ($("#chkRealizarRetencion").is(":checked")) {
            var calcularretension = true;
        } else {
            var calcularretension = false;
        }

        var parametros = [
            $("#cbomoneda").val(), //[0]
            $("#cborubro").val(), //[1]
            $("#txtfecdocumento").val(), //[2]
            $("#txtfecpago").val(), //[3]
            $("#hddidproveedor").val(), //[4]
            $("#txtproveedor").val(), //[5]
            $("#txtdescripcion").val(), //[6]
            $("#txtnumerodocumento").val(), //[7]
            cantidad, //[8]
            preciounitario, //[9]
            anticipo, //[10]
            subtotal, //[11]
            total, //[12]
            $("#hddSolicitudOrdenPago_Id").val(), //[13]
            $("#hddSolicitudOrdenPagoDetalle_Id").val(), //[14]
            $("#cboTipoDocumento").val(), //[15]
            calculariva, //[16]
            calcularretension, //[17]
            porcentajeiva, //[18]
            totaliva, //[19]
            porcentajeretencion, //[20]
            totalretenido, //[21]
            $("#cbogruporubro").val(), //[22]
            $("#cboformapago").val(), //[23]
            $("#txtobservaciones").val(), //[24]
        ];

        $.ajax({
            type: "POST",
            url: "AddItemPago",
            dataType: "json",
            data: { Item: parametros },
            success: function (data) {
                if (data[0].Codigox.length > 0) {
                    swal(
                        data[0].Codigox + " " + data[0].Mensajex,
                        data[0].Titulox,
                        data[0].Tipox
                    );
                } else {
                    swal(
                        "La solicitud de orden de pago se registro con exito",
                        "Información",
                        "info"
                    );
                    $("#hddSolicitudOrdenPago_Id").val(data[0].NumeroRegistroOrdenPagox);
                    $("#hddSolicitudOrdenPagoDetalle_Id").val("-1");
                    $("#numordpago").html("");
                    $("#numordpago").html(
                        "Solicitud de Orden de Pago Manual Número: " +
                        data[0].NumeroRegistroOrdenPagox
                    );

                    //si existen soportes de pago los registra
                    var fileInput = document.getElementById("fileAdjuntarArchivo");
                    if (fileInput.files.length > 0) {
                        SubirSoportesPagos(data[0].NumeroRegistroDetallePagox);
                    }

                    //borrar los campos para ingresar el nuevo pago
                    $("#chkCalculaIva").prop("checked", true);
                    $("#chkRealizarRetencion").prop("checked", true);
                    $("#cboTipoDocumento").val("");
                    $("#cboporcentajeretencion").val("75");
                    $("#cbogruporubro").val("");
                    $("#cbogruporubrodesc").val("");
                    $("#cborubro").val("");
                    $("#cborubrodesc").val("");
                    document.getElementById("txtfecdocumento").value = "";
                    document.getElementById("txtfecpago").value = "";
                    $("#txtrif").val("");
                    $("#hddidproveedor").val("");
                    $("#txtproveedor").val("");
                    $("#txtdescripcion").val("");
                    $("#txtnumerodocumento").val("");
                    $("#txtpreciounitario").val("0,00");
                    $("#txtporcentajeiva").val("0,00");
                    $("#txttotaliva").val("0,00");
                    $("#txtporcentajeretencion").val("0,00");
                    $("#txttotalretenido").val("0,00");
                    $("#txtsubtotal").val("0,00");
                    $("#txttotal").val("0,00");
                    fileInput.value = "";
                    $("#cboformapago").val("0");
                    $("#txtobservaciones").val("");
                }
            },
            error: function (ex) {
                var r = jQuery.parseJSON(response.responseText);
                alert("Message: " + r.Message);
                alert("StackTrace: " + r.StackTrace);
                alert("ExceptionType: " + r.ExceptionType);
            },
        });
    }

    return;
}