using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.ResourceManager
{
    public interface IResourceService
    {
        /// <summary>
        /// Method returns a string based on Current culture.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        string GetLiteral(string key, string DefaultValue = "");

        /// <summary>
        /// method return a currency sysmbol based on current culture.
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        string GetCurrencySymbol();

        /// <summary>
        /// method return a image resource for the current culture.
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        Image GetImage(string key);

        /// <summary>
        /// Add resource to Database or resource file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void AddResource(string name, object value);
    }
}
