(function () {
    var $sidebar_and_wrapper = $("#sidebar,#wrapper");
    var $icon = $("#toggle_sidebar i.fa");
    $("#toggle_sidebar").on("click", function () {
        $sidebar_and_wrapper.toggleClass("hide-sidebar");
        if ($sidebar_and_wrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        }
    });

})();