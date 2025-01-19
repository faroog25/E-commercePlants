///
$(function () {
    $('a.confirm').on("click", function () {
        if (!confirm("Are you sure?")) return false;
    });

    if ($("div.alert").length) {
        setTimeout(() => {
            $("div.alert").fadeOut();
        }, 2000);
    }
});
