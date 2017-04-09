function doRequest()
{
    var List;

    $.ajax({
        type: "POST",
        url: "/Graber/GetList",
        success: function (result) { },
        complete: function (result)
        {
            DoTestAppend("before DoListGrab");
            DoListGrab(result);
            DoTestAppend("after DoListGrab");
        }
    });
}

function DoListGrab(result) {
    DoTestAppend("in DoListGrab");
    try
    {
        $.each(result.responseJSON, function () {
            DoTestAppend("before doGrubItem " + this);
            doGrubItem(this);
            DoTestAppend("after doGrubItem " + this);
        });
    }
    catch( exp)
    {
        DoTestAppend("error in DoListGrab " + exp );
    }

    DoTestAppend("after DoListGrab");
}

function doGrubItem(Id) {
    DoTestAppend("before ajax item" + Id);
    $.ajax({
        type: "POST",
        url: "/Graber/DoGrub",
        data: "Id=" + Id,
        error: function (error) { $('#ListTable').append("<tr><td>error</td><td>" + error + "</td></tr>"); },
        complete: function (result) { DoAppendLine(result.responseJSON); }
    });
    DoTestAppend("after ajax item" + Id);
}

function DoAppendLine(result)
{
    $('#ListTable').append("<tr><td>" + result.Status + "</td><td>" + result.Name + "</td></tr>");
}

function DoTestAppend(result) {
    $('#ListTable').append("<tr><td>TestAppend</td><td>" + result + "</td></tr>");
}



