var express = require('express'),
    bodyParser = require('body-parser');

var app = express();

app.use(bodyParser.json());

app.get('/highscores', function (req, res) {
    res.json({
        highscores: [
            {
                name: 'Jakob',
                height: 1000000000.0
            },
            {
                name: 'Jens',
                height: 12479173.0
            }
        ]
    });
});

app.post('/highscores', function (req, res) {
    console.log(req.body);
    res.json({
        message: 'I did nothing.'
    });
});

app.listen(8080);

console.log('Listening...');
