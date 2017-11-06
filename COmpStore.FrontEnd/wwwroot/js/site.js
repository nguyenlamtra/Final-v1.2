// Write your Javascript code.
$(document).ready(function () {

    $('.animsition').animsition({
        inClass: 'fade-in-down-sm',
        outClass: 'fade-out-down-sm',
        inDuration: 1000,
        outDuration: 800,
        linkElement: '.animsition-link',
        // e.g. linkElement: 'a:not([target="_blank"]):not([href^="#"])'
        loading: true,
        loadingParentElement: 'body', //animsition wrapper element
        loadingClass: 'animsition-loading',
        loadingInner: '', // e.g '<img src="loading.svg" />'
        timeout: false,
        timeoutCountdown: 5000,
        onLoadEvent: true,
        browser: ['animation-duration', '-webkit-animation-duration'],
        // "browser" option allows you to disable the "animsition" in case the css property in the array is not supported by your browser.
        // The default setting is to disable the "animsition" in a browser that does not support "animation-duration".
        overlay: false,
        overlayClass: 'animsition-overlay-slide',
        overlayParentElement: 'body',
        transition: function (url) { window.location.href = url; }
    });

    // Banner slider
    $('.bxslider').bxSlider();

    // fix menu when passed
    $('.masthead')
        .visibility({
            once: false,
            onBottomPassed: function () {
                $('.fixed.menu').transition('fade in');
            },
            onBottomPassedReverse: function () {
                $('.fixed.menu').transition('fade out');
            }
        });

    // create sidebar and attach to menu open
    $('.ui.sidebar')
        .sidebar('attach events', '.toc.item')
        ;

    $('.ui.labeled.dropdown.icon.item').dropdown({
        on: 'hover',
    });

    $('.ui.pointing.dropdown.link.item').dropdown({

        on: 'click',
        onChange: function (value, text, $choice) {

            $choice.siblings().find('i').remove();
            $choice.children().before('<i class="checkmark icon"></i>');
        }
    });

    $('.ui.rating')
        .rating('disable');

    $('.ui.checkbox').checkbox();

    $('.producer')
        .popup({
            popup: $('.popup'),
            inline: true,
            hoverable: true,
            position: 'bottom left',
            delay: {
                show: 100,
                hide: 300
            }
        });

    $('.ui.dropdown')
        .dropdown();

    $('.ui.accordion')
        .accordion({exclusive:false});

    $('.ui.card .image').dimmer({
        on: 'hover'
    });

    $('#gallery .image').click(function () {
        $(this).transition({
            animation: 'pulse',

            interval: 200
        })
            ;
    })

    $('.ui.rating')
        .rating('disable')
        ;
});
