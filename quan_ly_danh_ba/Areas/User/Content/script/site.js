$(function () {
    $('.temp-alert').fadeOut(3000);
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
    $('.closeButton').on('click', function () {
        $(".visible-img").addClass('d-none');
        $('#avatar').val("").show();
    })
    $('#avatar').on('change', function (event) {
        const file = event.target.files[0];
        if (file) {

            const reader = new FileReader();
            reader.onload = function (e) {
                const base64String = e.target.result;
                console.log(base64String);

                $('#image').attr('src', base64String);
                $(".visible-img").removeClass('d-none');
                $('.closeButton').removeClass('d-none');
                $('#avatar').hide();
            }
            reader.readAsDataURL(file);
        }
    });
});
