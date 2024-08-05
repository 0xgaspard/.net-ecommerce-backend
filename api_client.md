# E-commerce Backend API Documentation


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


#### List Products by Category



**Endpoint:** `GET /api/products/category/:id`



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



#### List Categories



**Endpoint:** `GET /api/categories`



**Response:**



```json

[

  {

    "id": "integer",

    "name": "string"

  }

]

```

#### List SubCategories


**Endpoint:** `GET /api/categories/:id`



**Response:**



```json

[

  {

    "id": "integer",

    "name": "string"

  }

]

```


#### List All Categories



**Endpoint:** `GET /api/categories/all`



**Example Response:**



```json

[
    {
        "id": 1,
        "name": "Electronics",
        "subcategories": [
            {
                "id": 2,
                "name": "Laptops",
                "subcategories": [
                    {
                        "id": 3,
                        "name": "Gaming Laptops",
                        "subcategories": []
                    },
                    {
                        "id": 4,
                        "name": "Ultrabooks",
                        "subcategories": []
                    }
                ]
            },
            {
                "id": 5,
                "name": "Smartphones",
                "subcategories": [
                    {
                        "id": 6,
                        "name": "Android",
                        "subcategories": []
                    },
                    {
                        "id": 7,
                        "name": "iOS",
                        "subcategories": []
                    }
                ]
            }
        ]
    },
    {
        "id": 8,
        "name": "Home Appliances",
        "subcategories": [
            {
                "id": 9,
                "name": "Refrigerators",
                "subcategories": []
            },
            {
                "id": 10,
                "name": "Washing Machines",
                "subcategories": []
            }
        ]
    }
]

```

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


