$("#todate").text(new Date().getFullYear());

$("#today").text(new Date().toLocaleDateString());

$("#menu").on("click", function() { $("button").toggle(); });

$("#button").click(function () {
    var name = $("#button").text();
    if (name === "Open") {
        $("#button").text("Close");
    }
    else {
        $("#button").text("Open");
    }
});
