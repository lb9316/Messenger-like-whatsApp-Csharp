/**
 * ******************************************************************************************************************
 * FileName		: WhatsAppUser.cs
 * 
 * Description	: This class Stores the  users on the server. 
 * @version		: WhatsAppUser.cs v 1.0 05/09/2015 10:15 PM
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
    class WhatsAppUser
    {
        /// <summary>
        /// This is the constructor.
        /// </summary>
        /// <param name="email">email id of the user</param>
        /// <param name="name"> name of the user </param>
        public WhatsAppUser(string email, string name)
        {
            this.email_id = email;
            this.full_name = name;
        }

        public string email_id { get; set; }

        public string full_name { get; set; }

    }
}
