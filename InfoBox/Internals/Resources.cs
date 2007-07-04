using System;

namespace InfoBox
{
	/// <summary>
	/// Handles resources.
	/// </summary>
	internal class Resources 
	{
        private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
        
		internal Resources() 
		{
		}
        
		/// <summary>
		///   Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager 
		{
			get 
			{
				if (object.ReferenceEquals(resourceMan, null)) 
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("InfoBox.Properties.Resources", typeof(Resources).Assembly);
					resourceMan = temp;
				}
				return resourceMan;
			}
		}
        
		/// <summary>
		///   Overrides the current thread's CurrentUICulture property for all
		///   resource lookups using this strongly typed resource class.
		/// </summary>
		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static System.Globalization.CultureInfo Culture 
		{
			get 
			{
				return resourceCulture;
			}
			set 
			{
				resourceCulture = value;
			}
		}
        
		internal static System.Drawing.Icon IconError 
		{
			get 
			{
				object obj = ResourceManager.GetObject("IconError", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}
        
		internal static System.Drawing.Icon IconGood 
		{
			get 
			{
				object obj = ResourceManager.GetObject("IconGood", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}
        
		internal static System.Drawing.Icon IconInfo 
		{
			get 
			{
				object obj = ResourceManager.GetObject("IconInfo", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}
        
		internal static System.Drawing.Icon IconQuestion 
		{
			get 
			{
				object obj = ResourceManager.GetObject("IconQuestion", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}
        
		internal static System.Drawing.Icon IconWarning 
		{
			get 
			{
				object obj = ResourceManager.GetObject("IconWarning", resourceCulture);
				return ((System.Drawing.Icon)(obj));
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to Abort.
		/// </summary>
		internal static string LabelAbort 
		{
			get 
			{
				return ResourceManager.GetString("LabelAbort", resourceCulture);
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to Cancel.
		/// </summary>
		internal static string LabelCancel 
		{
			get 
			{
				return ResourceManager.GetString("LabelCancel", resourceCulture);
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to Ignore.
		/// </summary>
		internal static string LabelIgnore 
		{
			get 
			{
				return ResourceManager.GetString("LabelIgnore", resourceCulture);
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to No.
		/// </summary>
		internal static string LabelNo 
		{
			get 
			{
				return ResourceManager.GetString("LabelNo", resourceCulture);
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to OK.
		/// </summary>
		internal static string LabelOK 
		{
			get 
			{
				return ResourceManager.GetString("LabelOK", resourceCulture);
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to Retry.
		/// </summary>
		internal static string LabelRetry 
		{
			get 
			{
				return ResourceManager.GetString("LabelRetry", resourceCulture);
			}
		}
        
		/// <summary>
		///   Looks up a localized string similar to Yes.
		/// </summary>
		internal static string LabelYes 
		{
			get 
			{
				return ResourceManager.GetString("LabelYes", resourceCulture);
			}
		}
	}
}
