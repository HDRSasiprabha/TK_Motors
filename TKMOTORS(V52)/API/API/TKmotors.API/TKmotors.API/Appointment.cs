using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TKmotors.API
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class AppointmentController : ApiController
    {
        [AcceptVerbs("GET", "POST")]
        public string AddAppointment(AppointmentDomain appointment)
        {
            ConnectDB.openConnection();
            ConnectDB.cmd.CommandType = CommandType.Text;
            ConnectDB.sql = "insert into appointment([customername],[vehicleno],[phoneno],[appointmentdate]) values ( '" + appointment.CustomerName + "'  ,  '" + appointment.VehicleNo + "' , '" + appointment.TelephoneNo + "' , '" + appointment.AppointmentDate.ToString("MM/dd/yyyy") + "')";

            ConnectDB.cmd.CommandText = ConnectDB.sql;

            int line = ConnectDB.cmd.ExecuteNonQuery();
            if(line == 1)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }
    }

    public class AppointmentDomain
    {
        public string CustomerName { get; set; }
        public string VehicleNo { get; set; }
        public string TelephoneNo { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}