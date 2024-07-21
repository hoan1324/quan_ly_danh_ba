$(".bars-icon").on('click',function () {
    if ($(window.width() > 768)) {
        if ($(".category").hasClass("click-menu-bar")) {
            $(".left-navbar").removeClass("left-navbar-click")
            $(".category").removeClass("click-menu-bar");
        } else {
            $(".left-navbar").addClass("left-navbar-click")
            $(".category").addClass("click-menu-bar");
        }
    }
    else {
        if ($(".left-navbar").hasClass("hidden-left-navbar")) {
            $(".left-navbar").removeClass("hidden-left-navbar")
        }
    }
});
$(document).on('click', function () {
    if (!$(".left-navbar").hasClass("hidden-left-navbar")) {
        $(".left-navbar").addClass("hidden-left-navbar")

    }
})

