using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class IdGenerator
{
    public IdGenerator()
    {
    }

    public string GetRandomTransmissionId()
    {
        List<char> letList = new List<char>();
        letList.Add('A');
        letList.Add('B');
        letList.Add('C');
        letList.Add('D');
        letList.Add('E');
        letList.Add('F');
        letList.Add('0');
        letList.Add('1');
        letList.Add('2');
        letList.Add('3');
        letList.Add('4');
        letList.Add('5');
        letList.Add('6');
        letList.Add('7');
        letList.Add('8');
        letList.Add('9');

        Random random = new Random();

        string transmissionID = "";
        string temp = "";

        for (int k = 0; k < 4; k++)
        {
            for (int i = 0; i < 4; i++)
            {
                temp = temp + letList[random.Next(0, letList.Count())];
            }
            transmissionID = transmissionID + temp + "-";
            temp = "";
        }
        for (int i = 0; i < 4; i++)
        {
            temp = temp + letList[random.Next(0, letList.Count())];
        }
        transmissionID = transmissionID + temp;

        return transmissionID;
    }
    public string GetRandomIPAddress()
    {
        string ipAddress = String.Empty;
        Random rand = new Random();

        ipAddress = "192.168.1." + rand.Next(2, 255).ToString();

        return ipAddress;
    }
    public string GetRandomDouble(double magnitude)
    {
        Random random = new Random();
        double value = Math.Round(magnitude * random.NextDouble(), 3);
        return value.ToString();
    }
}
