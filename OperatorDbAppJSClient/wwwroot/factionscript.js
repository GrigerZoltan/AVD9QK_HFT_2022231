let factions = [];

let connection = null;

getdata();



setupSignalR();

let factionIdToUpdate = null;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:55349/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("FactionCreated", (user, message) => {
        getdata();
    });

    connection.on("FactionDeleted", (user, message) => {
        getdata();
    });

    connection.on("FactionUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });

    start();
}



async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }

};

async function getdata() {
    await fetch('http://localhost:55349/faction').then(x => x.json()).then(y => {
        factions = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    factions.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.factionId + "</td><td>"
            + t.factionName + "</td><td>" + t.nation + "</td><td>" + `<button type="button" onclick="remove(${t.factionId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.factionId})">Update</button>` + "</td></tr>";
    });
}

function create() {
    let name = document.getElementById('factionname').value;
    let nation = document.getElementById('factionnation').value;

    fetch('http://localhost:55349/faction', {
        method: 'POST', headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ FactionName: name, Nation: nation}),
    }).then(response => response)
        .then(data => {
            console.log('Succes: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });


}

function remove(id) {
    fetch('http://localhost:55349/faction/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Succes: ', data); 
            getdata();
        }).catch((error) => {
            console.error('Error: ', error);
        })
}

function showupdate(id) {
    document.getElementById('factionnametoupdate').value = factions.find(t => t['factionId'] == id)['factionName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    factionIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('factionnametoupdate').value;
    let nation = document.getElementById('factionnationtoupdate').value;
    
    fetch('http://localhost:55349/faction', {
        method: 'PUT', headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ FactionName: name, Nation: nation, FactionId: factionIdToUpdate }),
    }).then(response => response)
        .then(data => {
            console.log('Succes: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });


}