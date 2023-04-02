using Library_Api.Models;

namespace Library_Api.Services
{
    public interface IS3Service
    {

        /*
         * Uploads a file to an S3 bucket
         */
        public Task<S3ResponseDto> UploadFileAsync(S3Object s3Object, AwsCredentials awsCredentials);

         public Task<S3ResponseDto> DeleteFileAsync(string bookId, string bucketName, AwsCredentials awsCredentials);
 
    }
}
