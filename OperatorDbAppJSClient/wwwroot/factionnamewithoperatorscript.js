let factionnamewithoperator = [];



getdata();




function getdata() {
    fetch('http://localhost:55349/stat/factionnamewithoperators').then(x => x.json()).then(y => {
        factionnamewithoperator = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    factionnamewithoperator.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>";
    });
}