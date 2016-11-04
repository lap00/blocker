var express = require('express');

var app = express();

app.get('/', function (req, res) {
    res.send(JSON.stringify({foo: 'bar'}));
});

app.listen(8080);

console.log('Listening...');
