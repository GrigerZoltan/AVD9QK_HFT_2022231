let operatorsperfaction = [];



getdata();




function getdata() {
     fetch('http://localhost:55349/stat/operatorsperfaction').then(x => x.json()).then(y => {
        operatorsperfaction = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    operatorsperfaction.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>";
    });
}



