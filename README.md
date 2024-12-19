<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

# EA Product Catalog Service

This project is a microservice API developed using **.NET Core** as part of the **EA eCommerce** system. It is responsible for managing the product catalog of the application. The service utilizes **MongoDB** for data storage, **Redis** for distributed caching, and **pagination** for efficient data retrieval. It follows the principles of **Clean Architecture** with a 3-layered architecture, employing **Domain-Driven Design** (DDD) principles.

In addition to these features, the service also handles image storage through **Azure Blob Storage**, storing only the image ID in the database rather than the image itself. This ensures a lightweight database while offloading media management to the cloud, improving scalability and performance.

## 🛠️ Technologies Used

- **.NET Core** (C#)
- **MongoDB**: For storing product catalog data.
- **Redis**: For distributed caching.
- **Azure Blob Storage**: For storing product images, with only the image ID stored in the database.
- **Clean Architecture**: The project follows the **Clean Architecture** principles for better maintainability and separation of concerns.

## 📂 Image Storage Integration with Azure Blob Storage

One of the key features of this service is the integration with **Azure Blob Storage** for storing images associated with the products. Instead of storing the image files directly in the database, the image files are uploaded to Azure Blob Storage, and only the image **ID** is saved in the MongoDB database. This provides several benefits:

- **Scalability**: Offloading image storage to Azure Blob Storage allows the database to remain lightweight and scalable.
- **Performance**: Accessing images from Blob Storage is more efficient and faster than loading them directly from a database.
- **Cost Efficiency**: Storing large binary files in Blob Storage reduces database storage costs.

### Example: Storing Product Image in Azure Blob Storage

Here’s how the image storage and retrieval works:

1. **Upload Image to Blob Storage**: When a product image is uploaded, it is saved to Azure Blob Storage, and the image’s unique ID is saved in the database.

2. **Retrieving Image**: When needed, the product image can be retrieved from Azure Blob Storage using the stored image ID.

```csharp
// Example code for uploading image to Azure Blob Storage
public async Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
{
            var containerClient = blob.GetBlobContainerClient(CONTAINER_NAME);

            var fileId = Guid.NewGuid();
            var blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

            return fileId;
}
