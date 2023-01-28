window.Alert = function (message) {
    alert(message);
}

function test(message) {
    message = "abc" + message;
    alert(message);
}

//ToastPrompt
function showToast20200509(msg) {
    if (msg.length > 0) {
        var x = document.getElementById("snackbar20200509");
        x.innerHTML = msg;
        x.className = "show11";
        setTimeout(function () { x.className = "snackbar11"; }, 2000);
    }


};

//Set Focus input

function BlazorFocusElement(element) {
    // alert(14);
    // if (element instanceof HTMLElement) {

    //    alert(13);
    element.focus();
    //   alert(12);
    //  }
}
function movenextelement(event, me) {
    switch (event.key) {
        case "ArrowLeft":
            // Left pressed
            break;
        case "ArrowRight":
            // Right pressed
            break;
        case "ArrowUp":

            let adb = me.previousElementSibling;

            adb.focus();
            break;
        case "ArrowDown":
            // Down pressed
            let aaa = me.nextElementSibling;
            aaa.focus();
            break;
        case "Escape":
            // Escape pressed
            break;

    }
    //alert(event.key);
    return true;
}
function elementfocus(mepre, menext, meescape,escapreclick) {


    switch (event.key) {
        case "ArrowUp":
            if (mepre) {
                mepre.focus();

            }

            break;
        case "ArrowDown":
            // Down pressed
            if (menext) {
                menext.focus();

            }


            break;
        case "Escape":
            // Escape pressed
            if (meescape) {
                meescape.focus();
                if (escapreclick) {
                    meescape.click();

                }

            }
            break;

    }
}

function setdisplayvalue(objid,value) {

    
    var x = document.getElementById(objid);
    x.style.display = value;

}





