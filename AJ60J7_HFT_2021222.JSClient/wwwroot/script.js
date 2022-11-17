let brands = [];

fetch('http://localhost:44728/brand')
    .then(x => x.json())
    .then(y => {
        brands = y;
        console.log(brands);
        display();
    });

function display() {
    brands.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td></tr>"
        console.log(t.name)
    });

}