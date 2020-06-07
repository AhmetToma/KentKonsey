new WOW().init();

var subSliderElem = document.querySelector('#sub-slider'),
    mainSliderElem = document.querySelector('.slider'),
    subSliderOptions = {
        fullWidth: true,
        indicators: true,
        transition: 500,
        interval: 6000
    },
    mainSliderOptions = {
        indicators: false,
        height: 650,
        transition: 500,
        interval: 6000
    },
    ModalElem = $('#news'),
    subSliderInstance = M.Carousel.init(subSliderElem, subSliderOptions),
    mainSliderInstance = M.Slider.init(mainSliderElem, mainSliderOptions),
    ModalIns = M.Modal.init(ModalElem, {})[0];
$(document).ready(function () {
    // Custom JS & jQuery here
    $('.tooltipped').tooltip();

    // INIT SLIDER
    $('#main-slider-prev').click(function () {
        mainSliderInstance.prev();
    });
    $('#main-slider-next').click(function () {
        mainSliderInstance.next();
    });

    $('#sub-slider-prev').click(function () {
        subSliderInstance.prev();
    });
    $('#sub-slider-next').click(function () {
        subSliderInstance.next();
    });

    // INIT MODAL
    $('.modal').modal();

    $(".modal-close").on('click', function () {
        ModalIns.close();
    });
});




function Habergetir(haberid) {
    var url = "/KentKonsey/GetHaberById?haberid=" + haberid;

    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            ////$("#haberId").val(obj.id);
            $("#metin").html(obj.aciklama);
            $("#img").attr("src", obj.FotoUrl);
          
         
           
             ModalIns.open();



        }
    })
}


