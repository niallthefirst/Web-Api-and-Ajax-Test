/// <reference path="jquery-2.1.3.js" />
/// <reference path="jquery-2.1.3.intellisense.js" />

$(document).ready(function () {
    writeResult("ready");
    
    getTestimonials();

    $("#Add").click(function () {
        var data = {
            "Name": "name",
            "Comment": "comment",
            "Date": "2014"
        };

        addTestimonial(data);
    });

});

var writeResult = function (message)
{
     $("#result").text(message);
}

var addTestimonial = function (dataJSON) {

    $.ajax({
        dataType: "json",
        url: 'api/testimonials',
        type: 'POST',
        data: dataJSON
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            writeResult("fail " + errorThrown);
        })
        .done(function (data, textStatus, jqXHR) {
            writeResult(data);

        });


};

var getTestimonials = function () {

    $.ajax({
        dataType: "json",
        url: 'api/testimonials',
        type: 'GET'
    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            writeResult("fail " + errorThrown);
        })
        .done(function (data, textStatus, jqXHR) {
            //var jsonData = JSON.stringify(data);
            var message;
            $.each(data, function (index, value) {
                message += " | " + JSON.stringify(value);
            });
            writeResult(message);
        }
    );

};

var getTestimonialsFromJSONFile = function () {

    

    $.ajax("/models/datajson.js",
            { dataType: "json" })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.log("fail " + errorThrown);
        })
        .done(function (data, textStatus, jqXHR) {
            console.log("done");
            //var jsonData = JSON.stringify(data);

            $.each(data.testimonials, function (index, value) {
                console.log(value.name);
            });
        }
    );    
};

var getTestimonialsFromJSFile = function () {

    $.ajax("/models/datajs.js",
            { dataType: "script" })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.log("fail " + errorThrown);
        })
        .done(function (data, textStatus, jqXHR) {
            console.log("done");
            //var jsonData = JSON.stringify(data);


            var myarray = [];
            var myJSONString = "";
            var myJSONObject;
            for (var i = 0; i < testimonialsArray.length; i++) {
                var item = {
                    "name": testimonialsArray[i][1],
                    "comment": testimonialsArray[i][3],
                    "date": testimonialsArray[i][0]
                };

                myarray.push(item);
            }

            myJSONString = JSON.stringify({ testimonials: myarray });
            myJSONObject = JSON.parse(myJSONString);

            $.each(myJSONObject.testimonials, function (index, value) {
                console.log(value.name);
            });
        }
    );

};