fetch('http://localhost:44728/brand')
    .then(x => x.json())
    .then(y => console.log(y));