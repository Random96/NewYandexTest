function doRequest()
{
    var List;

    $.ajax({
        type: "POST",
        url: "/Graber/GetList",
        success: function (result) { },
        complete: function (result)
        {
            DoListGrab(result);
        }
    });
}

function DoListGrab(result) {
    $.each(result.responseJSON, function () {
        doGrubItem(this);
    });
}


function doGrubItem(Id) {
    $.ajax({
        type: "POST",
        url: "/Graber/DoGrub",
        data: "Id=" + Id,
        error: function (error) { $('#ListTable').append("<tr><td>error</td><td>" + error + "</td></tr>"); },
        complete: function (result) { DoAppendLine(result.responseJSON); }
    });
}

function DoAppendLine(result)
{
    var imgTag = "";

    if (result.Status === 1)
        imgTag = "<img src=\"/img/o.png\" />Ok</img>";
    else
        imgTag = "<img src=\"/img/n.png\" />Fail</img>";

    $('#ListTable').append("<tr><td>" + imgTag + "</td><td>" + result.Name + "</td></tr>");
}



