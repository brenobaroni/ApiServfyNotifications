using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ApiServfyNotifications.Domain.Entities
{
    public class EmailConfigurations
    {
        [Key]
        public ObjectId _id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
