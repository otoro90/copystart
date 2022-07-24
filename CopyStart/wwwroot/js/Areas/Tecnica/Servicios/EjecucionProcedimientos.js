var EjecucionProcedimientos = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------

        table: null,

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
                    { data: 'ProcedimientoTipoServicio.Numero'  },
                    { data: 'ProcedimientoTipoServicio.Procedimientos.Nombre', width: "15%" },
                    { data: 'ProcedimientoTipoServicio.Procedimientos.Descripcion', width: "20%"},
                    { data: 'ProcedimientoTipoServicio.Procedimientos.TiempoEjecucion', width: "10%" },
                    { data: 'ProcedimientosRealizadosCheck', width: "10%" },
                    { data: 'observaciones'},
                ],
            });
        },

        //Función que agrega el evento on change en los inputs checkbox RealizaProcedimiento
        initEventCheckBoxRealizaProcedimiento: function () {
            var inputs = $("#table-proser td input[type='checkbox']");
            inputs.each(function () {
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
        }
    }
}();