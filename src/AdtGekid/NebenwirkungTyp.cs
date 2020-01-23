#region license

//MIT License

//Copyright(c) 2016 Andreas Huebner

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion 
using System;
using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Nebenwirkung_Typ", Namespace = Root.GekidNamespace)]
    public class NebenwirkungTyp
    {
        private static char[] AllowedGradeCodes = "K345U".ToCharArray();
        private const string VersionPattern = @"^(Version )?4$";

        private NebenwirkungGrad _grad;
        private NebenwirkungVersion _version;
        private string _art;

        private string _typeName = "Nebenwirkung";

        /// <summary>
        /// Gibt an, zu welcher Nebenwirkung es bei der systemischen Therapie gekommen ist 
        /// (sogenannte akute Nebenwirkungen bis zum 90. Tag nach Therapiebeginn).
        /// </summary>
        [XmlElement("Nebenwirkung_Art", Order = 2)]
        public string Art
        {
            get { return _art; }
            set { _art = value.ValidateMaxLength(255, _typeName, nameof(this.Art)); }
        }

        /// <summary>
        /// Gibt an, zu welchem Schweregrad von Nebenwirkungen es bei der systemi-
        /// schen Therapie gekommen ist (sogenannte akute Nebenwirkungen bis zum 90.
        /// Tag nach Bestrahlungsbeginn).
        /// </summary>
        [XmlIgnore]
        public string Grad
        {
            get { return _grad.ToString(); }
            //set { _grad = value.ValidateOrThrow(AllowedGradeCodes, _typeName, nameof(this.Grad)); }
            set { _grad = value.TryParseAsEnumOrThrow<NebenwirkungGrad>(_typeName, nameof(this.Grad)); }
        }

        [XmlElement("Nebenwirkung_Grad", Order = 1)]
        public NebenwirkungGrad GradEnumValue
        {
            get { return _grad; }
            set { _grad = value; }
        }

        /// <summary>
        /// Gibt an, nach welcher CTC-Version die Nebenwirkungen angegeben sind.
        /// </summary>
        [XmlIgnore]
        public string Version
        {
            get { return _version.ToString(); }
            set { _version = value.TryParseAsEnumOrThrow<NebenwirkungVersion>(_typeName, nameof(this.Version)); }
        }

        [XmlElement("Nebenwirkung_Version", Order = 3)]
        public NebenwirkungVersion VersionEnumValue
        {
            get { return _version; }
            set { _version = value; }
        }
    }
}
