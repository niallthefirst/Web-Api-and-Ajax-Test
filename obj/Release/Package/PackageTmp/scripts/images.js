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
            //var jsonData = JSON.stringify(data);
            var url;
            $.each(data, function (index, value) {
                url = JSON.stringify(value);
            });

            drawImage(url);
        }
    );

};

var drawImage = function (imageUrl)
{
    $("#image").attr("src", imageUrl);
}