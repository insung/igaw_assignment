using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using EventClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventCollectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectController : ControllerBase
    {
        private static readonly string SQS_QUEUE_URL = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";
        private static BasicAWSCredentials awsCredential = new BasicAWSCredentials("AKIAI3C6XGQ3AAONQK5Q", "zK//5PQtSfNzY6LJziCvDZW+N9wNNq08fQe/G0ti");

        public CollectController()
        {
        }

        [HttpPost]
        public IActionResult PostEvent(DOEvent e)
        {
            AmazonSQSClient SQS_Client = new AmazonSQSClient(awsCredential, RegionEndpoint.APNortheast2);
            var request = new SendMessageRequest();
            request.QueueUrl = SQS_QUEUE_URL;
            request.MessageBody = JsonConvert.SerializeObject(e);
            var response = SQS_Client.SendMessageAsync(request).Result;

            if ((int)response.HttpStatusCode >= 200 && (int)response.HttpStatusCode <= 299)
            {
                return Ok(new { is_success = true });
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}