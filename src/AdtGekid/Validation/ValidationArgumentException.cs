using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;


namespace AdtGekid.Validation
{
    public class ValidationArgumentException : ArgumentException
    {
     
        /// <summary>
        /// Names des ADT-Objekts, dessen Feld validiert wurde
        /// </summary>
        public string ValidatedAdtObject { get; set; }

        /// <summary>
        /// Names des ADT-Felds, das validiert wurde
        /// </summary>
        public string ValidatedAdtField { get; set; }


        /// <summary>
        /// Id des zugehörigen Patienten-Datensatzes aus dem Quellsystem, 
        /// falls benötigt
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Id des zugehörigen Datensatzes aus dem Quellsystem, 
        /// falls benötigt
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// Typ des zugehörigen Datensatzes aus dem Quellsystem,
        /// falls benötigt
        /// </summary>
        public string RecordType { get; set; }



        public ValidationArgumentException() : base()
        {

        }

        public ValidationArgumentException(string message) : base(message)
        {

        }
        public ValidationArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ValidationArgumentException(string message, string paramName) 
            : base(message, paramName)
        {

        }

        public ValidationArgumentException(string message, string paramName, Exception innerException)
            : base (message, paramName, innerException)
        {

        }        

        public ValidationArgumentException(string message, string validatedAdtEntity, string validatedAdtField)
            : base(message)
        {
            this.ValidatedAdtObject = validatedAdtEntity;
            this.ValidatedAdtField = validatedAdtField;
        }

        public ValidationArgumentException(string message, string validatedAdtEntity, string validatedAdtField, string recordType, string recordId)
          : this(message, validatedAdtEntity, validatedAdtField)
        {
            this.RecordType = recordType;
            this.RecordId = recordId;
        }

        [SecuritySafeCritical]
        protected ValidationArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)            
        {
            base.GetObjectData(info, context);
        }

    }
}