const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7204/';

const PROXY_CONFIG = [
  {
    context: [
      "/Person/GetPeople",
      "/Person/GetPersonByName/{name}",
      "/Person/CreatePerson",
      "/AstronautDuty/GetAstronautDutiesByName/{name}",
      "/AstronautDuty/CreateAstronautDuty",
      "/Auth/Login",
      "/Auth/Register",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
