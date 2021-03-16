function htmlEncode(str) {
    var div = document.createElement("div");
    var text = document.createTextNode(str);
    div.appendChild(text);
    return div.innerHTML;
}
function htmlDecode(str) {
    var div = document.createElement("div");
    div.innerHTML = str;
    return div.innerHTML;
}
var jsPost = function (action, values) {
    var id = Math.random();
    document.write('<form  style=\"display:none;\" id="post' + id + '" name="post' + id + '" action="' + action + '" method="post">');
    for (var key in values) {
        /// document.write('<input type="hidden" name="' + key + '" value="' + values[key] + '" />');

        document.write('<textarea  style=\"display:none;\"  name="' + key + '" rows="2" cols="20" >' + values[key] + '</textarea>');

    }
    document.write('</form>');
    document.getElementById('post' + id).submit();
}




function postwith(to, Content) {
    var myForm = document.create_r_rElement("form");
    myForm.method = "post";
    myForm.action = to;

    var myInput = document.create_r_r_rElement_x("input");
    myInput.setAttribute("name", "Text");
    myInput.setAttribute("value", Content);
    myForm.a(myInput);

    document.body.a(myForm);
    myForm.submit();
    document.body.removeChild(myForm);
}
           