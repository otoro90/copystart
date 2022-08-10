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
            this.initSelectMunicipio();
        },

        initSelectDepartamento: function () {
            var selectDepartamento = $("#Ubicacion_CodigoDepartamento");
            $.ajax({
                url: '/Parametricas/Ubicaciones/GetDepartamentos',
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    $.each(response, function (key, value) {
                        selectDepartamento.append('<option value=' + value.value + '>' + value.text + '</option>');
                    });
                }
            });
            site.initSelect2(selectDepartamento);

            var selectMunicipio = $("#UbicacionId");

            selectDepartamento.on('select2:select', function (e) {
                var data = e.params.data;
                CompleteData.configSelectMunicipio(selectMunicipio, data.id, { disabled: false });
            });

            selectDepartamento.on('select2:clear', function (e) {
                site.initSelect2(selectMunicipio.val("").trigger("change"), { disabled: "readonly" });
            });
        },

        initSelectMunicipio: function () {
            var selectMunicipio = $("#UbicacionId");
            site.initSelect2(selectMunicipio, { disabled: "readonly" });
        },

        configSelectMunicipio: function (selectMunicipio, codigoDepartamento, properties, functionBeforeInit) {
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
    }
}();