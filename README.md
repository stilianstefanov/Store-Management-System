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

### Home page (not logged in)

![Home page (not logged in)](assets/Home-not-logged.png "Home Page")

### Login
*Allows the user to log in with his email or username.*

![Login](assets/login.png "Login")

### Register

![Register](assets/register.png "Register")

### Home page (logged in)
*The Main Dashboard serves as the operational hub for cashiers. With a seamless barcode reader integration (currently under development), products are quickly added to the transaction list upon scanning. Each item can be removed easily with the appearance of a remove button upon hover. The 'Finish Transaction' button completes the sale and updates inventory quantities accordingly. For customer convenience, the 'Delayed Payment' button triggers a modal to select or add a client, recording the transaction for future payment—ideal for repeat customers who prefer to settle their accounts at a later date.*

![Home page (logged in)](assets/home-logged.png "Home Page Logged")

### Delayed payment modal

![dPaymentModal](assets/delayed-payment-modal.png "Delayed payment modal")

### Insufficient credit modal
*The 'Insufficient Credit' modal is a critical feature for managing customer transactions on credit. When a client's purchases approach or exceed their allotted credit limit, this prompt appears, displaying their current credit, credit limit, total purchase cost, and the insufficient amount that prevents the transaction from completing. It offers the functionality to update the client's credit limit on the spot, enabling a seamless continuation of the checkout process.*

![Insufficient credit modal](assets/insufficient-credit-modal.png "insufficient credit modal")

### Products
*The Products page is a central feature of the Store Management System, offering a detailed and paged overview of all products. Users can effortlessly search for products, sort the list by various attributes like name, price, or quantity, and decide the number of products to display per page for customized viewing. Clicking on any product item brings up a detailed product information window, making it simple to view and manage product specifics.*

![Products](assets/products.png "Products")

### Add new product
*Streamlining product entry, the 'Add New Product' form is a vital component for inventory expansion. Users can input new product details, including barcode, name, description, and price, as well as defining the quantity and setting minimum and maximum stock levels. The form also allows for selecting the supplying warehouse from a searchable dropdown, ensuring that each product is properly linked to its source. Dynamic validation is built into the form, ensuring data integrity and prompting users for any necessary corrections before submission.*

![Add new product](assets/add-product.png "Add new product")

### Product details
*The Product Details window presents a comprehensive view of individual product metrics and management options. Detailed information such as barcode, name, description, price, delivery price, stock quantities, and linked warehouse is neatly displayed. Administrative actions are readily accessible, with options to update product details, add stock quantities, change the assigned warehouse, or delete the product from the inventory.*

![Product details](assets/product-details.png "Product details")

### Update product
*The Update Product window allows the user to update the individual product metrics. Dynamic validation is built into the form, ensuring data integrity and prompting users for any necessary corrections before submission.*

![Update product](assets/update-product.png "Update product")

### Add quantity
*The Add Quantity window allows the user to easily and quickly add a supplied quantity to the specific product.*

![Add quantity](assets/add-qty.png "Add quantity")

### Change warehouse
*The 'Change Warehouse' window facilitates the management of product sourcing within the system. It enables users to assign a different warehouse for a specific product with ease. The interface presents a searchable list of available warehouses, along with their types, allowing for a swift selection process. This feature ensures that the product inventory can be dynamically linked to different warehouses based on availability or strategic requirements.*

![Change warehouse](assets/change-warehouse.png "Change warehouse")
