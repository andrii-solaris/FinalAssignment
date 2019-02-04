using NUnit.Framework;
using System;
using System.IO;

namespace FinalAssignment.Utils
{
    //Contains a set of calculable pathes to the base directory of the project. Used for assiting in storing logs and HTML reports.
    struct Constants
    {
        public static readonly string Directory = $@"Reports_{ DateTime.Now:HH_ mm_ ss}";
        public static readonly string CurrentDirectory = $"{System.IO.Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory)))}";
    }
}