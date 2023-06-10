let maxageperfaction = [];



getdata();




function getdata() {
    fetch('http://localhost:55349/stat/maxageperfaction').then(x => x.json()).then(y => {
        maxageperfaction = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    maxageperfaction.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>";
    });
}