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
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Allgemeiner_Leistungszustand_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum AllgemeinerLeistungszustandTyp
    {           
        NotSpecified = -1,

        [XmlEnum("0")]
        Item0 = 0,
        
        [XmlEnum("1")]
        Item1 = 1,
        
        [XmlEnum("2")]
        Item2 = 2,
        
        [XmlEnum("3")]
        Item3 = 3,
        
        [XmlEnum("4")]
        Item4 = 4,

        [XmlEnum("U")]
        U,        

        [XmlEnum("10%")]
        Item10Percent,
        
        [XmlEnum("20%")]
        Item20Percent,
        
        [XmlEnum("30%")]
        Item30Percent,
        
        [XmlEnum("40%")]
        Item40Percent,
        
        [XmlEnum("50%")]
        Item50Percent,
        
        [XmlEnum("60%")]
        Item60Percent,
        
        [XmlEnum("70%")]
        Item70Percent,
        
        [XmlEnum("80%")]
        Item80Percent,
        
        [XmlEnum("90%")]
        Item90Percent,
        
        [XmlEnum("100%")]
        Item100Percent,
    }
}
