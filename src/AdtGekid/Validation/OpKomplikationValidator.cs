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

namespace AdtGekid.Validation
{
    public class OpKomplikationValidator : StringValidatorByStrings, IStringListValidator
    {
        private const string CodeNo = "N";
        private const string CodeUnknown = "U";
        private static string[] KomplikationenCodes = new string[]
        {
            CodeNo,
            CodeUnknown,
            "ABD",
            "ABS",
            "ASF",
            "ANI",
            "AEP",
            "ALR",
            "ANS",
            "AEE",
            "API",
            "BIF",
            "BOG",
            "BOE",
            "BSI",
            "CHI",
            "DAI",
            "DPS",
            "DIC",
            "DEP",
            "DLU",
            "DSI",
            "ENF",
            "GER",
            "HEM",
            "HUR",
            "HAE",
            "HFI",
            "HNK",
            "HZI",
            "HRS",
            "HNA",
            "HOP",
            "HYB",
            "HYF",
            "IFV",
            "KAS",
            "KES",
            "KIM",
            "KRA",
            "KDS",
            "LEV",
            "LOE",
            "LYF",
            "LYE",
            "MES",
            "MIL",
            "MED",
            "MAT",
            "MYI",
            "RNB",
            "NAB",
            "NIN",
            "OES",
            "OSM",
            "PAF",
            "PIT",
            "PAB",
            "PPA",
            "PAV",
            "PER",
            "PLB",
            "PEY",
            "PLE",
            "PMN",
            "PNT",
            "PDA",
            "PAE",
            "RPA",
            "RIN",
            "SKI",
            "SES",
            "SFH",
            "STK",
            "TZP",
            "TIA",
            "TRZ",
            "WUH",
            "WSS"
        };

        private static readonly Lazy<IValueValidator<string>> _instance = new Lazy<IValueValidator<string>>(() => new OpKomplikationValidator());
        public static IValueValidator<string> Instance => _instance.Value;

        private OpKomplikationValidator() : base(StringValidatorBehavior.UpcaseTrimAllowEmpty, KomplikationenCodes, 3)
        { }

        public string GetValidationErrorText(IList<string> list)
        {
            if (list.Count == 0) return null;

            var singletonCode = list.FirstOrDefault(p => p == CodeNo || p == CodeUnknown);
            if(singletonCode != null && list.Count > 1)
            {
                return $"Der Code '{singletonCode}' erlaubt keine weiteren Einträge in der Aufzählung der OP Komplikationen.";
            }

            return null;
        }
    }
}