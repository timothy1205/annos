FROM node:20-alpine

WORKDIR /usr/src/app
COPY package.json package-lock.json ./

RUN npm install -g @angular/cli
RUN npm ci
COPY . ./

EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0"]
