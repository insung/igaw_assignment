using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Linq;

namespace SQSConsole
{
    class Program
    {
        private static readonly string QUEUE_URL = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";

        static void Main(string[] args)
        {
            var client = new AmazonSQSClient(RegionEndpoint.APNortheast2);
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
            var req = new SendMessageRequest();
            req.QueueUrl = QUEUE_URL;
            req.MessageBody = "test2";
            var response = client.SendMessageAsync(req).Result;
            Console.WriteLine("res: " + response.HttpStatusCode);
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
