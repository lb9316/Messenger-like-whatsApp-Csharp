/**
 * ******************************************************************************************************************
 * FileName		: PivotPAge.xaml.cs
 * 
 * Description	: This project is created using mvvm design pattern.This the start screen for the application where 
 *                here is 1 pivot for logIn, SignUp and delete.
 *                
 * @version		: PivotPage.xaml.cs v 1.0 05/09/2015 10:15 PM
 *  
 * @author 		: lb9316 (Lakhan Bhojwani)
 
 * 
 * Revisions 	: Initial revision.
 * *******************************************************************************************************************
 */
using WhatsAppCSharp.Common;
using WhatsAppCSharp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;



namespace WhatsAppCSharp
{
    public sealed partial class PivotPage : Page
    {
        Model modelObject = new Model();
        private const string FirstGroupName = "FirstGroup";
        private const string SecondGroupName = "SecondGroup";


        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        /// <summary>
        /// Initializing the components of this page
        /// </summary>
        public PivotPage()
        {
            this.InitializeComponent();


        }

        /// <summary>
        /// This method is called when user click on the login button.
        /// </summary>
        /// <param name="sender"> button event </param>
        /// <param name="e"> Route </param>
        private async void loginClick(object sender, RoutedEventArgs e)
        {
            // call the method to validate the user. 
            var reply = await modelObject.validate(getEmail.Text, getPassword.Password);
            if (reply.Equals("Successful"))
            {

                // display the message
                var x = new MessageDialog(reply.ToString());
                x.Commands.Add(new UICommand(
                 "OK",
              new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
                await x.ShowAsync(); this.Frame.Navigate(typeof(UsersDisplay));



            }
            // If user is not validate 
            else
            {
                var error = new MessageDialog(reply.ToString());
                error.Commands.Add(new UICommand(
                "OK",
             new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
                await error.ShowAsync();
            }


        }

        /// <summary>
        /// This event is Triggered when the user clicks on the signUp button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            // call the SignUp event 
            var reply = await modelObject.SignUp(SignUpFirstName.Text, SignUpLastName.Text, SignupEmail.Text, SignupPassword.Password);
            if (reply.Equals("Successful"))
            {
                // prints the message
                var x = new MessageDialog(reply.ToString());
                x.Commands.Add(new UICommand(
                "OK",
             new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
                await x.ShowAsync();
                this.Frame.Navigate(typeof(UsersDisplay));
            }
            else
            {
                var x = new MessageDialog(reply.ToString());
                x.Commands.Add(new UICommand(
                 "OK",
              new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
                await x.ShowAsync();
            }
        }

        private void CommandInvokedHandler_Cancel(IUICommand command)
        {

        }

        /// <summary>
        /// This event is triggered when the user deletes the account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteClick(object sender, RoutedEventArgs e)
        {

            var reply = await modelObject.Delete(getDeleteEmail.Text, getDeletePassword.Password);
            if (reply.Equals("Successful"))
            {

                var error = new MessageDialog(reply.ToString());
                error.Commands.Add(new UICommand(
                 "OK",
              new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
                await error.ShowAsync();
                this.Frame.Navigate(typeof(UsersDisplay));



            }
            else
            {
                var error = new MessageDialog(reply.ToString());
                error.Commands.Add(new UICommand(
                "OK",
             new UICommandInvokedHandler(this.CommandInvokedHandler_Cancel)));
                await error.ShowAsync();

            }
        }




    }
}

