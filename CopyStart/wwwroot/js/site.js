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
            var selects = $('.select2');
            selects.each(function () {
                var element = $(this);
                var existOption = element.find("option:not([value])");
                if (!existOption.length) {
                    element.prepend("<option></option>");
                    element.val("");
                }
                var textPromt = element.closest("div").find("label").html();
                var placeholder = (textPromt == null || textPromt.length == 0) ? "Seleccione..." : "Seleccione " + element.closest("div").find("label").html().toLowerCase();
                element.select2({
                    theme: 'bootstrap',
                    placeholder: placeholder,
                    allowClear: true,
                    width: '100%'
                })
            });
        }
    }
}();