using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Model.Device
{
   public class DeviceViewModel
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public bool Status { get; set; }
        public int? LockerId { get; set; }
    }
}
