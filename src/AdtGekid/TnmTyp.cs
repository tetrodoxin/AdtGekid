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
    [XmlType("Menge_TNM_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class TnmTyp
    {
        private static readonly char[] SymbolYChars = new char[] { 'y' };
        private static readonly char[] SymbolAChars = new char[] { 'a' };
        private static readonly char[] SymbolRChars = new char[] { 'r' };

        private string _praefixM;
        private string _praefixN;
        private string _praefixT;
        private TnmSymbolA _symbolA;
        private TnmSymbolR _symbolR;
        private TnmSymbolY _symbolY;
        private TnmCategoryL _l;
        private TnmCategoryPn _pn;
        private TnmCategoryS _s;
        private TnmCategoryV _v;
        private string _id;        
        private TnmVersion _version;

        private string _typeName = "TNM";

        /// <summary>
        /// Gibt an, ob die Klassifikation aus Anlass einer Autopsie erfolgte.
        /// </summary>      
        [XmlIgnore]
        public string SymbolA
        {
            get { return _symbolA.ToString(); }
            set { _symbolA = value.TryParseAsEnumOrThrow<TnmSymbolA>(_typeName, nameof(this.SymbolA)); }           
        }

        [XmlElement("TNM_a_Symbol", Order = 5)]
        public TnmSymbolA SymbolAEnumValue
        {
            get { return _symbolA; }
            set { _symbolA = value; }
        }

        /// <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="SymbolAEnumValue"/>.
        /// Bei <see cref="TnmSymbolA.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool SymbolAEnumValueSpecified => SymbolAEnumValue != TnmSymbolA.NotSpecified;

        /// <summary>
        /// Gibt an, ob die Klassifikation klinisch oder pathologisch erfolgte.
        /// </summary>
        [XmlElement("TNM_c_p_u_Praefix_M", Order = 11)]
        public string PraefixM
        {
            get { return _praefixM; }
            set { _praefixM = value.ValidateOrThrow(TnmPraefixValidator.Instance, _typeName, nameof(this.PraefixM)); }
        }

        /// <summary>
        /// Gibt an, ob die Klassifikation klinisch oder pathologisch erfolgte.
        /// </summary>
        [XmlElement("TNM_c_p_u_Praefix_N", Order = 9)]
        public string PraefixN
        {
            get { return _praefixN; }
            set { _praefixN = value.ValidateOrThrow(TnmPraefixValidator.Instance, _typeName, nameof(this.PraefixN)); }
        }

        /// <summary>
        /// Gibt an, ob die Klassifikation klinisch oder pathologisch erfolgte.
        /// </summary>
        [XmlElement("TNM_c_p_u_Praefix_T", Order = 6)]
        public string PraefixT
        {
            get { return _praefixT; }
            set { _praefixT = value.ValidateOrThrow(TnmPraefixValidator.Instance, _typeName, nameof(this.PraefixT)); }
        }

        /// <summary>
        /// Gibt an, auf welchen Zeitpunkt sich die TNM-Klassifikation bezieht
        /// </summary>
        [XmlElement("TNM_Datum", Order = 1)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal für den TNM
        /// </summary>
        /// <value>
        /// Alphanumerischer Wert bis 16 Zeichen
        /// </value>
        [XmlAttribute("TNM_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Lymphgefäßinvasion
        /// </summary>
        [XmlIgnore]
        public string L
        {
            get { return _l.ToString(); }           
            set { _l = value.TryParseAsEnumOrThrow<TnmCategoryL>(_typeName, nameof(this.L)); }
        }

        [XmlElement("TNM_L", Order = 13)]
        public TnmCategoryL CategoryL
        {
            get { return _l; }
            set { _l = value; }
        }

        /// <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="CategoryL"/>.
        /// Bei <see cref="TnmCategoryL.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool CategoryLSpecified => CategoryL != TnmCategoryL.NotSpecified;


        /// <summary>
        /// Ausbreitung des Primärtumors, erfolgt gemäß Tumorentität nach TNM und
        /// TNM-Supplement
        /// </summary>
        [XmlElement("TNM_M", Order = 12)]
        public string M { get; set; }

        /// <summary>
        /// Kennzeichnet Vorhandensein multipler Primärtumoren in einem Bezirk.
        /// </summary>
        [XmlElement("TNM_m_Symbol", Order = 8)]
        public string SymbolM { get; set; }

        /// <summary>
        /// Ausbreitung des Primärtumors, erfolgt gemäß Tumorentität nach TNM und
        /// TNM-Supplement
        /// </summary>
        [XmlElement("TNM_N", Order = 10)]
        public string N { get; set; }

        /// <summary>
        /// Perineuralinvasion
        /// </summary>  
        [XmlIgnore]
        public string Pn
        {
            get { return _pn.ToString(); }            
            set { _pn = value.TryParseAsEnumOrThrow<TnmCategoryPn>(_typeName, nameof(this.Pn)); }
        }

        [XmlElement("TNM_Pn", Order = 15)]
        public TnmCategoryPn CategoryPn
        {
            get { return _pn; }
            set { _pn = value; }
        }

        /// <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="CategoryPn"/>.
        /// Bei <see cref="TnmCategoryPn.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool CategoryPnSpecified => CategoryPn != TnmCategoryPn.NotSpecified;

        /// <summary>
        /// Gibt an, ob die Klassifikation ein Rezidiv beurteilt.
        /// </summary>
        [XmlIgnore]
        public string SymbolR
        {
            get { return _symbolR.ToString(); }
            set { _symbolR = value.TryParseAsEnumOrThrow<TnmSymbolR>(_typeName, nameof(this.SymbolR)); }           
        }

        [XmlElement("TNM_r_Symbol", Order = 4)]
        public TnmSymbolR SymbolREnumValue
        {
            get { return _symbolR; }
            set { _symbolR = value; }
        }

        /// <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="SymbolREnumValue"/>.
        /// Bei <see cref="TnmSymbolR.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool SymbolREnumValueSpecified => SymbolREnumValue != TnmSymbolR.NotSpecified;


        /// <summary>
        /// Serumtumormarker
        /// </summary>
        [XmlIgnore]
        public string S
        {
            get { return _s.ToString(); }            
            set { _s = value.TryParseAsEnumOrThrow<TnmCategoryS>(_typeName, nameof(this.S)); }
        }

        [XmlElement("TNM_S", Order = 16)]
        public TnmCategoryS CategoryS
        {
            get { return _s; }
            set { _s = value; }
        }

        // <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="CategoryS"/>.
        /// Bei <see cref="TnmCategoryS.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool CategorySSpecified => CategoryS != TnmCategoryS.NotSpecified;



        /// <summary>
        /// Ausbreitung des Primärtumors, erfolgt gemäß Tumorentität nach TNM und
        /// TNM-Supplement.
        /// </summary>
        [XmlElement("TNM_T", Order = 7)]
        public string T { get; set; }

        /// <summary>
        /// Veneninvasion
        /// </summary>   
        [XmlIgnore]
        public string V
        {
            get { return _v.ToString(); }            
            set { _v = value.TryParseAsEnumOrThrow<TnmCategoryV>(_typeName, nameof(this.V)); }
        }

        // <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="CategoryV"/>.
        /// Bei <see cref="TnmCategoryV.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool CategoryVSpecified => CategoryV != TnmCategoryV.NotSpecified;


        [XmlElement("TNM_V", Order = 14)]
        public TnmCategoryV CategoryV
        {
            get { return _v; }
            set { _v = value; }
        }


        /// <summary>
        /// Gibt an, nach welcher Version des TNM klassifiziert wurde, da die Klassifikati-
        /// onsregeln abhängig von der Version sind.
        /// </summary>        
        [XmlIgnore]
        public int? Version
        {
            get { return (int)_version; }            
            set { _version = value.ToString().TryParseAsEnumOrThrow<TnmVersion>(_typeName, nameof(this.Version)); }
        }

        [XmlElement("TNM_Version", Order = 2)]
        public TnmVersion VersionEnumValue
        {
            get { return _version; }
            set { _version = value; }
        }

        
             

        /// <summary>
        /// Gibt an, ob die Klassifikation während oder nach initialer multimodaler Thera-
        /// pie erfolgte.
        /// </summary>      
        [XmlIgnore]
        public string SymbolY
        {
            get { return _symbolY.ToString(); }            
            set { _symbolY = value.ToString().TryParseAsEnumOrThrow<TnmSymbolY>(_typeName, nameof(this.SymbolY)); }
        }

        [XmlElement("TNM_y_Symbol", Order = 3)]
        public TnmSymbolY SymbolYEnumValue
        {
            get { return _symbolY; }
            set { _symbolY = value; }
        }

        /// <summary>
        /// Angabe zur Steuerung der Serialisierung von <see cref="SymbolYEnumValue"/>.
        /// Bei <see cref="TnmSymbolY.NotSpecified"/> wird nicht serialisiert.
        /// </summary>
        [XmlIgnore]
        public bool SymbolYEnumValueSpecified => SymbolYEnumValue != TnmSymbolY.NotSpecified;
    }
}
