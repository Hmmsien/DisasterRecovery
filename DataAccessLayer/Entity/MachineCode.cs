using System;
namespace DataAccessLayer.Entity
{
	public class MachineCode
	{
        public int MachineCodeID { get; set; }

        public string MachineCodeName { get; set; }

        public string Description { get; set; }

        public double UsageRate { get; set; }

        public double MaxHourPerDay { get; set; }
    }
}
