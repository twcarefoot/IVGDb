// Carousel Auto-Cycle
jQuery(document).ready(function($) {
    $('div.control-box a.middle').hover(function () {
        setHref();
    });
    $('.carousel-control.left, .carousel-control.right').click(function () {
        setHrefNext();
    });
    $('.carousel').carousel({
        interval: 6000
    }); 
});

function setHref() {
    var url = $('div.active div.bannerImage a').attr('href');
    $('div.control-box a.middle').attr('href', url);
}

function setHrefNext() {
    var url = $('div.active div.bannerImage a').attr('href').next();
    $('div.control-box a.middle').attr('href', url);
}