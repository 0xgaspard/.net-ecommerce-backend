# E-commerce Backend API Documentation


## Admin Endpoints



### Category Management



#### Create Category



**Endpoint:** `POST /api/admin/categories`



**Request:**



```json

{

  "name": "string",
  
  "parentId?": "string|null"

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

  }

]

```


#### List SubCategories



**Endpoint:** `GET /api/admin/categories/:id`



**Response:**



```json

[

  {

    "id": "integer",

    "name": "string",

  }

]

```


#### List All Categories



**Endpoint:** `GET /api/admin/categories/all`



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



#### List Products by Category



**Endpoint:** `GET /api/admin/products/category/:id`



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
