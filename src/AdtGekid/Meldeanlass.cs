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

namespace AdtGekid
{
    /// <summary>
    /// Aufz�hlung f�r den Anlass einer Meldung.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungMeldeanlass", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum Meldeanlass
    {
        /// <summary>
        /// Default (keine XML-Repr�sentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        /// <summary>
        /// Anlass ist eine neue meldepflichtige Tumordiagnose
        /// </summary>
        [XmlEnum("diagnose")]
        Diagnose,

        /// <summary>
        /// Anlass ist der Beginn einer Therapie
        /// </summary>
        [XmlEnum("behandlungsbeginn")]
        BehandlungsBeginn,

        /// <summary>
        /// Anlass war das Ende einer Therapie
        /// </summary>
        [XmlEnum("behandlungsende")]
        BehandlungsEnde,

        /// <summary>
        /// Eine Status�nderung ist Anlass der Meldung
        /// </summary>
        [XmlEnum("statusaenderung")]
        StatusAenderung,

        /// <summary>
        /// Die Meldung ist eine Statusmeldung
        /// </summary>
        [XmlEnum("statusmeldung")]
        StatusMeldung,

        /// <summary>
        /// Anlass ist der Tod des Patienten
        /// </summary>
        [XmlEnum("tod")]
        Tod,

        /// <summary>
        /// Anlass ist das Vorliegen eines histologischen/zytologischen Ergebnisses.
        /// </summary>
        [XmlEnum("histologie_zytologie")]
        HistologieZytologie,
    }
}
