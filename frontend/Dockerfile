FROM node:lts-alpine as build-stage

ARG VUE_APP_API_URL
ENV VUE_APP_API_URL ${VUE_APP_API_URL}

# make the 'app' folder the current working directory
WORKDIR /app

# copy 'package.json' to install dependencies
COPY package*.json ./
# install dependencies
RUN npm install
# copy files and folders to the current working directory (i.e. 'app' folder)
COPY . .
# build app for production with minification
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]