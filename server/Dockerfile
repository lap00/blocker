FROM node:argon

RUN mkdir -p /blocker
WORKDIR /blocker

COPY . /blocker/
RUN npm install

EXPOSE 8080

ENTRYPOINT [ "npm", "start" ]

