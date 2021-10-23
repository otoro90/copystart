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
        }
    }
}();