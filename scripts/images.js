/// <reference path="jquery-2.1.3.intellisense.js" />
/// <reference path="jquery-2.1.3.js" />


$(document).ready(function () {

    console.log("images ready");

    getImages();
});

var getImages = function () {

    $.ajax({
        dataType: "json",
        url: 'api/images',
        type: 'GET'
    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            writeResult("fail " + errorThrown);
        })
        .done(function (data, textStatus, jqXHR) {
            drawCarousel(data);
        }
    );

};

var drawImage = function (imageUrl)
{
    $("#image").attr("src", imageUrl);
}

function drawCarousel(arrayOfURIs) {
    var indicator = $("ol.carousel-indicators");
    var inner = $("div.carousel-inner");

    //add a li for each image.
    indicator.append("<li data-target='#myCarousel' data-slide-to='0' class='active'></li>");

    var url = arrayOfURIs[0];
    var altText = "Ballyglass Irish Thatched Cottage " + url.substr(("Images").length + 1);
    //add each image to the inner
    inner.append("<div class='item active'><img src='" + url + "' class='img-responsive' alt='" + altText + "' ></div>");

    //Delay loading of individual images.
    var index = 1;
    var interval = setInterval(function () {
        url = window.location.origin + "//" + arrayOfURIs[index];
        drawImageInCarousel(indicator, inner, index, url);
        if (index >= arrayOfURIs.length-1)
        {
            clearInterval(interval);
        }
        index++;
    }, 2000);
    
}

function drawImageInCarousel(indicator, inner, index, url)
{
    var altText = "Ballyglass Irish Thatched Cottage " + url.substr(("Images").length + 1);
    indicator.append("<li data-target='#myCarousel' data-slide-to='" + index + "'></li>");
    inner.append("<div class='item'><img src='" + url + "' class='img-responsive' alt='" + altText + "' ></div>");
}