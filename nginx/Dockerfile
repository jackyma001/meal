FROM nginx:1.17.10 AS dev
COPY nginx.dev.conf /etc/nginx/nginx.conf
EXPOSE 80

FROM dev AS prod
COPY nginx.conf /etc/nginx/nginx.conf