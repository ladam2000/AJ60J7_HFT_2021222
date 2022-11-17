let brands = [];
getdata();

async function getdata() {
    await fetch('http://localhost:44728/brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(brands);
            display();
        });
}

function display() {
    brands.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td></tr>"
        console.log(t.name)
    });
}

function create() {
    let brandName = document.getElementById('brandname').value;
    fetch('http://localhost:44728/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: brandName })})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}