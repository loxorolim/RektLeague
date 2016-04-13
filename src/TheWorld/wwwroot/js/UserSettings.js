$("#my-file-selector").change(function () {
    $("#input-filename").val(this.files[0].name);
});