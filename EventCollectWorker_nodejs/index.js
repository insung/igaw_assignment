var pg = require('pg');
var dateFormat = require('dateformat');

var config = {
  host     : 'eventdb.csgfkaaht30k.ap-northeast-2.rds.amazonaws.com',
  user     : 'insung',
  password : 'dlstjd85',
  database : 'event_collect_db',
  port     : 5432
};

exports.handler = async (event) => {
    var body = JSON.parse(event.Records[0].body);
    
    if (body.event_id == null)
        return 'data not defined';

    var client = new pg.Client(config);
    var now=dateFormat(new Date(), "yyyy-mm-dd hh:MM:ss");
    await client.connect();

    var queryString = `
INSERT INTO event_collects (event_id, user_id, event_name, order_id, event_datetime) 
VALUES ('${body.event_id}', '${body.user_id}', '${body.event_name}', '${body.parameters.order_id}', '${now}');

INSERT INTO parameters (order_id, currency, price) 
VALUES ('${body.parameters.order_id}', '${body.parameters.currency}', ${body.parameters.price});`;
    
    // db insert
    await client.query(queryString);
    await client.end();
    
    return `Successfully ${body.user_id} processed`;
};
