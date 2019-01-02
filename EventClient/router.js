var express = require('express');
var router = express.Router();

var home = require('./controllers/homeController');
router.get('/', home.index);

router.post('/event/add', home.add);

module.exports = router;