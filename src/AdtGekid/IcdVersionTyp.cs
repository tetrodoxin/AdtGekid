
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
    [XmlType("ICD_Version_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum IcdVersionTyp
    {        
        NotSpecified = 0,

        [XmlEnum("Sonstige")]
        Sonstige = 1,

        [XmlEnum("10 2013 GM")]
        GM_10_2013 = 2013,

        [XmlEnum("10 2014 GM")]
        GM_10_2014 = 2014,

        [XmlEnum("10 2015 GM")]
        GM_10_2015 = 2015,

        [XmlEnum("10 2016 GM")]
        GM_10_2016 = 2016,

        [XmlEnum("10 2017 GM")]
        GM_10_2017 = 2017,

        [XmlEnum("10 2018 GM")]
        GM_10_2018 = 2018,

        [XmlEnum("10 2019 GM")]
        GM_10_2019 = 2019
    }
}
