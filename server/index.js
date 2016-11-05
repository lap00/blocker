var express = require('express'),
    bodyParser = require('body-parser'),
    assert = require('assert'),
    async = require('async'),
    client = require('mongodb').MongoClient;

var app = express();

var mongoUrl = 'mongodb://' + process.env.MONGO_PORT_27017_TCP_ADDR + ':' + process.env.MONGO_PORT_27017_TCP_PORT + '/blocker';

app.use(bodyParser.json());

app.get('/highscores', function (req, res) {
    client.connect(mongoUrl, function (err, db) {
        db.collection('bests').find({}).sort({height: -1}).limit(5).toArray(function (err, docs) {
            res.json({ highscores: docs });
        });
        db.close();
    });
});

app.get('/highscores/:name', function (req, res) {
    client.connect(mongoUrl, function (err, db) {
        db.collection('bests').findOne(
            { name: req.params.name.toLowerCase() },
            null,
            { sort: [['height', 'desc']] },
            function (err, doc) { res.json(doc ? doc : {}); }
        );
        db.close();
    });
});

app.get('/highscores/nemesis/:height', function (req, res) {
    client.connect(mongoUrl, function (err, db) {
        db.collection('bests').findOne(
            { height: { $gt: parseFloat(req.params.height) } },
            null,
            { sort: 'height' },
            function (err, doc) { res.json(doc ? doc : {}); }
        );
        db.close();
    });
});

app.post('/states', function (req, res) {
    client.connect(mongoUrl, function (err, db) {
        async.waterfall([
            function (callback) {
                db.collection('states').updateOne(
                    { token: req.body.token },
                    { $set: {
                        name: req.body.name.toLowerCase(),
                        height: req.body.height,
                        count: req.body.count,
                        weight: req.body.weight
                    }},
                    { upsert: true }
                );
                return callback(null);
            },
            function (callback) {
                db.collection('bests').findOne(
                    { name: req.body.name.toLowerCase() },
                    null,
                    null,
                    function (err, doc) { return callback(null, doc ? doc : req.body); }
                );
            },
            function (best, callback) {
                db.collection('bests').update(
                    { name: req.body.name.toLowerCase() },
                    { $set: {
                        height: Math.max(req.body.height, best.height),
                        weight: Math.max(req.body.weight, best.weight),
                        count:  Math.max(req.body.count,  best.count)
                    }},
                    { upsert: true }
                );
                return callback(null);
            },
            function (callback) {
                db.close();
                res.json({});
            }
        ]);
    });
});

app.listen(8080);

console.log('Listening...');
