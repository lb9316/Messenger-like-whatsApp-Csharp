/**
 * ******************************************************************************************************************
 * FileName		: LocationsOfUser.cs
 * 
 * Description	: This class stores the details of the user. 
 * @version		: LocationsOfUser.cs v 1.0 05/09/2015 10:15 PM
 *  
 * @author 		: lb9316 (Lakhan Bhojwani)
 
 * 
 * Revisions 	: Initial revision.
 * *******************************************************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCSharp
{
    class LocationsOfUser
    {
        /// <summary>
        /// Constructor of this class
        /// </summary>
        /// <param name="email">email of the user</param>
        /// <param name="name">name of the user</param>
        /// <param name="latitude"> latitude od the user</param>
        /// <param name="longitute"> longitude of the user</param>
        /// <param name="accuray"> accuracy of the map accuracy</param>
        /// <param name="update"> udate time</param>
        
          public LocationsOfUser(string email, string name, string latitude, string longitute, string accuray, string update)
          {
            this.email = email;
            this.full_name = name;
            this.latitude = latitude;
            this.longitude = longitude;
            this.accuracy = accuray;
            this.lastUpdated = update;


          }
        public string email { get; set; }
        public string full_name { get; set; }

        public string latitude { get; set; }
        public string longitude { get; set; }
        public string accuracy { get; set; }
        public string lastUpdated { get; set; }
    }
    
}
