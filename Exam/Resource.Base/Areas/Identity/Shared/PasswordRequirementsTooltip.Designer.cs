﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.App.Views.Shared {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PasswordRequirementsTooltip {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PasswordRequirementsTooltip() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Resource.Base.Areas.Identity.Shared.PasswordRequirementsTooltip", typeof(PasswordRequirementsTooltip).Assembly);
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
        
        public static string PasswordRequirements {
            get {
                return ResourceManager.GetString("PasswordRequirements", resourceCulture);
            }
        }
        
        public static string PasswordLengthSingle {
            get {
                return ResourceManager.GetString("PasswordLengthSingle", resourceCulture);
            }
        }
        
        public static string PasswordLength {
            get {
                return ResourceManager.GetString("PasswordLength", resourceCulture);
            }
        }
        
        public static string PasswordRequireDigit {
            get {
                return ResourceManager.GetString("PasswordRequireDigit", resourceCulture);
            }
        }
        
        public static string PasswordRequireLowercase {
            get {
                return ResourceManager.GetString("PasswordRequireLowercase", resourceCulture);
            }
        }
        
        public static string PasswordRequireUppercase {
            get {
                return ResourceManager.GetString("PasswordRequireUppercase", resourceCulture);
            }
        }
        
        public static string PasswordRequireNonAlphanumeric {
            get {
                return ResourceManager.GetString("PasswordRequireNonAlphanumeric", resourceCulture);
            }
        }
        
        public static string PasswordRequireUniqueCharsSingle {
            get {
                return ResourceManager.GetString("PasswordRequireUniqueCharsSingle", resourceCulture);
            }
        }
        
        public static string PasswordRequireUniqueChars {
            get {
                return ResourceManager.GetString("PasswordRequireUniqueChars", resourceCulture);
            }
        }
    }
}
