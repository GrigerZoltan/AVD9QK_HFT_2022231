let weapons = [];

let connection = null;

getdata();



setupSignalR();

let weaponIdToUpdate = null;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:55349/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("WeaponCreated", (user, message) => {
        getdata();
    });

    connection.on("WeaponDeleted", (user, message) => {
        getdata();
    });

    connection.on("WeaponUpdated", (user, message) => {
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
    await fetch('http://localhost:55349/weapon').then(x => x.json()).then(y => {
        weapons = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    weapons.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.weaponId + "</td><td>"
        + t.weaponName + "</td><td>" + t.caliber + "</td><td>" + t.facturer + "</td><td>" + t.operatorId + "</td><td>" + `<button type="button" onclick="remove(${t.weaponId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.weaponId})">Update</button>` + "</td></tr>";
    });
}

function create() {
    let name = document.getElementById('weaponname').value;
    let caliber = document.getElementById('caliber').value;
    let facturer = document.getElementById('facturer').value;
    let operatorid = document.getElementById('operatorid').value;

    fetch('http://localhost:55349/weapon', {
        method: 'POST', headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ WeaponName: name, Caliber: caliber, Facturer: facturer, operatorId: operatorid }),
    }).then(response => response)
        .then(data => {
            console.log('Succes: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });


}

function remove(id) {
    fetch('http://localhost:55349/weapon/' + id, {
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
    document.getElementById('weaponnametoupdate').value = weapons.find(t => t['weaponId'] == id)['weaponName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    weaponIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';

    let name = document.getElementById('weaponnametoupdate').value;
    let caliber = document.getElementById('calibertoupdate').value;
    let facturer = document.getElementById('facturertoupdate').value;
    let operatorid = document.getElementById('operatoridtoupdate').value;

    fetch('http://localhost:55349/weapon', {
        method: 'PUT', headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ WeaponName: name, Caliber: caliber, Facturer: facturer, operatorId: operatorid, weaponId: weaponIdToUpdate }),
    }).then(response => response)
        .then(data => {
            console.log('Succes: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });


}