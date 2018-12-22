var pg = require('pg');
var dateFormat = require('dateformat');

var config = {
  host     : 'event-db.csgfkaaht30k.ap-northeast-2.rds.amazonaws.com',
  user     : 'insung',
  password : 'dlstjd85',
  database : 'event_collect_db',
  port     : 5432
};

exports.handler = async (event) => {
    var client = new pg.Client(config);
    var rand = Math.floor(Math.random() * (1000 - 1)) + 1;
    var now=dateFormat(new Date(), "yyyy-mm-dd hh:MM:ss");
    await client.connect();

    var queryString = `
INSERT INTO event_collects (event_id, user_id, event_name, order_id, event_datetime) 
VALUES ('${event.event_id}_${rand}', '${event.user_id}_${rand}', '${event.event_name}', '${event.parameters.order_id}_${rand}', '${now}');

INSERT INTO parameters (order_id, currency, price) 
VALUES ('${event.parameters.order_id}_${rand}', '${event.parameters.currency}', ${event.parameters.price});`;
    
    // db insert
    await client.query(queryString);
    await client.end();

    return `Successfully ${rand} processed`;
};
