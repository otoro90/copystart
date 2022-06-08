var Historial = function () {
    "use strict"
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------


        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.initDataTable();
        },

        initDataTable: function () {

            //$("#example1").DataTable({
            //    "responsive": true, "lengthChange": false, "autoWidth": false,
            //    "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            //}).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#tabla-historial').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "responsive": true,
            });
        }
    }
}();