const fetch = require('node-fetch');
var uuid = require('uuid/v4');
var moment = require('moment');

var index = function (req, res) {

  fetch('http://54.180.145.32/api/search/all')
  .then(res => res.json())
  .then(json => {
      res.render('pages/index', {
        events: json,
        date: moment().format('YYYY-MM-DD HH:mm:ss')
      });
   })
  .catch(err => { console.error(err); });
};

var add = function (req, res) {
  var guid = uuid();

  var jsonData = `{ 
    "event_id": "${guid}",
    "user_id": "${req.body.user}",
    "event_name": "${req.body.event}",
    "parameters": {
      "order_id": "${guid}",
      "currency": "krw",
      "price": "${req.body.price}"
    }
  }`;

  fetch('http://13.125.77.243/api/collect', {
    method: 'post',
    body: jsonData,
    headers: { 'Content-Type': 'application/json' }
  })
  .then(res => res.json())
  .then(json => { console.log(json); res.status(200).send(json); })
  .catch(err => { console.error(err); res.status(500); });
};

module.exports = {
  index: index,
  add: add
};