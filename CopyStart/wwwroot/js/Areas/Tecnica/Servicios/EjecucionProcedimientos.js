var EjecucionProcedimientos = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------

        table: null,
        inputs: null,
        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.initDataTable();
            this.initEventCheckBoxRealizaProcedimiento();
        },

        //Función que inicializa la tabla de procedimientos realizados
        initDataTable: function () {

            this.table = $('#table-proser').DataTable({
                "lengthChange": false,
                "paging": false,
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false,
                "responsive": true,
                columns: [
                    { data: 'Id' },
                    { data: 'ServicioId' },
                    { data: 'ProcedimientoTipoServicioId' },
                    { data: 'ProcedimientosRealizados' },
                    { data: 'ProcedimientoTipoServicio.Numero', width: "6%" },
                    { data: 'ProcedimientoTipoServicio.Procedimientos.Nombre', width: "20%" },
                    { data: 'ProcedimientoTipoServicio.Procedimientos.Descripcion', width: "24%"},
                    { data: 'ProcedimientoTipoServicio.Procedimientos.TiempoEjecucion', width: "8%" },
                    { data: 'ProcedimientosRealizadosCheck', width: "10%" },
                    { data: 'observaciones'},
                ],
            });
        },

        //Función que agrega el evento on change en los inputs checkbox RealizaProcedimiento
        initEventCheckBoxRealizaProcedimiento: function () {
            this.inputs = $("#table-proser td input[type='checkbox']");
            this.inputs.each(function () {
                var that = $(this);
                that.change(function () {
                    EjecucionProcedimientos.onChangeCheckRealizaProcedimiento(that);
                });
            });
        },

        //funcion que cambia el estado de realización del procedimiento
        onChangeCheckRealizaProcedimiento: function (element) {
            var isCheck = element.is(":checked");
            element.closest("tr").find("#procedimientosRealizados").html(isCheck ? isCheck : "False");
        },

        registrarProcedimientosRealizados: function () {
            debugger;
            var procedimientos = new Array();
            var table = $("#table-proser").DataTable();
            var data = table.rows().data().toArray();
            data.forEach((x, i) => {
                debugger;
                var procedimiento = {
                    Id: x.Id,
                    ServicioId: x.ServicioId,
                    ProcedimientoTipoServicioId: x.ProcedimientoTipoServicioId,
                    ProcedimientosRealizados: $(EjecucionProcedimientos.inputs[i]).is(":checked"),
                    Observaciones: x.Observaciones
                }
                procedimientos.push(procedimiento);
            })
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: "/Tecnica/Servicios/EjecucionProcedimientos",
                data: JSON.stringify(procedimientos),
                success: function (response) {
                    debugger;
                    alert(response);
                },
                error: function () {
                    alert('failure');
                }
            });
        }
    }
}();