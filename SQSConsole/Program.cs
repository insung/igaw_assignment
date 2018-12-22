using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AmazonSQSClient(RegionEndpoint.APNortheast2);
            GetUrl(client);
            SendMessage(client);
            //GetMessage(client);
            Console.WriteLine("Hello World!");
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
            req.QueueUrl = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";
            req.MessageBody = "test2";
            var response = client.SendMessageAsync(req).Result;
            Console.WriteLine("res: " + response.HttpStatusCode);
        }

        static void GetMessage(AmazonSQSClient client)
        {
            var req = new ReceiveMessageRequest();
            req.QueueUrl = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";
            var response = client.ReceiveMessageAsync(req).Result;

            if (response.Messages.Any())
            {
                foreach (var message in response.Messages)
                {
                    Console.WriteLine("msg " + message.Body);

                    var deleteReq = new DeleteMessageRequest();
                    deleteReq.QueueUrl = "https://sqs.ap-northeast-2.amazonaws.com/468917192189/EventCollectQueue";
                    deleteReq.ReceiptHandle = message.ReceiptHandle;

                    var result = client.DeleteMessageAsync(deleteReq).Result;
                    Console.WriteLine("del res: " + result.HttpStatusCode);
                }
            }
        }
    }
}
