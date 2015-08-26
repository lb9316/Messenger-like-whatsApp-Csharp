using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WhatsAppCSharp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatScreen : Page
    {
        Model modelObject = new Model();
        ObservableCollection<Chatting> perticularScreen = new ObservableCollection<Chatting>();
        public ChatScreen()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await modelObject.get();

            foreach (Chatting chat in modelObject.ObservableMessages)
            {
                if (chat.email.Equals(Model.bind_email))
                {
                    perticularScreen.Add(chat);
                }
            }
            nameProp.Text = Model.bind_fullname;
            list1.ItemsSource = perticularScreen;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = messageto.Text;
            await modelObject.send(text);
            messageto.Text = " ";
            this.Frame.Navigate(typeof(ChatScreen));
        

        }

        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UsersDisplay));
        }

        private void list1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
