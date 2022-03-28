-- Table: public.Meals

-- DROP TABLE IF EXISTS public."Meals";

CREATE TABLE IF NOT EXISTS public."Meals"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Summary" text COLLATE pg_catalog."default",
    "LastDateTime" timestamp with time zone NOT NULL,
    "PhotoPath" text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    CONSTRAINT "PK_Meals" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Meals"
    OWNER to postgres;