/**
 * Descripción Permite realizar el registro de los datos de la solicitud
 * */
function AddItemSolicitudPago() {
    var vTituloMensaje = ""
    var vMensaje = "";

    //Validar campos vRequeridos

    if ($("#txtCodigoPlantilla").val() == "") {
        vMensaje = '<div style="color:red;">* Seleccione el código de la plantilla.</div> <br />';
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

    if ($("#txtmontodocumento").val() == "0,00") {
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

        var vBaseIvaGE = $("#txtBaseIvaGE").val().replaceAll(".", "");
        var vBaseIvaRe = $("#txtBaseIvaRe").val().replaceAll(".", "");
        var vBaseIvaAd = $("#txtBaseIvaAd").val().replaceAll(".", "");
        var vMontoDocumento = $("#txtmontodocumento").val().replaceAll(".", "");

        var parametros = [
            $("#txtCodigoPlantilla").val(), //[0]
            $("#cbogruporubro").val(), //[1]
            $("#cborubro").val(), //[2]
            $("#txtProveedorId").val(), //[3]
            $("#txtrif").val(), //[4]
            $("#txtproveedor").val(), //[5]
            $("#txtTaxScheduleID").val(), //[6] Plan de Impuesto
            $("#cboTipoDocumento").val(), //[7]
            $("#txtdescripcion").val(), //[8]
            $("#txtnumerodocumento").val(), //[9]
            $("#txtnumcontrol").val(), //[10]
            $("#txtfecdocumento").val(), //[11]
            $("#txtfecpago").val(), //[12]
            $("#cboformapago").val(), //[13]
            vMontoDocumento, //[14]
            $("#cbomoneda").val(), //[15]
            vBaseIvaGE, //[16]
            vBaseIvaRe, //[17]
            $("#txttotal").val(), //[18] Total a Pagar
            $("#txtobservaciones").val(), //[19]
            $("#txtcodigoSolicitud").val(), //[20]
            $("#cboConcepto").val(), //[21]
            vBaseIvaAd //[22]
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
                        "La solicitud de orden de pago se registro con exito con el número: " + data[0].NumeroRegistroOrdenPagox,
                        "Información",
                        "info"
                    );

                    //si existen soportes de pago los registra
                    var fileInput = document.getElementById("fileAdjuntarArchivo");
                    if (fileInput.files.length > 0) {
                        /*subir archivos*/
                        SubirSoportesPagos(data[0].NumeroRegistroOrdenPagox, data[0].NumeroRegistroDetallePagox, $("#txtfecpago").val());
                    }

                    //borrar los campos para ingresar el nuevo pago
                    LoadPrincipal();
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
}