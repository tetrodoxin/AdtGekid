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
using AdtGekid.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdtGekid
{
    public class ValidatedStringList : Collection<string>
    {
        public IValueValidator<string> Validator { get; set; }

        public bool AllowDoubling { get; set; } = true;

        public ValidatedStringList() : base()
        { }

        public ValidatedStringList(IList<string> source) : this()
        {
            if(source != null)
            {
                foreach(var e in source)
                {
                    Add(e);
                }
            }
        }

        protected override void InsertItem(int index, string item)
        {
            item = getValueOrThrow(item);

            base.InsertItem(index, item);
        }

        private string getValueOrThrow(string value)
        {
            if (Validator != null)
            {
                value = Validator.GetValidatedValueOrThrow(value);

                if(!AllowDoubling && Contains(value))
                {
                    throw new ArgumentException($"Wert ' {value}' bereits vorhanden und keine Dopplungen erlaubt.");
                }

                var listValidator = Validator as IStringListValidator;
                if (listValidator != null)
                {
                    var items = this.ToList();
                    items.Add(value);

                    var err = listValidator.GetValidationErrorText(items);
                    if (err != null)
                    {
                        throw new ArgumentException(err);
                    }
                }
            }

            return value;
        }

        protected override void SetItem(int index, string item)
        {
            item = getValueOrThrow(item);

            base.SetItem(index, item);
        }
    }

    public static class StringListExtensions
    {
        public static ValidatedStringList EnsureValidatedStringList(this IList<string> c)
        {
            if (c == null)
            {
                return new ValidatedStringList();
            }
            else
            {
                return new ValidatedStringList(c);
            }
        }

        public static ValidatedStringList WithValidator(this ValidatedStringList list, IValueValidator<string> validator)
        {
            if (list != null)
            {
                list.Validator = validator;
            }

            return list;
        }
    }
}