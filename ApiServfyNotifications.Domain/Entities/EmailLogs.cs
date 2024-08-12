using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Domain.Entities
{
    public class EmailLogs
    {
        [Key]
        public ObjectId _id { get; set; }
        public bool success { get; set; }
        public string data { get; set; }
        public string emailService { get; set;}
    }
}
