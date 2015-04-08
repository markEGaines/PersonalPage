$(document).ready(function() {

    // Pre-load Task 1
    //$("#p1").val(Math.floor((Math.random() * 9999) + 1));
    //$("#p2").val(Math.floor((Math.random() * 9999) + 1));
    //$("#p3").val(Math.floor((Math.random() * 9999) + 1));
    //$("#p4").val(Math.floor((Math.random() * 9999) + 1));
    //$("#p5").val(Math.floor((Math.random() * 9999) + 1));

    //Pre-load Task 2
    $("#t2n1").val("1,2,3,4,5");

    //Pre-load Task 3
    $("#t3n1").val(5);




    //Pre-load Task 2.1
    //$("#t21n1").val(6);

    //Pre-load Task 2.5
    $("#t25n1").val(9);

    //Pre-load Task 2.7
    $("#t27s1").val("Alice");

    // Task 1 -------------------------------------------------------------
    // Task 1 find MAX value
    // Task 1 -------------------------------------------------------------
    // Event Handler: isolate the function from the HTML tags



    //task1 = function() {
    //    $("#result1").html("Max Value:" + maxOfFive(
	//		$('#t1n1').val(), $('#t1n2').val(), $('#t1n3').val(), $('#t1n4').val(), $('#t1n5').val()));
    //}
    //$('#task1').click(task1);

    //maxOfFive = function(n1, n2, n3, n4, n5) {
    //    return (Math.max(n1, n2, n3, n4, n5));
    //}

    function maxOfFive() {
        var num = Math.max(parseInt($("#p1").val()),
                           parseInt($("#p2").val()),
                           parseInt($("#p3").val()),
                           parseInt($("#p4").val()),
                           parseInt($("#p5").val()));

        $("#maxResult").html(num + " is the max number.");
    }
    $('#task1').click(maxOfFive);


    // randomizer button loads test data
    task1a = function () {
        $("#p1").val(Math.floor((Math.random() * 9999) + 1));
        $("#p2").val(Math.floor((Math.random() * 9999) + 1));
        $("#p3").val(Math.floor((Math.random() * 9999) + 1));
        $("#p4").val(Math.floor((Math.random() * 9999) + 1));
        $("#p5").val(Math.floor((Math.random() * 9999) + 1));
    };
    $('#task1a').click(task1a);





    // Task 2 -------------------------------------------------------------
    // Task 2 Sums and Products
    // Task 2 -------------------------------------------------------------
    // Event Handler:
    task2 = function(x) {
        var xAsArray = x.split(",");
        $("#result2sum").html("Sum: " + sum(xAsArray));
        $("#result2mult").html("Multiply: " + multiply(xAsArray));
    }

    sum = function(array) {
        var ttl = 0;
        for (i in array) {
            ttl = ttl + Number(array[i]);
        }
        return (ttl);
    };

    multiply = function(array) {
        var ttl = 1;
        for (i in array) {
            ttl = ttl * Number(array[i]);
        }
        return (ttl);
    };


    // Task 3 -------------------------------------------------------------
    // Task 3 Factorial
    // Task 3 -------------------------------------------------------------
    // Event Handler:
    task3 = function(n1) {
        $("#result3").html("Factorial: " + factorial(n1));
    };

    factorial = function(n1) {
        var ttl = 1;
        for (var i = n1; i > 0; i--) {
            ttl = ttl * i;
        }
        return (ttl);
    };

    // randomizer button loads test data
    task3a = function () {
        $("#t3n1").val(Math.floor((Math.random() * 10) + 2));
     };
    $('#task3a').click(task3a);

    // Task 4 -------------------------------------------------------------
    // Task 4 Palindromes
    // Task 4 -------------------------------------------------------------
    // Event Handler:
    task4 = function(x) {
        $("#result4").html(palindrome(x));
    }

    palindrome = function(x) {
        var xAsArray = x.replace(/[\W]+/g, '').toLowerCase().split(""); // use regular expression to match any non-word character and remove it
        var j = xAsArray.length;
        for (var i = 0; i < j; i++) {
            j--
            if (xAsArray[i] !== xAsArray[j]) {
                return ("No, Sorry, NOT a Palindrome");
            }
        }
        return ("YES, it IS a Palindrome!");
    }

    // suggest button text for Palindrome
    var palindromes = ["radar",
                       "A Man, A Plan, A Canal, Panama!",
                       "Are we not drawn onward, we few? Drawn onward to new era?",
                       "Are we not drawn onward, we few? Drawn onward to a new era?",
                       "Able was I, ere I saw Elba.",
                       "Madam, I'm Adam."];
    var q = 0;
    task4a = function () {
        if (q < palindromes.length -1) { q++ } else { q = 0 };
        var x = palindromes[q];
        $("#t4a1").val(x);
        $("#result4").html("?");
    }

    $('#task4a').click(task4a);


    // Task 5 -------------------------------------------------------------
    // Task 5 Fizz Buzz 
    // Task 5 -------------------------------------------------------------

    task5 = function () {
        var result = "";
        for (var i = 1; i <= 100; i++) {
            if (i % 3 == 0 && i % 5 == 0) {
                result += "FizzBuzz<br/>";
            } else
                if (i % 3 == 0) {
                    result += "Fizz<br/>";
                } else
                    if (i % 5 == 0) {
                        result += "Buzz<br/>";
                    } else
                        result += i + "<br/>";
        }
        $('#result5').html(result);
    };

    // Section 2
    // Task 1 -------------------------------------------------------------
    // Task 1 Perfect Numbers
    // Task 1 -------------------------------------------------------------
    // Event Handler:
    task21a = function(n1) {
        $("#result21").html("Perfect Number: " + isPerfect(n1));
    }

    isPerfect = function(n1) {
        var array = [];
        for (var i = (n1 / 2) ; i > 0; i--) { // find whole divisors
            if (n1 % i == 0) {
                array.push(i);
            }
        }
        if (sum(array) == n1) { // sum divisiors to determine if equal to candidate
            return (true);
        } else
            return (false);
    }

    listPerfect = function () {    // iterate to find Perfect numbers below 10,000
        var result = "";
        for (var i = 1; i <= 10000; i++) {
            if (isPerfect(i)) {
                result += (i + "<br>");
            }
        }
        $('#result21b').html(result);
    };

    // Section 2
    // Task 2 -------------------------------------------------------------
    // Task 2 Happy Numbers
    // Task 2 -------------------------------------------------------------

    listHappy = function () {
        var result = "";
        var hits = 0;
        var i = 0;
        do {
            i++
            if (isHappy(i)) {
                hits++
                result += (i + "<br>");
            }
        }
        while (hits < 5);
        $('#task22Result').html(result);
    };


    isHappy = function (n1) { 
        var str = n1.toString();
        var digits = str.split(""); 
        var sumOfDigitsSquared = 0;
        for (var j in digits) { 
            sumOfDigitsSquared = Math.pow(digits[j], 2) + sumOfDigitsSquared;
        };

        if (sumOfDigitsSquared == 1) {
            return (true);
        } else if (sumOfDigitsSquared == 4) { // detect loop
            return (false);
        } else
            return (isHappy(sumOfDigitsSquared));
    };

    // Section 2
    // Task 3 -------------------------------------------------------------
    // Task 3 Armstrong Numbers
    // Task 3 -------------------------------------------------------------

    listArmstrong = function() {
        var result = "";
        var hits = 0;
        var i = 99;
        for (var i = 100; i <= 999; i++) { // document.write("listArmstrong testing:" + i + "<br>");
            if (isArmstrong(i)) {
                result += (i + "<br>");
            }
        }
        $('#task23Result').html(result);
    }


    isArmstrong = function (n1) { // document.write("isArmstrong top<br>");
        var str = n1.toString();
        var digits = str.split(""); // document.write("isArmstrong str: " + str  + "<br>");
        var sumOfDigitsRaised = 0;
        for (var j in digits) { // document.write("isArmstrong j: " + j + " digit: " + digits[j] + "<br>");
            sumOfDigitsRaised = Math.pow(digits[j], digits.length) + sumOfDigitsRaised;
        } // document.write("  sumOfDigitsRaised:" + sumOfDigitsRaised + "<br>");
        if (sumOfDigitsRaised == n1) {
            return (true);
        } else
            return (false);
    };


    // Section 2
    // Task 4 -------------------------------------------------------------
    // Task 4 find longest word
    // Task 4-------------------------------------------------------------

    longestWord = function() {
        var input = $('#textfile4')[0];
        var reader = new FileReader();
        reader.onload = function() {
            $("#longestWordResult").html("The longest word is:" + findLongestWord(reader.result));
        };
        reader.readAsText(input.files[0]);
    }

    function findLongestWord(str) {
        var array1 = str.match(/\w[a-z]{0,}/gi);
        var result = array1[0];
        for (var x = 1; x < array1.length; x++) {

            if (result.length < array1[x].length) {
                result = array1[x];
            }
        }
        return result;
    }

    //document.getElementById('fileinput').addEventListener('change', readSingleFile, false); */


    // Section 2
    // Task 5 -------------------------------------------------------------
    // Task 5 filter longest words
    // Task 5-------------------------------------------------------------

    task25 = function(n1) {
        var input = $('#textfile5')[0];
        var reader = new FileReader();

        reader.onload = function() {
            $("#filterLongestWordsResult").html(filterLongestWords(n1, reader.result));
        };
        reader.readAsText(input.files[0]);
    }

    function filterLongestWords(n1, str) {
        var array1 = str.toLowerCase().match(/\w[a-z]{0,}/gi).sort();
        var result = "Words longer than " + n1 + " characters:";
        for (var x = 1; x < array1.length; x++) {

            if (n1 < array1[x].length) {
                if (array1[x] == array1[x - 1]) { continue }
                result = result + "  " + array1[x];
            }
        }
        return result;
    }

    // Section 2
    // Task 6 -------------------------------------------------------------
    // Task 6 word frequency count  wordFreq     RANKED!!
    // Task 6 -------------------------------------------------------------

    wordFreq = function() {
        var input = $('#textfile6')[0];
        var reader = new FileReader();
        reader.onload = function() {
            $("#wordFreqResult").html(findWordFreq(reader.result));
        };
        reader.readAsText(input.files[0]);
    };

    function findWordFreq(str) {
        var array1 = str.toLowerCase().match(/\w[a-z]{0,}/gi);         //load words in document into array
        var obj1 = {};
        for (var i in array1) {                                        //iterate through array counting words
            if (obj1[array1[i]] == null) {                             // have we seen this word
                (obj1[array1[i]]) = 1                                   // if no, count the first sighting
            } else {
                (obj1[array1[i]])++                                    // if yes, increment the count
            }
        };
        var array2 = [];
        for (var i in obj1) {                                         //put results back into an array
            array2.push([i, obj1[i]]);
        };
        array2.sort(function(a, b) {                                  //sort by count (rank)
            return b[1] - a[1]
        });
        var result = "Word/Count:";                                   //write an output string
        for (var i in array2) {
            result = result + "  " + array2[i];
        };
        return result;
    };

    // Section 2
    // Task 7 -------------------------------------------------------------
    // Task 7 count occurrences of a specific word
    // Task 7 -------------------------------------------------------------

    findWord = function(searchStr1) {
        var input = $('#textfile7')[0];
        var reader = new FileReader();
        reader.onload = function() {
            $("#findWordResult").html("Counted:" + countAWord(searchStr1, reader.result));
        };
        reader.readAsText(input.files[0]);
    }

    function countAWord(searchStr1, str) {
        var array1 = str.toLowerCase().match(/\w[a-z]{0,}/gi);
        var searchStr1 = searchStr1.toString().toLowerCase();
        var result = 0;
        for (var x in array1) {
            if (searchStr1 == array1[x]) {
                result++;
            }
        }
        return result;
    }

});