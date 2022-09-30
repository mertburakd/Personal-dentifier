using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Entities.Models
{
    [Table("BaseValues", Schema = "dbo")]
    public class BaseValues : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public int Section { get; set; }
    }
}
