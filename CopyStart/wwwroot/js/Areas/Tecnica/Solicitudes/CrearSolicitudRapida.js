var CrearSolicitudRapida = function () {
    "use strict"
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------

        inputsPersonaHidden: null,
        inputsActivoHidden: null,

        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.initEventOnClick();
            this.inputsPersonaHidden = document.querySelectorAll("#datos-persona .d-none");
            this.inputsActivoHidden = document.querySelectorAll("#datos-equipo .d-none");
        },

        initEventOnClick: function () {

            var buttonPersona = document.getElementById("buscar-persona");
            buttonPersona.addEventListener("click", () => this.buscarPersona());
            var buttonEquipo = document.getElementById("buscar-equipo");
            buttonEquipo.addEventListener("click", () => this.buscarEquipo());
        },

        buscarPersona: function () {
            var tipoDocumentoId = document.getElementById("Persona_TipoDocumentoId").value;
            var documento = document.getElementById("Persona_NumeroDocumento").value;

            if (!tipoDocumentoId && !documento) {
                alert('Tipo de documento y documento son campos obligatorios');
                return;
            }

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: `/Administracion/Personas/GetPersonaByDocument?tipoDocumentoId=${tipoDocumentoId}&documento=${documento}`,
                success: function (response) {
                    if (response) {
                        $("#Persona_Nombres").val(response.nombres).prop('disabled', true);
                        $("#Persona_Apellidos").val(response.apellidos).prop('disabled', true);
                        $("#Persona_Telefono").val(response.telefono).prop('disabled', true);
                        $("#Persona_UbicacionId").val(response.ubicacionId).trigger("change").prop('disabled', true);
                    }
                    else {
                        CrearSolicitudRapida.inputsPersonaHidden.forEach(x => {
                            $(x).find("input,select").val("").trigger("change").prop('disabled', false);
                        });
                        alert('Persona no registrada, ingrese sus datos');
                    }
                    CrearSolicitudRapida.inputsPersonaHidden.forEach(x => {
                        x.classList.remove("d-none")
                    });
                },
                error: function () {
                    alert('Error');
                }
            });
        },

        buscarEquipo: function () {
            var serial = document.getElementById("Activo_Serial").value;

            if (!serial) {
                alert('El serial es un campo obligatorio');
                return;
            }

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: `/Activos/Activos/GetActivoBySerial?serial=${serial}`,
                success: function (response) {
                    if (response) {
                        $("#Activo_TipoActivoId").val(response.tipoActivoId).trigger("change").prop('disabled', true);
                        $("#Activo_MarcaActivoId").val(response.marcaActivoId).trigger("change").prop('disabled', true);
                        $("#Activo_ModeloActivoId").val(response.modeloActivoId).trigger("change").prop('disabled', true);
                        $("#Activo_UbicacionId").val(response.ubicacionId).trigger("change").prop('disabled', true);
                        $("#Activo_Direccion").val(response.direccion).prop('disabled', true);
                    }
                    else {
                        CrearSolicitudRapida.inputsActivoHidden.forEach(x => {
                            $(x).find("input,select").val("").trigger("change").prop('disabled', false);
                        });
                        alert('Equipo no registrado, ingrese sus datos');
                    }
                    CrearSolicitudRapida.inputsActivoHidden.forEach(x => {
                        x.classList.remove("d-none")
                    });
                },
                error: function () {
                    alert('Error');
                }
            });
        }
    }
}();