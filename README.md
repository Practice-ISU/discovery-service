# discovery-service

![C#](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/ASP.NET-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![GRPC](https://img.shields.io/badge/grpc-2da7b0?style=for-the-badge&logoColor=white)
![License MIT](https://img.shields.io/badge/MIT-aa0000?style=for-the-badge&logoColor=white)

## Description

The repository contains the code necessary for developing and maintaining the discovery service within the ISU Practice project.

The main goal of the discovery service is to provide information about available services, their addresses, and versions so that other components of the system can easily find and use these services. It plays a crucial role in microservices architecture, enabling components to be dynamic and self-descriptive.

## Architecture

![Architecture](https://github.com/Practice-ISU/discovery-service/blob/main/images/discovery.png)

In this architecture, when each microservice is launched, it is registered in the discovery-service. Then discovery-service periodically checks the availability of registered microservices.
When making a request to the api-gateway, before accessing a specific microservice, the api-gateway requests information from the discovery-service about the availability of this microservice and its communication channel.
