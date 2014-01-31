

$.fn.kendoSync = function (url, options, postData, callBack, ready, useJsonp) {
    var selfy = this;
    var readyLocal = ready;
    var postDataLocal = postData;
    var urlLocal = url;
    var optionsLocal = options;
    var callBackLocal = callBack;
    var dataType = 'jsonp';
    var type = 'GET';
    var postDataFinal = null;

    if (useJsonp) {
        urlLocal = urlLocal + '?callback=?';
    } else {
        dataType = 'json';
        if (postDataLocal == undefined)
            type = 'GET';
        else
            type = 'POST';
    }

    // if we're posting, convert all JSON dates in the graph to ISO-8* so that 
    // Our lovely MVC model binder can convert our dates into our view models.
    if (postDataLocal != undefined) {
        ConvertAllDates(postDataLocal, 0);
        if (useJsonp)
            postDataFinal = JSON.parse(kendo.stringify(postDataLocal));
        else
            postDataFinal = kendo.stringify(postDataLocal);
    }
    else
        postDataFinal = kendo.stringify(postDataLocal);

    $.ajax({
        url: urlLocal,
        type: type,
        dataType: dataType,
        contentType: 'application/json; charset=utf-8',
        data: postDataFinal,//kendo.stringify(postDataLocal),
        beforeSend: function () {
            if (selfy.html() !== undefined) {
                selfy.addClass('hidden');
                selfy.parent().append("<div class='ajaxloader pad-b'><img src='/content/themes/ocw/img/ajax_loader.gif'/></div>");
            }
        },
        success: function (result) {
            //Note: We have to do this here because whenever we send back data in our VM
            // which is more than 1 level deep, we generally lose our kendo model formatting
            // which converts our JSON dates to Date objects us - this way, all +1 depth objects in the graph
            // will also get converted on their way in.
            if (result != undefined) {
                ConvertAllDates(result, 0);
            }

            if (optionsLocal != undefined) {
                optionsLocal.success(result);
            }

            if (selfy.html() !== undefined) {
                $(".ajaxloader").remove();
                selfy.removeClass('hidden');

            }

            if (callBackLocal !== undefined)
                callBackLocal(result);
        },
        error: function (result, b, c) {
            console.log('Failed in kendoSync:', result);
            if (selfy.html() !== undefined) {
                $("#ajaxloader").remove();
                selfy.parent().append("<div id='done'>Failed...</div>");
                $("#done").fadeOut(2000, function () {
                    $("#done").remove();
                    selfy.removeClass('hidden');
                });
            } else {
                alert("There is a problem with Kendo sync...");
            }
            if (optionsLocal != undefined) {
                optionsLocal.error(result);
            }
            if (callBackLocal != undefined) {
                callBackLocal(result);
            }
        },
        complete: function () {
            if (readyLocal !== undefined) readyLocal();
        }
    });

};

function ConvertAllDates(obj, depth) {
    depth++;
    if (depth < 10) {
        for (var property in obj) {
            if (obj.hasOwnProperty(property)) {
                obj[property] = parseJsonDateString(obj[property]);
                ConvertAllDates(obj[property], depth);
            }
        }
    }
    depth--;
}

parseJsonDateString = function (value) {
    var jsonDateRE = /^\/Date\((-?\d+)(\+|-)?(\d+)?\)\/$/;
    var arr = value && jsonDateRE.exec(value);
    if (arr) {
        return new Date(parseInt(arr[1]));
    }
    return value;
};