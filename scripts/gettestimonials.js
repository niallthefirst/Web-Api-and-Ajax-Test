/// <reference path="jquery-2.1.3.js" />
/// <reference path="jquery-2.1.3.intellisense.js" />

$(document).ready(function () {
    console.log("ready");

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


    $.ajax("/models/dataraw.js",
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
});