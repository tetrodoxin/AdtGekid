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
    /// <summary>
    /// Datentyp für den Absender einer Meldung.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDAbsender", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Absender
    {
        private string _id;
        private string _softwareId;
        private string _installationsId;
        private string _bezeichnung;
        private string _ansprechpartner;
        private string _telefon;
        private string _anschrift;
        private string _email;

        private string _typeName = typeof(Absender).ToString();

        /// <summary>
        /// <para>
        /// Eindeutig identifizierendes Merkmal des Absenders
        /// </para>
        /// <para>
        /// Ein zwar beliebiger, aber eindeutiger Melderschlüssel, vergeben durch das für
        /// den Behandlungsort zuständige Krebsregister
        /// </para>
        /// </summary>
        [XmlAttribute("Absender_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal der Software
        /// </summary>
        [XmlAttribute("Software_ID")]
        public string SoftwareId
        {
            get { return _softwareId; }
            set { _softwareId = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.SoftwareId)); }
        }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal der Installation
        /// </summary>
        [XmlAttribute("Installations_ID")]
        public string InstallationsId
        {
            get { return _installationsId; }
            set { _installationsId = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.InstallationsId)); }
        }

        /// <summary>
        /// Name des Ansprechpartners beim Absender
        /// </summary>
        [XmlElement("Absender_Ansprechpartner", Order = 2)]
        public string Ansprechpartner
        {
            get { return _ansprechpartner; }
            set { _ansprechpartner = value.ValidateMaxLength(100, _typeName, nameof(this.Ansprechpartner)); }
        }

        /// <summary>
        /// Name der Einrichtung des Absenders
        /// </summary>
        [XmlElement("Absender_Bezeichnung", Order = 1)]
        public string Bezeichnung
        {
            get { return _bezeichnung; }
            set { _bezeichnung = value.ValidateMaxLength(255, _typeName, nameof(this.Bezeichnung)); }
        }

        /// <summary>
        /// Anschrift (Straße, Hausnummer und Ort) des Absenders
        /// </summary>
        [XmlElement("Absender_Anschrift", Order = 3)]
        public string Anschrift
        {
            get { return _anschrift; }
            set { _anschrift = value.ValidateMaxLength(70, _typeName, nameof(this.Anschrift)); }
        }

        /// <summary>
        /// Telefonnummer des Absenders
        /// </summary>
        [XmlElement("Absender_Telefon", Order = 4)]
        public string Telefon
        {
            get { return _telefon; }
            set { _telefon = value.ValidateOrThrow(TelefonStringValidator.Instance, _typeName, nameof(this.Telefon)); }
        }

        /// <summary>
        /// E-Mail-Adresse des Absenders
        /// </summary>
        [XmlElement("Absender_EMail", Order = 5)]
        public string Email
        {
            get { return _email; }
            set { _email = value.ValidateOrThrow(EmailStringValidator.Instance, _typeName, nameof(this.Email)); }
        }
    }
}
