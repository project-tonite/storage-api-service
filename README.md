# File Storage API

## Brief Description

The File Storage API is a simple yet powerful service that allows you to upload, manage, and version binary files. It provides endpoints to create, update, delete, and list files, making it easy to integrate file storage into your applications. This API is built using ASP.NET Core 6 and stores files in a database. A note of thanks to the team at Agorus for the Motivation. 

## Endpoints and Payloads

### Create File
- **Endpoint:** `POST /api/files`
- **Description:** Upload and create a new file.
- **Request Payload:**
  - `file` (Multipart Form Data): The binary file to upload.
  - `Version` (String, Optional): The version of the file.

### Get File by ID
- **Endpoint:** `GET /api/files/{id}`
- **Description:** Retrieve file details by its unique ID.
- **Response Payload:** A JSON object representing the file, including its name, content, version, and timestamps.

### List Files
- **Endpoint:** `GET /api/files`
- **Description:** List all files available in the storage.
- **Response Payload:** An array of JSON objects, each representing a file with details.

### Update File
- **Endpoint:** `PUT /api/files/{id}`
- **Description:** Update an existing file by its ID.
- **Request Payload:**
  - `file` (Multipart Form Data): The updated binary file.
  - `Version` (String, Optional): The new version of the file.
- **Response:** No content on success.

### Delete File
- **Endpoint:** `DELETE /api/files/{id}`
- **Description:** Delete a file by its ID.
- **Response:** No content on success.

## Instructions

1. **Create a New File:**
   - Use the `POST /api/files` endpoint to upload a file. Provide the file as a multipart form data field and include an optional version string if necessary.

2. **Retrieve File Details:**
   - Use the `GET /api/files/{id}` endpoint to get details of a file by specifying its ID in the URL.

3. **List All Files:**
   - Use the `GET /api/files` endpoint to retrieve a list of all files currently stored in the system.

4. **Update an Existing File:**
   - Use the `PUT /api/files/{id}` endpoint to update an existing file by providing a new file and an optional version.

5. **Delete a File:**
   - Use the `DELETE /api/files/{id}` endpoint to delete a file by specifying its ID in the URL.

For development and testing, you can use tools like `curl`, `Postman`, or your favorite programming language to interact with the API.

**Note:** If you prefer, you can also make use of the **uploadTestUI.html** file located within the test project. This file serves as a dummy UI for uploading physical files to the endpoint. Just remember to update the `action` attribute in the form to reflect the URL where your API is hosted/running.

## Sample API Usage

Here is a sample `curl` command to create a new file:

```bash
curl -X POST 'https://your-api-url/api/files' \
  -H 'Content-Type: multipart/form-data' \
  -F 'file=@/path/to/your/file.jpg' \
  -F 'Version=1.0'

Contributors
## Anthony Nicolini
Feel free to contribute to this project or report issues by creating a pull request or raising an issue on GitHub.

Happy coding!
