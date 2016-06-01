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
namespace AdtGekid.Validation
{
    /// <summary>
    /// Aufzählung der Arten, wie Stringwerte geprüft und zurückgegeben werden.
    /// </summary>
    public enum StringValidatorBehavior
    {
        /// <summary>
        /// Der Stringwert wird nicht verändert.
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind erlaubt.
        /// </summary>
        AllowEmpty,

        /// <summary>
        /// Der Stringwert wird, falls nicht <c>null</c> von führenden/angehängten Leerzeichen
        /// befreit (siehe <see cref="string.Trim"/>) und Großschreibung wird erzwungen.
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind erlaubt.
        /// </summary>
        UpcaseTrimAllowEmpty,

        /// <summary>
        /// Der Stringwert wird, falls nicht <c>null</c> von führenden/angehängten Leerzeichen
        /// befreit (siehe <see cref="string.Trim"/>) und Kleinschreibung wird erzwungen.
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind erlaubt.
        /// </summary>
        LowcaseTrimAllowEmpty,

        /// <summary>
        /// Der Stringwert wird nicht verändert.
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind NICHT erlaubt.
        /// </summary>
        NonEmpty,

        /// <summary>
        /// Der Stringwert wird, falls nicht <c>null</c> von führenden/angehängten Leerzeichen
        /// befreit (siehe <see cref="string.Trim"/>) und Großschreibung wird erzwungen.
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind NICHT erlaubt.
        /// </summary>
        UpcaseTrimNonEmpty,

        /// <summary>
        /// Der Stringwert wird, falls nicht <c>null</c> von führenden/angehängten Leerzeichen
        /// befreit (siehe <see cref="string.Trim"/>) und Kleinschreibung wird erzwungen.
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind NICHT erlaubt.
        /// </summary>
        LowcaseTrimNonEmpty,

        /// <summary>
        /// Der Stringwert wird lediglich, falls nicht <c>null</c>, von führenden/angehängten Leerzeichen
        /// befreit (siehe <see cref="string.Trim"/>).
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind NICHT erlaubt.
        /// </summary>
        TrimNonEmpty,

        /// <summary>
        /// Der Stringwert wird lediglich, falls nicht <c>null</c>, von führenden/angehängten Leerzeichen
        /// befreit (siehe <see cref="string.Trim"/>).
        /// Leere Strings, <c>null</c> sowie Strings bestehend aus Leerzeichen sind erlaubt.
        /// </summary>
        TrimAllowEmpty
    }

    public static class StringValidatorBehaviorExt
    {
        public static bool AllowsEmpty(this StringValidatorBehavior handling)
        => handling == StringValidatorBehavior.AllowEmpty
            || handling == StringValidatorBehavior.LowcaseTrimAllowEmpty
            || handling == StringValidatorBehavior.UpcaseTrimAllowEmpty
            || handling == StringValidatorBehavior.TrimAllowEmpty;

    }
}