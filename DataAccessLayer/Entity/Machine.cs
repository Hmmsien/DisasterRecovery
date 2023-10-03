using System;
namespace DataAccessLayer.Entity
{
	public class Machine
	{
        public string MachineCode { get; set; }

        public int HrsUsed { get; set; }

        public int TotalAmount { get; set; }
    }
}
