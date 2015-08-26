/**
 * ******************************************************************************************************************
 * FileName		: UserDisplay.cs
 * 
 * Description	: This class Displays the user list and the locations of the users. 
 * @version		: UserDisplay.cs v 1.0 05/09/2015 10:15 PM
 *  
 * @author 		: lb9316 (Lakhan Bhojwani)
 
 * 
 * Revisions 	: Initial revision.
 * *******************************************************************************************************************
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WhatsAppCSharp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UsersDisplay : Page
    {
        // to change the zoom level
        static double ZoomLevel1=1;

       private BasicGeoposition rectangepos;
        private Geopoint point;
        private Rectangle rectangle;
       private BitmapImage img;
        Model modelObject = new Model();

        /// <summary>
        /// Initializing the event.
        /// </summary>
        public UsersDisplay()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        
        // when this page is loaded then this event is loaded
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await modelObject.getlistUsers();
            UserList.ItemsSource = modelObject.ObservableList;
        }

        /// <summary>
        /// This change the window to users chat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public  void GoToUserChat(object sender, TappedRoutedEventArgs e)
        {
            ListBoxItem newListBox = (ListBoxItem)sender;
            Model.bind_email = newListBox.Tag.ToString();
            
            //string a = newListBox.Content.ToString();
            
            // store the name of the user selected
            Model.bind_fullname = newListBox.Content.ToString();
          
            this.Frame.Navigate(typeof(ChatScreen));


        }

        /// <summary>
        /// This event is called when the map is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            // map center point
            myMap.Center = new Geopoint(new BasicGeoposition() { Altitude = 643, Latitude = 43.089863, Longitude = -77.669609 });
            // map zoom level
            myMap.ZoomLevel = 10;

           
            var loc = await modelObject.getLocations();


            var latandLong = JArray.Parse(loc);

            foreach (JObject root in latandLong)
            {

                try
                {

                    double userLatitude = Convert.ToDouble(root["latitude"].ToString());
                    double userLongitude = Convert.ToDouble(root["longitude"].ToString());
                    string lastUpdated = root["lastUpdated"].ToString();

                    if ((userLatitude > -90) && (userLatitude < 90) && (userLongitude > -180) && (userLongitude < 180) && !lastUpdated.Equals("0000-00-00 00:00:00"))
                    {
                        rectangepos = new BasicGeoposition() { Latitude = Convert.ToDouble(root["latitude"].ToString()), Longitude = Convert.ToDouble(root["longitude"].ToString()) };
                        rectangle = new Rectangle();
                        rectangle.Name = root["first_name"].ToString() + " " + root["last_name"].ToString() + "\n" + "Last Updated: " + root["lastUpdated"].ToString();
                        rectangle.Tag = root["email"].ToString();

                        point = new Geopoint(rectangepos);
                        rectangle.Width = 30;
                        rectangle.Height = 30;

                        img = new BitmapImage(new Uri("ms-appx:///Assets/redpin.png"));

                        rectangle.Fill = new ImageBrush() { ImageSource = img };
                        MapControl.SetLocation(rectangle, point);
                        MapControl.SetNormalizedAnchorPoint(rectangle, new Point(1.0, 0.5));


                        myMap.Children.Add(rectangle);

                        rectangle.AddHandler(UIElement.TappedEvent, new TappedEventHandler(myMap_Tapped), true);
                    }
                }
                catch (Exception ex)
                {
                    String x = "" + ex;
                    continue;
                }
            }
        }

        /// <summary>
        /// This event is called when the map is tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void myMap_Tapped(object sender, TappedRoutedEventArgs e)
        {
           Rectangle newfence = (Rectangle)sender;
           MessageDialog x = new MessageDialog(newfence.Name);
           Model.bind_email = newfence.Tag.ToString();


           Model.bind_fullname = newfence.Name.ToString();

            
            x.Commands.Add(new UICommand(
              "Message",
              new UICommandInvokedHandler(this.CommandInvokedHandler_Message)));

           x.Commands.Add(new UICommand(
            "Cancel",
            new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
          
            await x.ShowAsync();
        }

        private void CommandInvokedHandler_Message(IUICommand command)
        {
            
            this.Frame.Navigate(typeof(ChatScreen));

            
        }

        private void CommandInvokedHandler_Cancel(IUICommand command)
        {

        }

        /// <summary>
        /// This event sets the locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Set_Location_Click(object sender, RoutedEventArgs e)
        {
            double myLatitude = myMap.Center.Position.Latitude;
            double myLongitude = myMap.Center.Position.Longitude;
            string myLat = Convert.ToString(myLatitude);
            string myLong = Convert.ToString(myLongitude);

            await modelObject.setLocations(myLat, myLong);
            
            MessageDialog x = new MessageDialog("Location Updated");
            x.Commands.Add(new UICommand(
             "OK",
             new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
            await x.ShowAsync();

        }

        /// <summary>
        /// This adjust the zoom level of the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zoomout_Click(object sender, RoutedEventArgs e)
        {
            if (ZoomLevel1 > 0)
            {
                ZoomLevel1 = myMap.ZoomLevel;
                ZoomLevel1--;
                myMap.ZoomLevel = ZoomLevel1;
            }
        }

        /// <summary>
        /// This allows user to log out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void logout_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog x = new MessageDialog("Do you want to LogOut?");
            x.Commands.Add(new UICommand(
             "Yes",
             new UICommandInvokedHandler(this.CommandInvokedHandler_Yes)));

            x.Commands.Add(new UICommand(
             "No",
             new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));

            await x.ShowAsync();
           

        }

        private void CommandInvokedHandler_Yes(IUICommand command)
        {
            this.Frame.Navigate(typeof(PivotPage));
        }
        
    }
}
