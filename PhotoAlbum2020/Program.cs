//Mikaela Stanislav, Technical Showcase for Lean TECHniques, 6/11/2019
//Revised 12/2020
using Newtonsoft.Json;
using PhotoAlbum2020;
using System;
using System.Collections.Generic;

namespace PhotoAlbum
{
    class Program
    {
        public static void Main(string[] args)
        {
            int choice;
            Console.Write("Please Enter An Album Number bwtween 1 and 100. Enter 999 for all albums or -1 to exit.\n");

            // Verify entry is numeric
            bool validInput;
            validInput = int.TryParse(Console.ReadLine(), out choice);
            while (validInput == false)
            {
                Console.Write("Please enter numeric values between 1 and 100 only. Please try again.\n");
                validInput = int.TryParse(Console.ReadLine(), out choice);
            }

            while (choice != -1)
            {
                //displays error message and requests new input for invalid album numbers
                if (choice != 999 && (choice > 100 || choice < 1))
                {
                    Console.Write("You have entered an invalid album number. Please try again.\n");
                    validInput = int.TryParse(Console.ReadLine(), out choice);
                    if (choice == -1)
                    {
                        return;
                    }
                    while (validInput == false)
                    {
                        Console.Write("Please enter numeric values between 1 and 100 only. Please try again.\n");
                        validInput = int.TryParse(Console.ReadLine(), out choice);
                    }
                }

                List<Photo> albumList = GetAlbum(choice);
                //sets the master album id - used to display album number upon first instance of photo from album (for all album queries)
                int masterAlbumID = 1;
                //sets master album id to requested album if a single one is desired
                if (choice != 999)
                {
                    masterAlbumID = choice;
                }

                foreach (Photo p in albumList)
                {
                    if (masterAlbumID == p.albumId)
                    {
                        Console.Write("\nPhoto Album " + p.albumId + "\n\n");
                        masterAlbumID++;
                    }
                    Console.WriteLine("[{0}] {1} ", p.id, p.title);
                }
                Console.Write("Please Enter An Album Number between 1 and 100. Enter 999 for all albums or - 1 to exit.\n");

               validInput = int.TryParse(Console.ReadLine(), out choice);
                while (validInput == false)
                {
                    Console.Write("Please enter numeric values between 1 and 100 only. Please try again.\n");
                    validInput = int.TryParse(Console.ReadLine(), out choice);
                }
            }
        }

        public static List<Photo> GetAlbum(int albumNumber)
        {
            //sets url - either for a specific album or all albums
            string url = "https://jsonplaceholder.typicode.com/photos?albumId=";
            url = url + albumNumber.ToString();

            if (albumNumber == 999)
                url = "https://jsonplaceholder.typicode.com/photos";

            //reads in JSON data and deserializes it into a List of object "Photo"
            System.Net.WebClient wc = new System.Net.WebClient();
            string webData = wc.DownloadString(url);
            List<Photo> photoList = JsonConvert.DeserializeObject<List<Photo>>(webData);

            return photoList;
        }
    }
}



