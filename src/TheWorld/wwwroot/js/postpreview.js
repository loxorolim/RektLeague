(function () {

    $("#postTitle").on("input", function () {
        var postTitle = $("#postTitle").val();
        $("#previewTitle").text(postTitle);
        $("#previewTitle").fitText(1.2);
    });
    $("#postText").on("input",function () {
        var postTitle = $("#postText").val();
        $("#previewText").text(postTitle);
    });
    $("#postURL").on("input",function () {
        var src = $("#postURL").val();
        $("#previewURL").attr("src", src);
        var teste = $("#previewURL").attr("src");
    });

})();