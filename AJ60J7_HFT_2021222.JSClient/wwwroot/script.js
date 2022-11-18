let brands = [];
getdata();

async function getdata() {
    await fetch('http://localhost:44728/brand/')
        .then(x => x.json())
        .then(y => {
            brands = y;
            display();
        });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
function remove(id) {
    fetch('http://localhost:44728/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}


function create() {
    let brandName = document.getElementById('brandname').value;
    fetch('http://localhost:44728/brand/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandname: brandName })})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}