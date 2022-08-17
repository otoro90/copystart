var Edit = function () {
    "use strict"
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------


        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.initSelectDepartamento();
        },

        initSelectDepartamento: function () {
            var that = this;
            var selectDepartamento = $("#Ubicacion_CodigoDepartamento");
            $.ajax({
                url: '/Parametricas/Ubicaciones/GetDepartamentos',
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    $.each(response, function (key, value) {
                        selectDepartamento.append('<option value=' + value.value + '>' + value.text + '</option>');
                    });
                    that.setDataSelectUbicacionDepartamento();
                }
            });
            site.initSelect2(selectDepartamento);

            var selectMunicipio = $("select#UbicacionId");

            selectDepartamento.on('select2:select', function (e) {
                var data = e.params.data;
                that.configSelectMunicipio(selectMunicipio, data.id, { disabled: false });
            });

            selectDepartamento.on('select2:clear', function (e) {
                site.initSelect2(selectMunicipio.val("").trigger("change"), { disabled: "readonly" });
            });

            selectDepartamento.trigger({
                type: 'select2:select',
                params: {
                    data: { id: $("input#UbicacionId").val().substring(0, 2) }
                }
            });
        },

        initSelectMunicipio: function () {
            var selectMunicipio = $("select#UbicacionId");
            site.initSelect2(selectMunicipio, { disabled: "readonly" });
        },

        configSelectMunicipio: function (selectMunicipio, codigoDepartamento, properties) {

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
                        Edit.setDataSelectUbicacion()
                    }
                });
                site.initSelect2(selectMunicipio, properties);
            }
        },

        setDataSelectUbicacion: function () {

            var codeMunicipio = $("input#UbicacionId").val();
            $("select#UbicacionId").val(codeMunicipio).trigger('change');
        },

        setDataSelectUbicacionDepartamento: function () {
            var codeDepartamento = $("input#UbicacionId").val().substring(0, 2);
            $("select#Ubicacion_CodigoDepartamento").val(codeDepartamento).trigger('change');
        }
    }
}();