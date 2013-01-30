/**
* Author: Derek J. Curtis
* Version: 0.9.0
* Website: http://www.summersetsoftware.com
* Git-Hub: https://github.com/derekjamescurtis/js-simple-pwd-gen
*/



// returns a randomly-generated string matching the provided criteria
function gen_pw(pw_len, pw_inc_ucase, pw_inc_lcase, pw_inc_num, pw_inc_punct) {
    "use strict";

    // populate array with all possible characters to go into password string
    var ch = [];
    for (var i = 0; i < 33; i++)
        ch[ch.length] = 0;
    for (; i < 48; i++)
        ch[ch.length] = pw_inc_punct ? 1 : 0;
    for (; i < 58; i++)
        ch[ch.length] = pw_inc_num ? 1 : 0;
    for (; i < 65; i++)
        ch[ch.length] = pw_inc_punct ? 1 : 0;
    for (; i < 91; i++)
        ch[ch.length] = pw_inc_ucase ? 1 : 0;
    for (; i < 97; i++)
        ch[ch.length] = pw_inc_punct ? 1 : 0;
    for (; i < 123; i++)
        ch[ch.length] = pw_inc_lcase ? 1 : 0;
    for (; i < 127; i++)
        ch[ch.length] = pw_inc_punct ? 1 : 0;
    ch[ch.length] = 0;

    // generate password -- rereun until we have a good password  
    var pw = "";    
    while (!test_pw(pw, pw_inc_ucase, pw_inc_lcase, pw_inc_num, pw_inc_punct)) {

        // (reset) password var to an empty string
        pw = '';

        // add randomly-chosen characters to our pw string
        do {
            var x = Math.floor(Math.random() * 128);
            if (ch[x] == 1) 
                pw += String.fromCharCode(x);            

        } while (pw.length < pw_len);     
    }

    return pw;
}

// checks a password string to see if it contains the required elements.
function test_pw(pw_string, req_ucase, req_lcase, req_num, req_punct) {
    "use strict";

    // test the password to see if it's good
    var found_ucase = false;
    var found_lcase = false;
    var found_num = false;
    var found_punct = false;


    // we're just going to be really basic in our match groups
    var ucase_pattern = /[A-Z]/;
    var lcase_pattern = /[a-z]/;
    var num_pattern = /[0-9]/; 


    // test each character to determine it's type.  
    for (var i = 0; i < pw_string.length; i++) {
        
        if (ucase_pattern.test(pw_string[i])) 
            found_ucase = true;        
        else if (lcase_pattern.test(pw_string[i])) 
            found_lcase = true;        
        else if (num_pattern.test(pw_string[i])) 
            found_num = true;        
        else 
            found_punct = true; // the only good way to test this exclusion, like we're doing here
    }


    // test each requirement to make sure it was met
    if (req_ucase == true && found_ucase == false)
        return false;
    else if (req_lcase == true && found_lcase == false)
        return false;
    else if (req_num == true && found_num == false)
        return false;
    else if (req_punct == true && found_punct == false)
        return false;
    else
        return true;
}

