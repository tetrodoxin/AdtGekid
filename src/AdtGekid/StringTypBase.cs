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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AdtGekid
{
    public abstract class StringTypBase : IXmlSerializable, IEquatable<StringTypBase>
    {
        private string _str;

        protected abstract bool AllowEmpty { get; }

        protected StringTypBase()
        {
            _str = null;
        }

        XmlSchema IXmlSerializable.GetSchema() => null;

        protected void SetString(string str)
        {
            if (AllowEmpty && string.IsNullOrEmpty(str))
            {
                _str = str;
            }
            if (!AllowEmpty && string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Leerer String nicht erlaubt.");
            }

            str = TransformNonemptyString(str);
            if (IsStringValid(str))
            {
                _str = str;
            }
            else
            {
                throw new ArgumentException("Unerlaubtes Format.");
            }
        }

        protected virtual string TransformNonemptyString(string str) => str;

        protected abstract bool IsStringValid(string str);

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                _str = reader.ReadString();
                reader.ReadEndElement();
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if(!string.IsNullOrEmpty(_str))
            {
                writer.WriteString(_str);
            }
        }

        public static implicit operator string(StringTypBase nt) => nt != null ? nt._str : null;

        public override string ToString() => _str;

        public static bool Equals(StringTypBase x, StringTypBase y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if ((object)x == null || (object)y == null)
            {
                return false;
            }

            return string.Equals(x._str, y._str);
        }

        public static int GetHashCode(StringTypBase o)
        {
            if(o == null | o._str == null)
            {
                return 0;
            }
            else
            {
                return o._str.GetHashCode();
            }
        }

        public bool Equals(StringTypBase other) => Equals(this, other);

        public override bool Equals(object obj) => Equals(this, obj as StringTypBase);

        public override int GetHashCode() => GetHashCode(this);

        public static bool operator ==(StringTypBase x, StringTypBase y) => Equals(x, y);


        public static bool operator !=(StringTypBase x, StringTypBase y) => !Equals(x, y);
    }
}

