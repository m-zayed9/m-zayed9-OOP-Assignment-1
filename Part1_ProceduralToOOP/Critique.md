# Critique of `order_system.cpp`

## 1. Too Many Global Variables
All the data is stored in global variables, so any function can access and change it. This makes the code harder to understand and maintain.

## 2. Using Arrays
Customer and product information is stored in separate arrays. Using classes would keep related data together and make the code easier to organize.

## 3. Using Indexes to Connect Data
Orders and products are connected using array indexes. This makes the code harder to follow and maintain when the arrays change.

## 4. Fixed Array Sizes
The program uses fixed-size arrays with maximum limits. This makes it difficult to add more customers, products, or orders.

## 5. Too Many Responsibilities in One Place
The code mixes data management, program operations, and console output. Separating these responsibilities into classes and methods would make the code easier to read and reuse.