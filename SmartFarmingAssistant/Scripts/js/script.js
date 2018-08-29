
function insertAtCareto(el, dele, value) {
	
	if (el.selectionStart || el.selectionStart == "0") {
		var start = el.selectionStart;
		var end = el.selectionEnd;
		var val_start = el.value.substring(0, start-dele);
		var val_end = el.value.substring(end, el.value.length);
		el.value = val_start + value + val_end;
		return (val_start+value).length;
		
	} else {
		el.value += value;
		return el.value.length;
	}
}

function setCaretPositiono(ctrl, pos) {
	
	if (ctrl.setSelectionRange) {
		ctrl.focus();
		ctrl.setSelectionRange(pos, pos);
	} else if (ctrl.createTextRange) {
		var range = ctrl.createTextRange();
		range.collapse(true);
		range.moveEnd('character', pos);
		range.moveStart('character', pos);
		range.select();
	}
}

//$(document).ready(function(){

	var parser = new RidmikParser();
	var writing = "";
	var phLen = 0;
	var banglaMode = true;
    var elem1 = document.getElementById("bangla");
    var elem2 = document.getElementById("bangla2");
	var ctrlPressed = false;

	elem1.onkeypress = function(e){
		var x = (window.event) ? e.keyCode : e.which; 
		console.log("pressed 0");

		if(ctrlPressed || !banglaMode)
			x = -1;
					
		if((x > 64 && x < 91) || (x > 96 && x < 123)){
			writing += String.fromCharCode(e.which);
			var bangla = parser.toBangla(writing);
			var p = insertAtCareto(elem1, phLen, bangla);
			setCaretPositiono(elem1, p);
			phLen = bangla.length;
			return false;
		}else {
			writing = "";
			phLen = 0;
		}
		return true;
	}
	elem1.onkeydown = function(e){
		var x = (window.event) ? e.keyCode : e.which;
		if(x == 17){
			ctrlPressed = true;
			return true;
		}
		// for chrome, when control is pressed, other keys go through keydown
		// and for firefox it goes through both keydown & keypress 
        if ((x == 109 || x == 77) && ctrlPressed) {
            banglaMode = !banglaMode;
            writing = "";
            phLen = 0;
        } 
		
}
    elem1.onkeyup = function (e) {
    var x = (window.event) ? e.keyCode : e.which;
    if (x == 17) {
        ctrlPressed = false;
    } else if (x == 8) { // for chrome, backspace is not in keypress event
        writing = "";
        phLen = 0;
    }
}
	
	
//});





writing = "";
phLen = 0;
banglaMode = true;
ctrlPressed = false;

elem2.onkeypress = function (e) {
    var x = (window.event) ? e.keyCode : e.which;
    console.log("pressed 1");

    if (ctrlPressed || !banglaMode)
        x = -1;

    if ((x > 64 && x < 91) || (x > 96 && x < 123)) {
        writing += String.fromCharCode(e.which);
        var bangla = parser.toBangla(writing);
        var p = insertAtCareto(elem2, phLen, bangla);
        setCaretPositiono(elem2, p);
        phLen = bangla.length;
        return false;
    } else {
        writing = "";
        phLen = 0;
    }
    return true;
}

elem2.onkeydown = function (e) {
    var x = (window.event) ? e.keyCode : e.which;
    if (x == 17) {
        ctrlPressed = true;
        return true;
    }
    // for chrome, when control is pressed, other keys go through keydown
    // and for firefox it goes through both keydown & keypress 
    if ((x == 109 || x == 77) && ctrlPressed) {
        banglaMode = !banglaMode;
        writing = "";
        phLen = 0;
    }

}

elem2.onkeyup = function (e) {
    var x = (window.event) ? e.keyCode : e.which;
    if (x == 17) {
        ctrlPressed = false;
    } else if (x == 8) { // for chrome, backspace is not in keypress event
        writing = "";
        phLen = 0;
    }
}




