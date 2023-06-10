let operatorspreferredweapon = [];



getdata();




function getdata() {
    fetch('http://localhost:55349/stat/operatorspreferredweapon').then(x => x.json()).then(y => {
        operatorspreferredweapon = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    operatorspreferredweapon.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>";
    });
}
