using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Library_Api.Models;

namespace Library_Api.Services
{
    public class S3Service : IS3Service
    {
      
 

       public async Task<S3ResponseDto> DeleteFileAsync(string bookId, string bucketName, AwsCredentials awsCredentials)
        {
            S3ResponseDto response = new S3ResponseDto();
            var credentials = new BasicAWSCredentials(awsCredentials.AwsAcessKey, awsCredentials.AwsSecretKey);
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            try
            {

                using var client = new AmazonS3Client(credentials, config);

                await client.DeleteObjectAsync(bucketName, bookId);


                response.StatusCode = 200;
                response.Message = $"book cover with id {bookId} successfully deleted";
            }

            catch(AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;

            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<S3ResponseDto> UploadFileAsync(S3Object s3Object, AwsCredentials awsCredentials)
        {
            var credentials = new BasicAWSCredentials(awsCredentials.AwsAcessKey, awsCredentials.AwsSecretKey);
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var response = new S3ResponseDto();

            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3Object.InputStream,
                    Key = s3Object.Name,
                    BucketName = s3Object.BucketName,

                    CannedACL = S3CannedACL.NoACL
                };

                using var client = new AmazonS3Client(credentials, config);

                var transferUtility = new TransferUtility(client);


                // upload file to S3
                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 200;
                response.Message = $"{s3Object.Name} uploaded successfully";

            }
            catch (AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}
 
