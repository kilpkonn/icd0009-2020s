﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.Base.Areas.Identity.Pages.Account.Manage {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Disable2fa {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Disable2fa() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Resource.Base.Areas.Identity.Pages.Account.Manage.Disable2fa", typeof(Disable2fa).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string DisableTwoFactorAuthentication {
            get {
                return ResourceManager.GetString("DisableTwoFactorAuthentication", resourceCulture);
            }
        }
        
        public static string OnlyDisables2FA {
            get {
                return ResourceManager.GetString("OnlyDisables2FA", resourceCulture);
            }
        }
        
        public static string Disabling2FAExplanation {
            get {
                return ResourceManager.GetString("Disabling2FAExplanation", resourceCulture);
            }
        }
        
        public static string Disable2FASubmit {
            get {
                return ResourceManager.GetString("Disable2FASubmit", resourceCulture);
            }
        }
        
        public static string ResetYourAuthenticatorKeys {
            get {
                return ResourceManager.GetString("ResetYourAuthenticatorKeys", resourceCulture);
            }
        }
    }
}
