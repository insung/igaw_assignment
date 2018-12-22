CREATE TABLE event_collects (
    event_id varchar(50) PRIMARY KEY,
    user_id varchar(255) NOT NULL,
    event_name varchar(50) NOT NULL,
    order_id varchar(50) UNIQUE NOT NULL,
    event_datetime timestamp without time zone NOT NULL
);

CREATE TABLE parameters (
    order_id varchar(50) REFERENCES event_collects(order_id),
    currency varchar(10),
    price integer
);