using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public partial class Device
    {
        [Key]
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public bool Status { get; set; }
        public int? LockerId { get; set; }
    }
}
