using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SensorDataWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        // GET: api/Sensor
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sensor/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sensor
        [HttpPost]
        //public void Post([FromBody] string value)
        public void Post()
        {
            HttpRequest req = Request;                      //Represents the REST request
            StreamReader sr = new StreamReader(req.Body);   //plain-text must get data from Request.Body
            string sensData = sr.ReadToEnd();               //Read entire Request.Body data stream

            ///////////////////////////////////
            //TODO Handle input from hardware//
            ///////////////////////////////////

            //Make connection to my database within SQL server on my laptop with a login
            SqlServerConnHelp sqlConnManager = new SqlServerConnHelp();

            //Use to connect to server
            //SqlConnection sqlConnection = sqlConnManager.GetConnection("SensorData.tk", "SensorData", "jdw", "asdf");
            
            //Use to connect to my laptop
            SqlConnection sqlConnection = sqlConnManager.GetConnection(@"LAPTOP-AD2UF7HI\SQLEXPRESS", "SensorData", "jdw", "asdf");

            //Used to insert data into the first database *SensorData*
            //Insert data:: Insert Into Table (Column A, Column B) Values (Value A, Value B)
            string query = "Insert Into SensorData (UniqueID, Transmitter_ID, IP_Address) Values (@UniqueID, @Transmitter_ID, @IP_Address)";
            SqlCommand cmd = new SqlCommand(query, sqlConnection); //Create a new command for database w/ string and connection
            IdGenerator idGen = new IdGenerator();                 //Create auto generating variable
            string uid = idGen.GetRandomTransmissionId();

            //Fill out command with parameters
            cmd.Parameters.AddWithValue("@UniqueID", uid); 
            cmd.Parameters.AddWithValue("@Transmitter_ID", "1111-FFFF");
            cmd.Parameters.AddWithValue("@IP_Address", idGen.GetRandomIPAddress());

            //Send command to database
            cmd.ExecuteNonQuery();  

            //Used to insert data into the second database *SensorDataDetail*
            for (int i = 0; i < 3; i++)
            {
                query = "Insert Into SensorDataDetail (UniqueID, SensorNode, SensorValue) Values (@UniqueID, @SensorNode, @SensorValue)";
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@UniqueID", uid); //Fillout command with paramaters
                cmd.Parameters.AddWithValue("@SensorValue", idGen.GetRandomDouble(100));

                switch (i)
                {
                    case 0:
                        cmd.Parameters.AddWithValue("@SensorNode", "Temperature");
                        break;
                    case 1:
                        cmd.Parameters.AddWithValue("@SensorNode", "Moisture");
                        break;
                    case 2:
                        cmd.Parameters.AddWithValue("@SensorNode", "Sunlight");
                        break;
                }

                //Send command to database
                cmd.ExecuteNonQuery();  
            }

            cmd.Dispose();  //clean up
            sqlConnection.Close();  //close connection to database
            sqlConnection.Dispose();    //clean up
        }

        // PUT: api/Sensor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}