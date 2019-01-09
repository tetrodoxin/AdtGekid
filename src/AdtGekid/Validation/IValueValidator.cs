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
    /// Schnittstelle für Validierer bestimmter Datentypen.
    /// </summary>
    /// <typeparam name="T">Der Datentyp der Werte, die mit implementierenden Klassen validiert werden können.</typeparam>
    public interface IValueValidator<T>
    {
        /// <summary>
        /// Der Name des ADT-Objekts, die geprüft wird.
        /// Z.B. Patient, Diagnose, WeitereKlassifikation...
        /// </summary>
        string ValidateAdtObject { get; }

        /// <summary>
        /// Der Name des zu prüfenden Feldes innerhalb der Entität 
        /// (was auch vom komplexen Typ sein kann)
        /// </summary>
        string ValidatedAdtField { get; }
       

        /// <summary>
        /// Prüft einen Wert und gibt ihn angepasst zurück, sofern er korrekt verarbeitet werden konnte
        /// und löst für falsche Werte eine Ausnahme aus.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <returns>Der Datentyp des zu validierenden Werts.</returns>
        //T GetValidatedValueOrThrow(T value, string validatedEntity, string validatedObject);
        T GetValidatedValueOrThrow(T value);

        T GetValidatedValueOrThrow(T value, string validatedAdtObject, string validatedAdtField);
    }
}