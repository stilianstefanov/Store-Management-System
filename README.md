# Store Management System
![License](https://img.shields.io/badge/license-MIT-blue)

## Overview

The Store Management System is a robust application designed to streamline operations for small to medium-sized supermarkets. At its core, the system provides comprehensive management of products and warehouses, while also facilitating the monitoring of Gross Merchandise Value (GMV). A standout feature of the system is the ability to handle delayed payments, allowing supermarkets to offer flexible payment solutions to their clients.

### Key Features

- **Product Management:** Manage product inventory with real-time updates and stock monitoring.
- **Warehouse Oversight:** Manages the stocks of products and suggests order quantities based on current stock levels to ensure optimal inventory management.
- **GMV Tracking:** Automatically calculate and track Gross Merchandise Value to assess sales performance and support data-driven decision-making. (In development)
- **Delayed Payments:** Provide customers with deferred payment options to enhance service and financial management.

### Technical Architecture

The project is built using a microservice architecture with .NET following the REST principles and ensuring scalability and robust performance. It includes five main services:

- **Product Service:** Manages all operations related to products.
- **Warehouse Service:** Oversees stock management, including tracking stock levels and providing recommendations for reorder quantities to maintain optimal inventory levels.
- **Delayed Payment Service:** Facilitates the processing and management of delayed payments.
- **GMV Service:** Responsible for the calculation and reporting of GMV. (In development)
- **Identity Service:** Manages secure access and user interactions across the platform.

These services utilize RabbitMQ and gRPC for efficient and reliable inter-service communication. The entire backend is deployed on Kubernetes, offering enhanced scalability and resilience. The Ingress NGINX controller serves as the gateway, facilitating smooth interaction with external systems and clients.

### Frontend

The interface is crafted with React, providing a responsive and intuitive experience that simplifies supermarket management.

## Technologies Used

This project utilizes a range of technologies to deliver a comprehensive and robust system. Below is a breakdown of the main technologies and libraries used:

### Backend
- **.NET 8**: Used for building the microservices with high performance and scalability in mind.
- **Entity Framework**: ORM for data access, simplifying interactions with the MSSQL Server database.
- **MSSQL Server**: The database system used for storing all application data reliably.
- **JWT**: Utilized for securing the services by handling authentication and authorization across the system.
- **RabbitMQ**: Messaging broker for handling communications between the different services efficiently.
- **gRPC**: A high-performance, open-source framework for handling remote procedure calls, used between microservices.
- **Kubernetes**: Container orchestration platform used to deploy, scale, and manage the microservices.

### Frontend
- **React**: A JavaScript library for building user interfaces, enabling dynamic and responsive client-side interactions.
- **HTML & CSS**: Core technologies for structuring and styling the web application.
- **React Router**: Utilized for handling navigation and routing within the React application, enhancing the single-page app (SPA) experience.
- **React-toastify**: A library used for adding notifications to the React applications.
- **React-loading**: Used to manage loading states in the UI, improving user experience during asynchronous operations.

### Networking
- **Ingress NGINX**: An Ingress controller for Kubernetes using NGINX as a reverse proxy and load balancer.

## Screenshots

Below are some screenshots from the Store Management System, showcasing its key functionalities:
