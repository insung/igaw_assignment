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
    //console.log(event);
    //if (event.event_id == null)
    //    return 'data not defined';
        
    var client = new pg.Client(config);
    var rand = Math.floor(Math.random() * (1000 - 1)) + 1;
    var now=dateFormat(new Date(), "yyyy-mm-dd hh:MM:ss");
    var msg = JSON.stringify(event.Records[0].body, null, 2);
    var body = JSON.parse(event.Records[0].body);
    
    await client.connect();
    
    var queryString = `INSERT INTO events (msg) VALUES ('${msg}');`;
    
    // db insert
    await client.query(queryString);
    await client.end();

    return `Successfully ${rand} processed`;
};
