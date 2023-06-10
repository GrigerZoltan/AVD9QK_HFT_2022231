let minheightperfaction = [];



getdata();




function getdata() {
   fetch('http://localhost:55349/stat/minheightperfaction').then(x => x.json()).then(y => {
        minheightperfaction = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    minheightperfaction.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.key + "</td><td>"
            + t.value + "</td></tr>";
    });
}