/**
 * ******************************************************************************************************************
 * FileName		: Chatting.cs
 * 
 * Description	: This class Stores the messages between the user and the person he has selected. 
 * @version		: Chatting.cs v 1.0 05/09/2015 10:15 PM
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
    class Chatting
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> messages between the two </param>
        /// <param name="msg_type"> Type of msg </param>
        /// <param name="email">Email of the user </param>
        public Chatting(string message, string msg_type, string email , string name)
        {
            // assigning values
          
            if (msg_type.Equals("to"))
            {
                this.message ="ME:   " +message+" \n ";
                
            }
            else
            {
                this.message = name.ToUpper()+ ":   " + message + " \n ";
            }

            this.email = email;
        }
        public string message { get; set; }
        public string msg_type { get; set; }
        public string email { get; set; }

    }
}
