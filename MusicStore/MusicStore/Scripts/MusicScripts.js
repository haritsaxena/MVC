$(function () {
    $("#autocomplete").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("data-autocomplete-source") });
    });
});

