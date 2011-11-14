using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;

namespace ReferenceDatabase
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void CreateLocalDBButton_Click(object sender, RoutedEventArgs e)
        {
            // Uri to the reference database
            var uri = new Uri("ReferenceDB.sdf", UriKind.Relative);

            // Obtain the virtual store for the application.
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Create a stream for the file in the installation folder.
                using (Stream input = Application.GetResourceStream(uri).Stream)
                {
                    // Create a stream for the new file in isolated storage.
                    using (IsolatedStorageFileStream output = iso.CreateFile("ReferenceDB.sdf"))
                    {
                        // Initialize the buffer.
                        byte[] readBuffer = new byte[4096];
                        int bytesRead = -1;

                        // Copy the file from the installation folder to isolated storage.
                        while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            output.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
            }
        }

        private void ShowOriginalDBButton_Click(object sender, RoutedEventArgs e)
        {
            ShowData("Data Source = 'appdata:/ReferenceDB.sdf'; File Mode = read only;");
        }

        private void ShowNewDBButton_Click(object sender, RoutedEventArgs e)
        {
            ShowData("Data Source = 'isostore:/ReferenceDB.sdf';");
        }

        private void ShowData(string connectionString)
        {
            var db = new MyDataContext(connectionString);
            var people = from p in db.People select p;
            DatabaseInfoListBox.ItemsSource = people;
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new MyDataContext("Data Source = 'isostore:/ReferenceDB.sdf';");
            var p = new Person()
            {
                FirstName = "Baby",
                LastName = "Koenig",
                BirthDate = DateTime.Now,
                FavoriteColor = "Pink",
            };
            db.People.InsertOnSubmit(p);
            db.SubmitChanges();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseInfoListBox.ItemsSource = null;
        }
    }
}