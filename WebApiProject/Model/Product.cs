using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiProject.Model
{
    public class Product
    {
        

        public int ID { get; set; }
        public string Name { get; set; }
        public short Price { get; set; }
        public short Quantity { get; set; }

        public string Img { get; set; }

        public string Category { get; set; }
        [JsonIgnore]
        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        [JsonIgnore]
        public virtual IdentityUser Owner { get; set; }
    }
}