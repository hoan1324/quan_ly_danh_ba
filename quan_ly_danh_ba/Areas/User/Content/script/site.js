$(".bars-icon").click(function () {
    if ($(".category").hasClass("click-menu-bar")) {
        $(".left-navbar").removeClass("left-navbar-click")
        $(".category").removeClass("click-menu-bar");
    } else {
        $(".left-navbar").addClass("left-navbar-click")
        $(".category").addClass("click-menu-bar");
    }
});
