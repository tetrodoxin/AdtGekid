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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungVerlaufTod", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class VerlaufTod
    {
        private Collection<string> _ursachenIcdCodes;

        [XmlArrayItem("Todesursache_ICD", IsNullable = false)]
        [XmlArray("Menge_Todesursache", Order = 3)]
        public Collection<string> UrsachenIcdCodes
        {
            get { return _ursachenIcdCodes; }
            set { _ursachenIcdCodes = value.EnsureValidatedStringList().WithValidator(new StringValidatorByRegex(@"^[A-Z]\d\d(\.\d(\d)?)?$")); }
        }

        /// <summary>
        /// Tag an dem der Patient verstorben ist.
        /// </summary>
        [XmlElement("Sterbedatum", Order = 1)]
        public DatumTyp Sterbedatum { get; set; }

        /// <summary>
        /// Krebs-Tod-Relation
        /// </summary>
        [XmlElement("Tod_tumorbedingt", Order = 2)]
        public JnuTyp Tumorbedingt { get; set; }
    }
}
