var client = require('mongodb').MongoClient;

var exports = module.exports = {};

exports.go = function (callback) {
    var url = 'mongodb://' + process.env.MONGO_PORT_27017_TCP_ADDR + ':' + process.env.MONGO_PORT_27017_TCP_PORT + '/blocker';
    client.connect(url, callback);
};

