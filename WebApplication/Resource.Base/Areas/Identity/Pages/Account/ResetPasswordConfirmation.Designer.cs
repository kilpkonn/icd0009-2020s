﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.Base.Areas.Identity.Pages.Account {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResetPasswordConfirmation {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResetPasswordConfirmation() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Resource.Base.Areas.Identity.Pages.Account.ResetPasswordConfirmation", typeof(ResetPasswordConfirmation).Assembly);
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
        
        public static string ResetPasswordConfirm {
            get {
                return ResourceManager.GetString("ResetPasswordConfirm", resourceCulture);
            }
        }
        
        public static string PasswordHasBeenResetPlease {
            get {
                return ResourceManager.GetString("PasswordHasBeenResetPlease", resourceCulture);
            }
        }
        
        public static string ClickHereToLogIn {
            get {
                return ResourceManager.GetString("ClickHereToLogIn", resourceCulture);
            }
        }
    }
}
