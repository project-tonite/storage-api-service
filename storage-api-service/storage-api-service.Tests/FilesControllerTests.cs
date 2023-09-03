using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using storage_api_service.Models;
using Newtonsoft.Json;

namespace storage_api_service.Tests
{
   

    [TestFixture]
    public class FilesControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            
            _factory = new WebApplicationFactory<Program>();

            _client = _factory.CreateClient();

        }
        [TearDown]
        public void TearDown()
        {
            // Dispose the resources
            _client.Dispose();
            _factory.Dispose();
        }
        [Test]
        public async Task UploadFile_ReturnsOkResult()
        {
            // Arrange: 
            var formData = new MultipartFormDataContent();
            formData.Add(new ByteArrayContent(new byte[] { 1, 2, 3 }), "file", "testfile.txt");
            formData.Add(new StringContent("1.0"), "Version");

            // Act: 
            var response = await _client.PostAsync("/api/files", formData);
            var contentString = await response.Content.ReadAsStringAsync();
            var fileEntity = JsonConvert.DeserializeObject<FileEntity>(contentString);


            // Assert:
            response.EnsureSuccessStatusCode();

            //checked the returned object
            Assert.IsNotNull(fileEntity); 
            Assert.IsFalse(string.IsNullOrEmpty(fileEntity.FileName));
            Assert.IsFalse(string.IsNullOrEmpty(fileEntity.FileName)); 
            Assert.IsNotNull(fileEntity.Content); 
            Assert.IsFalse(string.IsNullOrEmpty(fileEntity.Version)); 
            Assert.That(default(DateTime), Is.Not.EqualTo(fileEntity.CreatedAt));
            Assert.That(default(DateTime), Is.Not.EqualTo(fileEntity.UpdatedAt)); 

        }
    }
}
