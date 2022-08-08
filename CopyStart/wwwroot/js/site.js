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
            this.initAllSelect2Render();
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

        initAllSelect2Render: function () {
            var selects = $('select.select2');
            selects.each(function () {
                var element = $(this);
                site.initSelect2(element);
            });
        },

        getPlaceHolderSelect: function (element) {
            return element.closest("div").find("label").html();
        },

        addOptionPlaceHolderInSelect: function (element) {
            var existOption = element.find("option:not([value])");
            if (!existOption.length) {
                element.prepend("<option></option>");
                element.val("");
            }
        },

        initSelect2: function (element, properties) {
            this.addOptionPlaceHolderInSelect(element);
            var textPromt = this.getPlaceHolderSelect(element);
            var placeholder = (textPromt == null || textPromt.length == 0) ? "Seleccione..." : "Seleccione " + textPromt.toLowerCase();
            element.select2({
                theme: 'bootstrap',
                placeholder: placeholder,
                allowClear: true,
                width: '100%',
                ...properties
            })
        }
    }
}();