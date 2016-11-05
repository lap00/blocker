var express = require('express'),
    bodyParser = require('body-parser'),
    assert = require('assert'),
    async = require('async'),
    mongo = require('./mongo.js');

var app = express();

app.use(bodyParser.json());

app.get('/highscores', function (req, res) {
    mongo.go(function (err, db) {
        db.collection('bests').find({}).sort({height: -1}).limit(5).toArray(function (err, docs) {
            res.json({ highscores: docs });
        });
    });
});

app.post('/states', function (req, res) {
    async.waterfall([
        function (callback) {
            mongo.go(function (err, db) {
                db.collection('states').updateOne(
                    { token: req.body.token },
                    { $set: {
                        name: req.body.name,
                        height: req.body.height,
                        count: req.body.count,
                        weight: req.body.weight
                    }},
                    { upsert: true }
                );
                return callback(null);
            });
        },
        function (callback) {
            mongo.go(function (err, db) {
                db.collection('bests').findOne(
                    { name: req.body.name },
                    null,
                    null,
                    function (err, doc) { return callback(null, doc ? doc : req.body); }
                );
            });
        },
        function (best, callback) {
            mongo.go(function (err, db) {
                db.collection('bests').update(
                    { name: req.body.name },
                    { $set: {
                        height: Math.max(req.body.height, best.height),
                        weight: Math.max(req.body.weight, best.weight),
                        count:  Math.max(req.body.count,  best.count)
                    }},
                    { upsert: true }
                );
            });
            return callback(null);
        },
        function (callback) {
            res.json({});
        }
    ]);
});

mongo.go(function (err, db) {
    assert.equal(err, null);
    db.close();
});

app.listen(8080);

console.log('Listening...');
