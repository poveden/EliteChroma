﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EliteChroma.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EliteChroma.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon EliteChromaIcon {
            get {
                object obj = ResourceManager.GetObject("EliteChromaIcon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap EliteChromaLogo {
            get {
                object obj = ResourceManager.GetObject("EliteChromaLogo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RedDot {
            get {
                object obj = ResourceManager.GetObject("RedDot", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EliteChroma was unable to find the Razer Chroma SDK.
        ///
        ///The application will now exit..
        /// </summary>
        internal static string MsgBox_RazerChromaSdkNotFound {
            get {
                return ResourceManager.GetString("MsgBox_RazerChromaSdkNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EliteChroma was unable to identify all Elite:Dangerous-related folders.
        ///
        ///Please choose the right folders in the following window..
        /// </summary>
        internal static string MsgBox_UnableToIdentifyFolders {
            get {
                return ResourceManager.GetString("MsgBox_UnableToIdentifyFolders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://support.frontier.co.uk/kb/faq.php?id=108.
        /// </summary>
        internal static string Url_GameInstallFoldersHelp {
            get {
                return ResourceManager.GetString("Url_GameInstallFoldersHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://support.frontier.co.uk/kb/faq.php?id=424.
        /// </summary>
        internal static string Url_GameOptionsFolderHelp {
            get {
                return ResourceManager.GetString("Url_GameOptionsFolderHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://support.frontier.co.uk/kb/faq.php?id=372.
        /// </summary>
        internal static string Url_JournalFolderHelp {
            get {
                return ResourceManager.GetString("Url_JournalFolderHelp", resourceCulture);
            }
        }
    }
}
