# E-commerce Backend API Documentation


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

**Endpoint** `POST /api/auth/forget-password`

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


#### Send Email Verification



**Endpoint:** `POST /api/emailverification/send`



**Request:**



```json

{

  "email": "string",

}

```



**Response:**



```json

{

  "message": "Verification email sent"

}

```


#### Verify Email



**Endpoint:** `GET /api/emailverification/verify?email={email}&token={token}`



**Response:**



```json

{

  "message": "Email verified successfully. You can log in now."

}

```