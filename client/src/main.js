async function connect(server_address, name) {
    const data = {
        name: name
    }

    fetch(server_address, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Connection-Type': 'connect'
        },
        body: JSON.stringify(data)
    })
    .then(response => response.json())
    .then(json => {
        showOutput(json["message"], "")
    })
    .catch(e => {
        showOutput("Error", e)
        console.error(e);
    });
    return false;
}

async function showOutput(status, response) {
    document.getElementById("http_status").innerHTML = status;
    document.getElementById("http_response").innerHTML = response;
}