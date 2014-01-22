/* ---------------------------------------------------------------------- */
/*	On Page Load
/* ---------------------------------------------------------------------- */
function Page_Load() {

    $("#features").equalHeights();

    /* ---------------------------------------------------------------------- */
    /*	Ini oneByOne Banner
    /* ---------------------------------------------------------------------- */

    $("#banner").oneByOne({
        // the wrapper's name	
        className: 'oneByOne1',	             								// the wrapper div's class name of each slider	
        /* Please provide the width and height in the responsive 
        version, for the slider will keep the ratio when resize 
        depends on these size. */
        pauseByHover: true,													// pause the auto delay slideshow when user hover		
        width: 940,															// width of the slider		
        height: 341,														// height of the slider		
        easeType: 'random',													// will override effect if you don't pre-defined it in the element
        delay: 300,  														// the delay of the touch/drag tween	
        tolerance: 0.25, 													// the tolerance of the touch/drag
        slideShow: true,													// auto play the slider or not		
        slideShowDelay: 3000,												// the delay millisecond of the slidershow	
        responsive: true,													// slider's size with the media query in CSS
        minWidth: 480														// text is hidden at this width
    });

    $('#features-horisontal .item:first').addClass('active');

    if ($('#features-horisontal .item').length > 1) {
        $('#features-horisontal').carousel({
            interval: 5000
        });
    }


    $('#features-vertical .item:first').addClass('active');
    
    if ($('#features-vertical .item').length > 1) {
        $('#features-vertical').carousel({
            interval: 5000
        });
    }
    
    Cufon.replace("h1");
    Cufon.replace("h2");
    Cufon.replace("h3");
    Cufon.replace("h4");
    Cufon.replace(".breadcrumb li");
    Cufon.replace(".breadcrumb li a");

    $('.file-upload').find('input').addClass('btn');
    $('.wysihtml5').wysihtml5({
        "font-styles": true, //Font styling, e.g. h1, h2, etc. Default true
        "emphasis": true, //Italics, bold, etc. Default true
        "lists": true, //(Un)ordered lists, e.g. Bullets, Numbers. Default true
        "html": false, //Button which allows you to edit the generated HTML. Default false
        "link": true, //Button to insert a link. Default true
        "image": true, //Button to insert an image. Default true,
        "color": false //Button to change color of font  );
    });

    $(window).resize(function () {
        $("#features").equalHeights();
    });
}

$(document).ready(function () {
    Page_Load();
});




