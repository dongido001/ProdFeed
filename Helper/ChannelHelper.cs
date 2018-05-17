
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace ProdFeed.Helpers
{
    public class Channel
    {
        public static async Task<IActionResult> Trigger(object data, string channelName, string eventName)
        {
            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };
            var pusher = new Pusher(
              "413866",
              "9c81928de3b4f4c19599",
              "7628a2032bf75ec2953d",
              options
            );
        
            var result = await pusher.TriggerAsync(
              channelName,
              eventName,
              data
            );
            return new OkObjectResult(data);
        }
    }
}