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
                debugger;
                var url = $(this).data('url');
                var PlaceHolderElement = $('#PlaceHolderHere')
                $.get(url).done(function (data) {
                    debugger;
                    PlaceHolderElement.html(data);
                    PlaceHolderElement.find('.modal').modal('show');
                })
            })
        }
    }
}();