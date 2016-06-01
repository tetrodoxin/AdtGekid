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
    /// <summary>
    /// Schnittstelle für Validierer von <see cref="ValidatedStringList"/>,
    /// die nicht einzelne Einträge validieren, sondern die
    /// Plausibilität der ganzen Liste prüfen sollen.
    /// </summary>
    public interface IStringListValidator
    {
        /// <summary>
        /// Prüfe Validität einer Liste von Strings, die einzeln bereits validiert wurden
        /// , frei von Dopplungen sind und nun in ihrer Gesamtheit geprüft werden sollen.
        /// </summary>
        /// <param name="list">Die zu validierende Liste von Strings.</param>
        /// <returns>Den Fehlertext der Plausibilitätsregel, gegen den die Liste verstößt. 
        /// Andernfalls <c>null</c>, falls keine Fehler gefunden wurden.</returns>
        string GetValidationErrorText(IList<string> list);
    }
}
