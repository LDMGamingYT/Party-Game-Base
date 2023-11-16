async function connect(server_address, name) {
    const data = {
        name: name
    }

    console.log("Request:", new Request(server_address, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Connection-Type': 'connect'
        },
        body: JSON.stringify(data)
    }));

    fetch(server_address, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Connection-Type': 'connect'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        console.log(response);
        // print the request
        return response.json();
    })
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