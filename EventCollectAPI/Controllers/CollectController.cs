using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using EventCollectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventCollectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectController : ControllerBase
    {
        private readonly string SQS_QUEUE_URL = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";

        public CollectController()
        {
        }

        [HttpPost]
        public IActionResult PostEvent(IGAWEvent e)
        {
            AmazonSQSClient SQS_Client = new AmazonSQSClient(RegionEndpoint.APNortheast2);
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