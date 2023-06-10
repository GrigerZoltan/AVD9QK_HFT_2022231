let operators = [];

let connection = null;

getdata();

setupSignalR();

let operatorIdToUpdate = null;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:55349/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("OperatorCreated", (user, message) => {
        getdata();
    });

    connection.on("OperatorDeleted", (user, message) => {
        getdata();
    });

    connection.on("OperatorUpdated", (user, message) => {
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
    await fetch('http://localhost:55349/operator').then(x => x.json()).then(y => {
        operators = y

        display();
    });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    operators.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.operatorId + "</td><td>"
        + t.name + "</td><td>" + t.age + "</td><td>" + t.height + "</td><td>" + t.factionId + "</td><td>" + t.weaponId + "</td><td>" + `<button type="button" onclick="remove(${t.operatorId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.operatorId})">Update</button>` +"</td></tr>";       
    });
}

function create() {
    let name = document.getElementById('operatorname').value;
    let age = document.getElementById('operatorage').value;
    let height = document.getElementById('operatorheight').value;
    let factionId = document.getElementById('factionId').value;
    let weaponId = document.getElementById('weaponId').value;
    fetch('http://localhost:55349/operator', {
        method: 'POST', headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ name: name, age: age, height: height, factionId: factionId, weaponId: weaponId }),
    }).then(response => response)
        .then(data =>
        {
            console.log('Succes: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });

   
}

function remove(id) {
    fetch('http://localhost:55349/operator/' + id, {
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
    document.getElementById('operatornametoupdate').value = operators.find(t => t['operatorId'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    operatorIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('operatornametoupdate').value;
    let age = document.getElementById('operatoragetoupdate').value;
    let height = document.getElementById('operatorheighttoupdate').value;
    let factionId = document.getElementById('factionidtoupdate').value;
    let weaponId = document.getElementById('weaponidtoupdate').value;
    fetch('http://localhost:55349/operator', {
        method: 'PUT', headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ name: name, age: age, operatorId: operatorIdToUpdate, height: height, factionId: factionId, weaponId: weaponId }),
    }).then(response => response)
        .then(data => {
            console.log('Succes: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });


}