using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_TT_Server.CustomAssemblyInfo
{
    /// <summary>
    /// Stores the version of Tracking Tools that this build is designed for
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyTrackingToolsVersion : Attribute 
    {
        private string _version = "";
        /// <summary>
        /// The version of Tracking Tools that this build is designed for
        /// </summary>
        /// <param name="version">The version number</param>
        public AssemblyTrackingToolsVersion(string version)
        {
            _version = version;
        }

        /// <summary>
        /// Gets the version of Tracking Tools that this build is designed for
        /// </summary>
        public string TrackingToolsVersion
        {
            get { return _version; }
        }
    }

}
