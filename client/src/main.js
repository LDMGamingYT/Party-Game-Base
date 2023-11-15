async function connect(server_address, name) {
    const data = {
        name: name
    }

    fetch(server_address, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(result => {
            console.log(result);
        })
        .catch(error => {
            console.error(error);
        });
    return false;
}