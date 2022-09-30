using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Personal",Schema ="dbo")]
    public class Personal:IEntity
    {
        [Key]
        public long Id { get; set; }
        public int UserId { get; set; }
        public string Data { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Slug { get; set; }
        public bool Active { get; set; }
    }
}
