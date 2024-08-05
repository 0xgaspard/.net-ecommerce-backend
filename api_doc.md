# E-commerce Backend API Documentation



## Authentication Endpoints



### Register



**Endpoint:** `POST /api/auth/register`



**Request:**



```json

{

  "username": "string",

  "email": "string",

  "password": "string",

}

```



**Response:**



```json

{

  "message": "User registered successfully"

}

```



### Login



**Endpoint:** `POST /api/auth/login`



**Request:**



```json

{

  "email": "string",

  "password": "string"

}

```



**Response:**



```json

{

  "token": "JWT token"

}

```



### Logout



**Endpoint:** `GET /api/auth/logout`







**Response:**



```json

{

  "message": "User logged out successfully"

}

```

### Get Recovery Key

**Endpoint** `GET /api/auth/recoverykey`


**Response**

```json
{
  "recoveryKey": "string"
}
```

### Forget Password

**Endpoint** `POST /api/auth/forgetPassword`

**Request**
```json
{

  "email": "string",

  "recoveryKey": "string",

  "newPassword": "string",

}
```

**Response**

```json
{

  "message": "password updated successfully"

}
```
## Admin Endpoints



### Category Management



#### Create Category



**Endpoint:** `POST /api/admin/categories`



**Request:**



```json

{

  "name": "string",

  "description": "string"

}

```



**Response:**



```json

{

  "message": "Category created successfully"

}

```



#### List Categories



**Endpoint:** `GET /api/admin/categories`



**Response:**



```json

[

  {

    "id": "integer",

    "name": "string",

    "description": "string"

  }

]

```



#### Update Category



**Endpoint:** `PUT /api/admin/categories/:id`



**Request:**



```json

{

  "name": "string",

  "description": "string"

}

```



**Response:**



```json

{

  "message": "Category updated successfully"

}

```



#### Delete Category



**Endpoint:** `DELETE /api/admin/categories/:id`



**Response:**



```json

{

  "message": "Category deleted successfully"

}

```


### Product Management



#### Create Product



**Endpoint:** `POST /api/admin/products`



**Request:**



```json

{

  "name": "string",

  "description": "string",

  "imgUrl": "string",

  "quantity": "number",

  "price": "number",

  "categoryId": "integer"

}

```



**Response:**



```json

{

  "message": "Product created successfully"

}

```



#### List Products



**Endpoint:** `GET /api/admin/products`



**Response:**



```json

[

  {

    "id": "integer",

    "name": "string",

    "description": "string",

    "imgUrl": "string",

    "quantity": "number",

    "price": "number",

    "categoryId": "integer"

  }

]

```



#### Update Product



**Endpoint:** `PUT /api/admin/products/:id`



**Request:**



```json

{

  "name": "string",

  "description": "string",

  "quantity": "string",

  "imagUrl": "string",

  "price": "number",

  "categoryId": "integer"

}

```



**Response:**



```json

{

  "message": "Product updated successfully"

}

```



#### Delete Product



**Endpoint:** `DELETE /api/admin/products/:id`



**Response:**



```json

{

  "message": "Product deleted successfully"

}

```



### Order Management



#### List Orders



**Endpoint:** `GET /api/admin/orders`



**Response:**



```json

[

  {

    "id": "integer",

    "clientId": "integer",

    "productList": [

      {

        "productId": "integer",

        "quantity": "integer",

        "totalAmount": "number",

      }

    ],

    "status": "string"

  }

]

```



#### Update Order Status



**Endpoint:** `PUT /api/admin/orders/:id/status`



**Request:**



```json

{

  "status": "string"

}

```



**Response:**



```json

{

  "message": "Order status updated successfully"

}

```

### Analytics

#### Sales Amount

**EndPoint** `GET/ api/admin/sales`

**Example Queary** `https://yourdomain.com/api/admin/analytics/sales?startDate=2023-01-01&endDate=2023-12-31`

**Response**

```json
  [
    {
      "id": "integer",
      
      "name": "string",

      "description": "string",

      "price": "number",

      "imgUrl": "string",

      "salesAmount": "number",

      "category": "string"
    }
  ]

```



## Client Endpoints

## General Endpoints

### Product Browsing



#### List Products



**Endpoint:** `GET /api/products`

**Example query** `GET https://yourdomain.com/api/products?page=2&limit=20&category=categoryName`

**Response:**



```json

[

  {

    "id": "integer",

    "name": "string",

    "description": "string",

    "price": "number",

    "categoryId": "integer"

  }

]

```



#### Get Product by ID



**Endpoint:** `GET /api/products/:id`



**Response:**



```json

{

  "id": "integer",

  "name": "string",

  "description": "string",

  "price": "number",

  "categoryId": "integer"

}

```



#### List Categories



**Endpoint:** `GET /api/categories`



**Response:**



```json

[

  {

    "id": "integer",

    "name": "string",

    "description": "string"

  }

]

```


## Client Endpoints

### Shopping Cart (Bucket)


#### Add Product to Cart



**Endpoint:** `POST /api/cart`



**Request:**



```json

{

  "productId": "integer",

  "quantity": "integer"

}

```



**Response:**



```json

{

  "message": "Product added to cart"

}

```



#### View Cart



**Endpoint:** `GET /api/cart`



**Response:**



```json

[

  {

    "productId": "integer",

    "quantity": "integer"

  }

]

```



#### Remove Product from Cart



**Endpoint:** `DELETE /api/cart/:productId`



**Response:**



```json

{

  "message": "Product removed from cart"

}

```



### Purchase



#### Place Order



**Endpoint:** `POST /api/orders`



**Request:**



```json

{

  "productList": [

    {

      "productId": "integer",

      "quantity": "integer"

    }

  ],

  "totalAmount": "number",

  "paymentMethod": "string"

}

```



**Response:**



```json

{

  "message": "Order placed successfully"

}

```



#### View Order by ID



**Endpoint:** `GET /api/orders/:id`



**Response:**



```json

{

  "id": "integer",

  "clientId": "integer",

  "productList": [

    {

      "productId": "integer",

      "quantity": "integer"

    }

  ],

  "totalAmount": "number",

  "status": "string"

}

```



## General Features



### User Profile



#### View Profile



**Endpoint:** `GET /api/profile`



**Response:**



```json

{

  "id": "integer",

  "username": "string",

  "email": "string",

  "role": "admin" | "client"

}

```



#### Update Profile



**Endpoint:** `PUT /api/profile`



**Request:**



```json

{

  "username": "string",

  "email": "string",

  "password": "string"

}

```



**Response:**



```json

{

  "message": "Profile updated successfully"

}

```



### Reviews and Ratings



#### Add Review



**Endpoint:** `POST /api/reviews`



**Request:**



```json

{

  "productId": "integer",

  "rating": "number",

  "comment": "string"

}

```



**Response:**



```json

{

  "message": "Review added successfully"

}

```



#### Get Reviews by Product ID



**Endpoint:** `GET /api/reviews/:productId`



**Response:**



```json

[

  {

    "id": "integer",

    "productId": "integer",

    "rating": "number",

    "comment": "string",

    "clientId": "integer"

  }

]

```