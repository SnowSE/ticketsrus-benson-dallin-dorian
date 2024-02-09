--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.1 (Debian 16.1-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Create bdd role

Create role bdd;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: azure_pg_admin
--

-- CREATE SCHEMA public;


-- ALTER SCHEMA public OWNER TO azure_pg_admin;

--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: azure_pg_admin
--

-- COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: concert; Type: TABLE; Schema: public; Owner: bdd
--

CREATE TABLE public.concert (
    id integer NOT NULL,
    start_time timestamp without time zone NOT NULL,
    end_time timestamp without time zone,
    event_name character varying(60) NOT NULL,
    description character varying(300)
);


ALTER TABLE public.concert OWNER TO bdd;

--
-- Name: concert_id_seq; Type: SEQUENCE; Schema: public; Owner: bdd
--

CREATE SEQUENCE public.concert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.concert_id_seq OWNER TO bdd;

--
-- Name: concert_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: bdd
--

ALTER SEQUENCE public.concert_id_seq OWNED BY public.concert.id;


--
-- Name: ticket; Type: TABLE; Schema: public; Owner: bdd
--

CREATE TABLE public.ticket (
    id integer NOT NULL,
    qrhash character varying(16) NOT NULL,
    email character varying(40),
    concert_id integer,
    timescanned timestamp without time zone
);


ALTER TABLE public.ticket OWNER TO bdd;

--
-- Name: ticket_id_seq; Type: SEQUENCE; Schema: public; Owner: bdd
--

CREATE SEQUENCE public.ticket_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ticket_id_seq OWNER TO bdd;

--
-- Name: ticket_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: bdd
--

ALTER SEQUENCE public.ticket_id_seq OWNED BY public.ticket.id;


--
-- Name: concert id; Type: DEFAULT; Schema: public; Owner: bdd
--

ALTER TABLE ONLY public.concert ALTER COLUMN id SET DEFAULT nextval('public.concert_id_seq'::regclass);


--
-- Name: ticket id; Type: DEFAULT; Schema: public; Owner: bdd
--

ALTER TABLE ONLY public.ticket ALTER COLUMN id SET DEFAULT nextval('public.ticket_id_seq'::regclass);


--
-- Data for Name: concert; Type: TABLE DATA; Schema: public; Owner: bdd
--

COPY public.concert (id, start_time, end_time, event_name, description) FROM stdin;
1	2024-03-31 12:00:00	2024-03-31 16:00:00	The Magic Dragons	Experience The Magic Dragons – where music meets enchantment! Journey through mystical melodies and mesmerizing magic for an unforgettable evening of spellbinding entertainment! Don't miss this otherworldly concert experience!
2	2024-07-30 18:00:00	2024-07-30 23:30:00	El Livia Rod Ego	Immerse yourself in the captivating sounds of El Livia Rod Ego! Prepare for an unforgettable evening filled with soulful melodies and raw energy. Don't miss this sensational concert experience!
3	2025-02-10 15:00:00	2025-02-10 19:00:00	Scrapped Funk	Get ready to groove with Scrapped Funk! Experience their infectious rhythms and funky beats for a night of non-stop entertainment and dance.
\.


--
-- Data for Name: ticket; Type: TABLE DATA; Schema: public; Owner: bdd
--

COPY public.ticket (id, qrhash, email, concert_id, timescanned) FROM stdin;
1	1234567890abcdef	thenewdorian21@gmail.com	1	\N
\.


--
-- Name: concert_id_seq; Type: SEQUENCE SET; Schema: public; Owner: bdd
--

SELECT pg_catalog.setval('public.concert_id_seq', 3, true);


--
-- Name: ticket_id_seq; Type: SEQUENCE SET; Schema: public; Owner: bdd
--

SELECT pg_catalog.setval('public.ticket_id_seq', 1, true);


--
-- Name: concert concert_pkey; Type: CONSTRAINT; Schema: public; Owner: bdd
--

ALTER TABLE ONLY public.concert
    ADD CONSTRAINT concert_pkey PRIMARY KEY (id);


--
-- Name: ticket ticket_pkey; Type: CONSTRAINT; Schema: public; Owner: bdd
--

ALTER TABLE ONLY public.ticket
    ADD CONSTRAINT ticket_pkey PRIMARY KEY (id);


--
-- Name: ticket ticket_concert_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: bdd
--

ALTER TABLE ONLY public.ticket
    ADD CONSTRAINT ticket_concert_id_fkey FOREIGN KEY (concert_id) REFERENCES public.concert(id);


--
-- PostgreSQL database dump complete
--

