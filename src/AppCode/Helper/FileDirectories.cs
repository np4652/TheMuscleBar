﻿using System.IO;

namespace TheMuscleBar.AppCode.Helper
{
    public class FileDirectories
    {
        public static string Receipt = "wwwroot/receipt/"; 
        public static string Profile = "wwwroot/Images/Profile/"; 
        public static string Thumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Thumbnail/");
    }
}
