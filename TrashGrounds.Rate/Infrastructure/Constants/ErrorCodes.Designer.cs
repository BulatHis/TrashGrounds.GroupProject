﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TrashGrounds.Rate.Infrastructure.Constants {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    public class ErrorCodes {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorCodes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null))
                {
                    ResourceManager temp =
                        new ResourceManager(
                            "TrashGrounds.Rate.Infrastructure.Constants.ErrorCodes", typeof(ErrorCodes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALREADY_EXISTS.
        /// </summary>
        public static string AlreadyExistsError {
            get {
                return ResourceManager.GetString("AlreadyExistsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FORBIDDEN.
        /// </summary>
        public static string ForbiddenError {
            get {
                return ResourceManager.GetString("ForbiddenError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BAD_REQUEST.
        /// </summary>
        public static string BadRequestError {
            get {
                return ResourceManager.GetString("BadRequestError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NOT_FOUND.
        /// </summary>
        public static string NotFoundError {
            get {
                return ResourceManager.GetString("NotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VALIDATION_FAILED.
        /// </summary>
        public static string ValidationFailedError {
            get {
                return ResourceManager.GetString("ValidationFailedError", resourceCulture);
            }
        }
    }
}