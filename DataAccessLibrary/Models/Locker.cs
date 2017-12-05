using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public partial class Locker
    {
        [Key]
        public int Id { get; set; }
        public int? DeviceId { get; set; }
        public int? LockerId { get; set; }
        public int? CabinetId { get; set; }
        public string Name { get; set; }
        public bool? HasDevice { get; set; }

    }
}