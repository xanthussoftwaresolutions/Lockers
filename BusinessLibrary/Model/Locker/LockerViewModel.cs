using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Model.Locker
{
   public class LockerViewModel
    {
        public int Id { get; set; }
        public int? DeviceId { get; set; }
        public int? LockerId { get; set; }
        public int? CabinetId { get; set; }
        public string Name { get; set; }
        public bool? HasDevice { get; set; }
    }
}
