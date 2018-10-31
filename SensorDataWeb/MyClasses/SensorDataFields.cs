using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SensorDataFields
{
    public int Id { get; set; }
    public string UniqueId { get; set; }
    public string TransmissionId { get; set; }
    public string IpAddress { get; set; }
    public DateTime date { get; set; }

    public SensorDataFields()
    {
    }
}