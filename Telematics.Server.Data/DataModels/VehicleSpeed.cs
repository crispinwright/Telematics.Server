//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Telematics.Server.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class VehicleSpeed
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int VehicleID { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lon { get; set; }
        public Nullable<long> Speed { get; set; }
        public byte[] EventTime { get; set; }
        public Nullable<System.DateTime> UTCTime { get; set; }
        public string DeviceID { get; set; }
    
        public virtual User User { get; set; }
        public virtual UserVehicle UserVehicle { get; set; }
    }
}
