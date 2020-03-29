function validate(form) {
    var valid = true;

    if (!checkStringAndFocus(form.elements["f_email"], "Podaj prawidłowy e-mail!", isEmailInvalid)) valid = false;
    if (!checkStringAndFocus(form.elements["f_imie"], "Podaj imię!", isStringInvalid)) valid = false;
    if (!checkStringAndFocus(form.elements["f_nazwisko"], "Podaj nazwisko", isStringInvalid)) valid = false;
    if (!checkStringAndFocus(form.elements["f_kod"], "Podaj kod pocztowy!", isStringInvalid)) valid = false;
    if (!checkStringAndFocus(form.elements["f_miasto"], "Podaj miasto!", isStringInvalid)) valid = false;
    
    return valid;
}



function alterRows(i, e) {
    if (e) {
        if (i % 2 == 1) {
            e.setAttribute("style", "background-color: Aqua;");
        }
        e = e.nextSibling;
        while (e && e.nodeType != 1) {
            e = e.nextSibling;
        }
        alterRows(++i, e)
    }
}

function showElement(e) {
    document.getElementById(e).style.visibility = 'visible';
}

function hideElement(e) { 
    document.getElementById(e).style.visibility = 'hidden';
}

function checkStringAndFocus(obj, msg, func) {
    let str = obj.value
    let errorFieldName = "e_" + obj.name.substr(2, obj.name.length)
    if (func(str)) {
        document.getElementById(errorFieldName).innerHTML = msg;
        obj.focus();
        return false
    }
    document.getElementById(errorFieldName).innerHTML = "";
    return true;
}


function isEmailInvalid(str) {
    let email = /^[a-zA-Z_0-9\.]+@[a-zA-Z_0-9\.]+\.[a-zA-Z][a-zA-Z]+$/;
    return !email.test(str)
}

function isStringInvalid(text) {
    return isWhiteSpaceOrEmpty(text)
}

function isWhiteSpaceOrEmpty(str) {
    return /^[\s\t\r\n]*$/.test(str);
}

