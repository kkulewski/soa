FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY --from=build-stage /app/node_modules/oidc-client/dist/oidc-client.min.js /usr/share/nginx/html/js/oidc-client.min.js
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]