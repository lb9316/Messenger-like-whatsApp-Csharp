/**
 * ******************************************************************************************************************
 * FileName		: Model.cs
 * 
 * Description	: This class is the backend of the application which is connected to the user. 
 * @version		: Model.cs v 1.0 05/09/2015 10:15 PM
 *  
 * @author 		: lb9316 (Lakhan Bhojwani)
 
 * 
 * Revisions 	: Initial revision.
 * *******************************************************************************************************************
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCSharp
{
    class Model
    {
        //logged in user's email
        public static string finalemail;


        //logged in user's password
        public static string finalpassword;

        //The email of the selected friend 
        public static string bind_email;

        //The name of the selected friend
        public static string bind_fullname;

        // list of users
        public ObservableCollection<WhatsAppUser> ObservableList = new ObservableCollection<WhatsAppUser>();

        // list of chats
        public ObservableCollection<Chatting> ObservableMessages = new ObservableCollection<Chatting>();

        // list of locations
        public ObservableCollection<LocationsOfUser> ObservableLocations = new ObservableCollection<LocationsOfUser>();

        // server
        public static string URL = "http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php";

        /// <summary>
        /// This method validates the user login details.
        /// </summary>
        /// <param name="username"> username of user</param>
        /// <param name="password">password of user </param>
        /// <returns></returns>
        public async Task<string> validate(string username, string password)
        {

            finalemail = username;
            finalpassword = password;




            if (!password.Equals(""))
            {
                var value = await listUsers(username, password);
                if (value.ToString().Equals("Invalid user"))
                {
                    return "Invalid user";
                }
                else if (value.ToString().Equals("Invalid"))
                {
                    return "No Internet Connection";
                }
                else
                {
                    return "Successful";
                }
            }

            return "Invalid User";
        }

        /// <summary>
        /// getting and storing the list of users
        /// </summary>
        /// <returns> Task </returns>
        public async Task getlistUsers()
        {

            // getting the list of users
            var list = await listUsers(finalemail, finalpassword);

            // Converting to JArray
            var splitdata = JArray.Parse(list);
            foreach (JObject temp in splitdata)
            {
                if (temp["email"].ToString() != finalemail)
                {
                    var ul = new WhatsAppUser(temp["email"].ToString(), temp["first_name"].ToString() + " " + temp["last_name"].ToString());
                    ObservableList.Add(ul);
                }
            }
        }

        /// <summary>
        /// This returns the list of the users.
        /// </summary>
        /// <param name="username">username of users </param>
        /// <param name="password">password of the user </param>
        /// <returns></returns>
        private async Task<string> listUsers(string username, string password)
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","getUsers"),
                     new KeyValuePair<string,string>("email",username),
                       new KeyValuePair<string,string>("password",password)
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;
                return "Invalid";
            }
        }

        /// <summary>
        /// This returns the users list.
        /// </summary>
        /// <param name="email">email of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns></returns>
        public async Task<string> getUsers(string email, string password)
        {

            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","getUsers"),
                     new KeyValuePair<string,string>("email",email),
                       new KeyValuePair<string,string>("password",password),
                       
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;
                return "Invalid";
            }

        }
        /// <summary>
        /// This method helps user to sign up
        /// </summary>
        /// <param name="firstname"> users first name</param>
        /// <param name="lastname">users last name</param>
        /// <param name="email">users email id</param>
        /// <param name="password">users password </param>
        /// <returns></returns>
        public async Task<string> SignUp(string firstname, string lastname, string email, string password)
        {
            finalemail = email;
            finalpassword = password;
            if (!firstname.Equals("") && !lastname.Equals("") && !email.Equals("") && !password.Equals(""))
            {
                if (email.Contains("@"))
                {
                    var value = await registerUsers(firstname, lastname, email, password);
                    if (value.ToString().Equals("Account already exists"))
                    {
                        return "Try different Email ID";
                    }
                    else if (value.ToString().Equals("Account Created"))
                    {
                        return "Successful";
                    }
                }
            }
            return "Invalid";
        }
        /// <summary>
        /// This method deletes the user from the server
        /// </summary>
        /// <param name="email">email of user </param>
        /// <param name="password">password of user</param>
        /// <returns></returns>
        public async Task<string> Delete(string email, string password)
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","deleteAccount"),
                     new KeyValuePair<string,string>("email",email),
                       new KeyValuePair<string,string>("password",password)};
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;
                return "Invalid";
            }

        }
        /// <summary>
        /// This registers the user.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<string> registerUsers(string firstname, string lastname, string email, string password)
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","createAccount"),
                     new KeyValuePair<string,string>("email",email),
                       new KeyValuePair<string,string>("password",password),
                        new KeyValuePair<string,string>("first_name",firstname),
                         new KeyValuePair<string,string>("last_name",lastname)
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;
                return "Invalid";
            }
        }



        /// <summary>
        /// This get messages between the 2 users 
        /// </summary>
        /// <returns></returns>
        public async Task get()
        {
            var Messages = await get_messages();

            if (!Messages.ToString().Equals("Invalid"))
            {


                var result = JArray.Parse(Messages);
                foreach (JObject temp in result)
                {

                    Chatting chat = new Chatting(temp["message"].ToString(), temp["msg_type"].ToString(), temp["email"].ToString(), temp["first_name"].ToString());
                    ObservableMessages.Add(chat);

                }
            }
        }

        /// <summary>
        /// Sending the massages
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task send(string text)
        {
            await send_messages(text);
        }
        public async Task<string> get_messages()
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","getMessages"),
                     new KeyValuePair<string,string>("email",finalemail),
                       new KeyValuePair<string,string>("password",finalpassword),
                          
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;
                return "Invalid";
            }
        }


        public async Task<string> getLocations()
        {
            return await Locations();


        }

        /// <summary>
        /// Getting the locations of the users.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Locations()
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","getLocations"),
                     new KeyValuePair<string,string>("email",finalemail),
                       new KeyValuePair<string,string>("password",finalpassword),
                          
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;
                return "Invalid";
            }
        }

        /// <summary>
        /// setting the locations
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="mylong"></param>
        /// <returns></returns>
        public async Task setLocations(string lat, string mylong)
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","setLocation"),
                     new KeyValuePair<string,string>("email",finalemail),
                       new KeyValuePair<string,string>("password",finalpassword),

                       new KeyValuePair<string,string>("lat",lat),
                       new KeyValuePair<string,string>("long",mylong),
                       new KeyValuePair<string,string>("acc","6"),


   
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                String x = "" + e;

            }
        }

        /// <summary>
        /// querying the server
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task send_messages(string text)
        {
            try
            {
                var c = new HttpClient();
                var result = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("command","sendMessage"),
                     new KeyValuePair<string,string>("email",finalemail),
                       new KeyValuePair<string,string>("password",finalpassword),
                          new KeyValuePair<string,string>("to",bind_email),
                           new KeyValuePair<string,string>("message",text),
                };
                c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpResponseMessage response = await c.PostAsync(URL, new FormUrlEncodedContent(result));
                response.EnsureSuccessStatusCode();

            }
            catch (Exception e)
            {
                String x = "" + e;
            }
        }




    }
}

