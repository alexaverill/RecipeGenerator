
FROM node:alpine
WORKDIR /app
COPY package.json ./
COPY package-lock.json ./
RUN npm i
COPY . ./
#Convert to production build rather then a dev build
CMD ["npm", "start"]