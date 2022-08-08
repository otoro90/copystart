var CrearSolicitudRapida = function () {
    "use strict"
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------

        inputsPersonaHidden: null,
        inputsActivoHidden: null,
        selectPersonaDepartamento: null,
        selectActivoDepartamento: null,
        selectPersonaUbicacion: null,
        selectActivoUbicacion: null,

        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.initEventOnClick();
            this.inputsPersonaHidden = document.querySelectorAll("#datos-persona .d-none");
            this.inputsActivoHidden = document.querySelectorAll("#datos-equipo .d-none");
            this.initSelectUbicaciones();
            this.initSelectMunicipio();
        },

        initSelectUbicaciones: function () {
            this.selectPersonaUbicacion = $("#Persona_UbicacionId");
            this.selectActivoUbicacion = $("#Activo_UbicacionId");
            this.selectPersonaDepartamento = $("#Persona_Ubicacion_CodigoDepartamento");
            this.selectActivoDepartamento = $("#Activo_Ubicacion_CodigoDepartamento");


            this.selectPersonaDepartamento.on('select2:select', function (e) {
                var data = e.params.data;
                CrearSolicitudRapida.initSelectMunicipio(CrearSolicitudRapida.selectPersonaUbicacion, data.id);
                CrearSolicitudRapida.initSelectMunicipio(CrearSolicitudRapida.selectActivoUbicacion, data.id);
            });
            this.selectActivoDepartamento.on('select2:select', function (e) {
                var data = e.params.data;
                CrearSolicitudRapida.initSelectMunicipio(CrearSolicitudRapida.selectActivoUbicacion, data.id);
            });

            this.selectPersonaDepartamento.on('select2:clear', function (e) {
                site.initSelect2(CrearSolicitudRapida.selectPersonaUbicacion.val("").trigger("change"), { disabled: "readonly" });
            });

            this.selectActivoDepartamento.on('select2:clear', function (e) {
                site.initSelect2(CrearSolicitudRapida.selectActivoUbicacion.val("").trigger("change"), { disabled: "readonly" });
            });
        },

        initSelectMunicipio: function (selectMunicipio, codigoDepartamento, properties, functionBeforeInit) {
            if (codigoDepartamento) {
                $.ajax({
                    url: '/Parametricas/Ubicaciones/GetMunicipios?codigoDepartamento=' + codigoDepartamento,
                    type: 'GET',
                    dataType: 'json',
                    success: function (response) {
                        selectMunicipio.html("<option></option>");
                        $.each(response, function (key, value) {
                            selectMunicipio.append('<option value=' + value.value + '>' + value.text + '</option>');
                        });
                        if (functionBeforeInit)
                            functionBeforeInit.call();
                    }
                });
                site.initSelect2(selectMunicipio, properties);
            }
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

            if (!tipoDocumentoId || !documento) {
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
                        $("#Persona_Nombres").val(response.nombres).prop('readonly', true);
                        $("#Persona_Apellidos").val(response.apellidos).prop('readonly', true);
                        $("#Persona_Telefono").val(response.telefono).prop('readonly', true);
                        $("#Persona_Direccion").val(response.direccion);
                        ///selects ubicacion
                        var codeDepartamento = response.ubicacionId.substring(0, 2);
                        site.initSelect2(CrearSolicitudRapida.selectPersonaDepartamento.val(codeDepartamento).trigger("change"), { disabled: "readonly" });
                        CrearSolicitudRapida.initSelectMunicipio(CrearSolicitudRapida.selectPersonaUbicacion, codeDepartamento, { disabled: "readonly" },
                            () => CrearSolicitudRapida.selectPersonaUbicacion.val(response.ubicacionId).trigger("change"));
                    }
                    else {
                        CrearSolicitudRapida.inputsPersonaHidden.forEach(x => {
                            site.initSelect2($(x).find("select").val("").trigger("change").prop('readonly', false));
                            $(x).find("input").val("").prop('readonly', false);
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
                        site.initSelect2($("#Activo_TipoActivoId").val(response.tipoActivoId).trigger("change").prop('readonly', true), { disabled: "readonly" });
                        site.initSelect2($("#Activo_MarcaActivoId").val(response.marcaActivoId).trigger("change").prop('readonly', true), { disabled: "readonly" });
                        site.initSelect2($("#Activo_ModeloActivoId").val(response.modeloActivoId).trigger("change").prop('readonly', true), { disabled: "readonly" });
                        $("#Activo_Direccion").val(response.direccion).prop('readonly', true);
                        ///selects ubicacion
                        var codeDepartamento = response.ubicacionId.substring(0, 2);
                        site.initSelect2(CrearSolicitudRapida.selectActivoDepartamento.val(codeDepartamento).trigger("change").prop('readonly', true), { disabled: "readonly" });
                        CrearSolicitudRapida.initSelectMunicipio(CrearSolicitudRapida.selectActivoUbicacion,
                            codeDepartamento,
                            { disabled: "readonly" },
                            () => CrearSolicitudRapida.selectActivoUbicacion.val(response.ubicacionId).trigger("change").prop('readonly', true));

                    }
                    else {
                        CrearSolicitudRapida.inputsActivoHidden.forEach(x => {
                            $(x).find("input,select").val("").trigger("change").prop('readonly', false);
                        });
                        ///selects ubicacion
                        CrearSolicitudRapida.selectActivoDepartamento.val(CrearSolicitudRapida.selectPersonaDepartamento.val()).trigger("change").prop('readonly', false);
                        CrearSolicitudRapida.selectActivoUbicacion.val(CrearSolicitudRapida.selectPersonaUbicacion.val()).trigger("change").prop('readonly', false);
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