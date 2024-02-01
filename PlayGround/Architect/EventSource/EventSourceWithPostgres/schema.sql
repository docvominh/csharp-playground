CREATE TABLE events
(
    id        SERIAL PRIMARY KEY,
    stream_id BIGINT NOT NULL,
    version   BIGINT NOT NULL,
    data      JSONB  NOT NULL,
    UNIQUE (stream_id, version)
);


CREATE TABLE users
(
    id       SERIAL PRIMARY KEY,
    username TEXT UNIQUE,
    address TEXT
);
