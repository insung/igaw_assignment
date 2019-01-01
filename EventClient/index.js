const express = require('express');
const path = require('path');
const app = express();

const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.set('view engine', 'pug');
app.set('views', path.join(__dirname, 'views'));

var router = require('./router');
app.use('/', router);

var config = require('./config.json');

// bootstrap, jquery 등의 외부 라이브러리를 바인딩 하기 위함
app.use('/external', express.static(__dirname + '/views/external'));

var server = app.listen(config.port, () => {
    console.log(`listening on port ${config.port}`);
});