$(function () {
    $(".left-navbar a.link-light").on("click", function (e) {
        e.preventDefault();
        $(".left-navbar a.link-light").removeClass("active-title");
        var link = $(this).data("link");
        if (link) {
            window.location.href = link;
            $(this).addClass("active-title");
        }
    });

    $(".bars-icon").on('click', function () {
        if ($(window).width() > 992) {
            $(".left-navbar").toggleClass("left-navbar-click");
            $(".category").toggleClass("click-menu-bar");
        } else {
            $(".left-navbar").toggleClass("hidden-left-navbar");
        }
    });

    function adjustNavbar() {
        if ($(window).width() <= 992) {
            $(".left-navbar").removeClass("left-navbar-click").addClass("hidden-left-navbar");
            $(".category").removeClass("click-menu-bar");
        } else {
            $(".left-navbar").removeClass("hidden-left-navbar");
        }
    }
    $(window).on('resize', adjustNavbar);
    adjustNavbar();

    $(document).on('click', function (e) {
        if ($(window).width() <= 992 && !$(e.target).closest('.left-navbar, .bars-icon').length) {
            $(".left-navbar").addClass("hidden-left-navbar");
        }
    });
    $(".icon-close").on('click', function () {

        $(".left-navbar").addClass("hidden-left-navbar");

    });

    $(".PagedList-skipToNext").text("Next").on("click", function () {
        var numberActive = parseInt($(".pagination-container ul.pagination li.active a").text());
        window.location.href = `/User/Contact?page=${numberActive + 1}`;
    });

    $(".PagedList-skipToPrevious").text("Prev").on("click", function () {
        var numberActive = parseInt($(".pagination-container ul.pagination li.active a").text());
        window.location.href = `/User/Contact?page=${numberActive - 1}`;
    });
});
