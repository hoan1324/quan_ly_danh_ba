$(".left-navbar a.link-light").on("click", function (e) {
    
    // Loại bỏ class 'active-title' khỏi tất cả các liên kết
    $(".left-navbar a.link-light").removeClass("active-title");
    

    // Lấy liên kết từ thuộc tính data-link và chuyển sang trang đích
    var link = $(this).data("link");
    if (link) {
        window.location.href = link;
        $(this).addClass("active-title");
    }
});
$(".bars-icon").on('click', function () {
    if ($(window).width() > 992) {
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
if ($(window).width() <= 992) {
    $(".left-navbar").removeClass("left-navbar-click")
    $(".category").removeClass("click-menu-bar");
}
$(document).on('click', function () {
    if ($(window).width() <= 992) {
        if (!$(".left-navbar").hasClass("hidden-left-navbar")) {
            $(".left-navbar").addClass("hidden-left-navbar")

        }
    }
});
$(".PagedList-skipToNext").text("Next").on("click", function () {
    var numberActive = $(".pagination-container ul.pagination li.active a").text();
    window.location.href = `/User/Contact?page=${parseInt(numberActive)+1}`
});
$(".PagedList-skipToPrevious").text("Prev").on("click", function () {
    var numberActive = $(".pagination-container ul.pagination li.active a").text();
    window.location.href = `/User/Contact?page=${parseInt(numberActive) - 1}`
});


