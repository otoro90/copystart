var site = function () {
    "use strict"
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------


        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.onModalClick();
            this.initSelect2();
        },

        onModalClick: function () {

            $('button[data-toggle="ajax-modal"]').click(function (event) {
                var url = $(this).data('url');
                var PlaceHolderElement = $('#PlaceHolderHere')
                $.get(url).done(function (data) {
                    PlaceHolderElement.html(data);
                    PlaceHolderElement.find('.modal').modal('show');
                })
            })
        },

        initSelect2: function () {
            $('.select2').select2({
                theme: 'bootstrap',
                placeholder: "Seleccione...",
                allowClear: true
            })
        }
    }
}();