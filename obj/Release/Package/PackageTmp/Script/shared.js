function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}
function toDate(dateStr) {
    var parts = dateStr.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}
function ConvertDate(date) {
    var date = new Date(parseInt(date.substr(6)));
    var month = date.getMonth() + 1;
    return month + "/" + date.getDate() + "/" + date.getFullYear();
}
function isNumberKey(txt, evt) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (!(charCode == 8                       // backspace
        || charCode == 9                          // Tab
        || (charCode >= 35 && charCode <= 40)     // arrow keys/home/end
        || (charCode >= 48 && charCode <= 57)))     // numbers on keyboar))
            return false;
    }
    return true;
}
function AllowNumbers(event)
{
    if (!(event.keyCode == 8                                // backspace
        || event.keyCode == 9                               // Tab
        || event.keyCode == 46                              // delete
        || (event.keyCode >= 35 && event.keyCode <= 40)     // arrow keys/home/end
        || (event.keyCode >= 48 && event.keyCode <= 57)     // numbers on keyboard
        || (event.keyCode >= 96 && event.keyCode <= 105)))   // number on keypad
    {
        event.preventDefault();
    }
}
function addCommas(Num) { //function to add commas to textboxes
    Num += '';
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    x = Num.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    return x1 + x2;
}
function ceilNumber(rnum, rlength) {
    var newnumber = Math.ceil(rnum * Math.pow(10, rlength)) / Math.pow(10, rlength);
    return newnumber;
}

