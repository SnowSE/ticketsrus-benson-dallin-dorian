-- Create the concert table
CREATE TABLE IF NOT EXISTS concert (
    id SERIAL PRIMARY KEY,
    description VARCHAR(300),
    end_time TIMESTAMP WITHOUT TIME ZONE,
    event_name VARCHAR(60),
    start_time TIMESTAMP WITHOUT TIME ZONE
);

-- Create the ticket table
CREATE TABLE IF NOT EXISTS ticket (
    id SERIAL PRIMARY KEY,
    concert_id INTEGER REFERENCES concert(id),
    email VARCHAR(40),
    qrhash VARCHAR(16),
    timescanned TIMESTAMP WITHOUT TIME ZONE
);

-- Create indexes for foreign keys
CREATE INDEX IF NOT EXISTS ticket_concert_id_idx ON ticket(concert_id);

-- Create primary key constraints explicitly (Optional)
ALTER TABLE concert ADD CONSTRAINT concert_pkey PRIMARY KEY (id);
ALTER TABLE ticket ADD CONSTRAINT ticket_pkey PRIMARY KEY (id);

-- Create foreign key constraint for tickets referencing concerts
ALTER TABLE ticket ADD CONSTRAINT ticket_concert_id_fkey FOREIGN KEY (concert_id) REFERENCES concert(id);


-- Insert a sample row into the concert table
INSERT INTO concert (description, end_time, event_name, start_time)
VALUES ('Sample Concert Description', '2024-03-18 20:00:00', 'Sample Event', '2024-03-18 18:00:00');

-- Insert a sample row into the ticket table
INSERT INTO ticket (concert_id, email, qrhash, timescanned)
VALUES (1, 'sample@email.com', 'ABCD1234', '2024-03-18 19:00:00');
