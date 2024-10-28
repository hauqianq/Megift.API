using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PhotosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("upload-product")]
        public async Task<IActionResult> UploadImagesForProduct(List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            string firebaseBucket = _configuration["FirebaseConfiguration:StorageBucket"];
            var firebaseStorage = new FirebaseStorage(firebaseBucket);
            var uploadResults = new List<object>();

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    string filename = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var task = firebaseStorage.Child("Images").Child(filename);
                    using (var stream = image.OpenReadStream())
                    {
                        await task.PutAsync(stream);
                    }
                    string downloadUrl = await task.GetDownloadUrlAsync();
                    uploadResults.Add(downloadUrl);
                }
            }

            return Ok(uploadResults);
        }


        [HttpPost("upload-koifish")]
        public async Task<IActionResult> UploadImagesForKoiFish(List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            string firebaseBucket = _configuration["FirebaseConfiguration:StorageBucket"];
            var firebaseStorage = new FirebaseStorage(firebaseBucket);
            var uploadResults = new List<object>();

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    string filename = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var task = firebaseStorage.Child("KoiFishes").Child(filename);
                    using (var stream = image.OpenReadStream())
                    {
                        await task.PutAsync(stream);
                    }
                    string downloadUrl = await task.GetDownloadUrlAsync();
                    uploadResults.Add(downloadUrl);
                }
            }

            return Ok(uploadResults);
        }
    }
}
