using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using EventClassLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace SQSConsole
{
    class Program
    {
        private static readonly string QUEUE_URL = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";
        private static BasicAWSCredentials awsCredential = new BasicAWSCredentials("AKIAI3C6XGQ3AAONQK5Q", "zK//5PQtSfNzY6LJziCvDZW+N9wNNq08fQe/G0ti");

        static void Main(string[] args)
        {
            var client = new AmazonSQSClient(awsCredential, Amazon.RegionEndpoint.APNortheast2);
            //GetUrl(client);
            //for (int i=0; i< 10; i++)
            SendMessage(client);
            //GetMessage(client);

            Console.WriteLine("Hello SQS!");
        }

        static void GetUrl(AmazonSQSClient client)
        {
            var request = new GetQueueUrlRequest
            {
                QueueName = "EventCollectQueue",
                QueueOwnerAWSAccountId = "468917192189"
            };
            var response = client.GetQueueUrlAsync(request).Result;
            Console.WriteLine("res: " + response.QueueUrl);
        }

        static void SendMessage(AmazonSQSClient client)
        {
            string id = Guid.NewGuid().ToString();
            DOEvent ev = new DOEvent();
            DOParameter pa = new DOParameter();
            ev.event_id = id;
            ev.event_name = "purchase";
            ev.user_id = id;
            pa.order_id = id;
            pa.currency = "krw";
            pa.price = 300;
            ev.parameters = pa;

            var req = new SendMessageRequest();
            req.QueueUrl = QUEUE_URL;
            req.MessageBody = JsonConvert.SerializeObject(ev);
            var response = client.SendMessageAsync(req).Result;
            Console.WriteLine("status code: " + response.HttpStatusCode);
            Console.WriteLine("md5 body: " + response.MD5OfMessageBody);
            Console.WriteLine("msg id: " + response.MessageId);
            Console.WriteLine("msg length: " + response.ContentLength);
        }

        static void GetMessage(AmazonSQSClient client)
        {
            var req = new ReceiveMessageRequest();
            req.QueueUrl = QUEUE_URL;
            var response = client.ReceiveMessageAsync(req).Result;

            if (response.Messages.Any())
            {
                foreach (var message in response.Messages)
                {
                    Console.WriteLine("msg " + message.Body);

                    var deleteReq = new DeleteMessageRequest();
                    deleteReq.QueueUrl = QUEUE_URL;
                    deleteReq.ReceiptHandle = message.ReceiptHandle;

                    var result = client.DeleteMessageAsync(deleteReq).Result;
                    Console.WriteLine("del res: " + result.HttpStatusCode);
                }
            }
        }
    }
}
