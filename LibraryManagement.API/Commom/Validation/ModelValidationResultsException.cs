using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Common.Validation
{
    [Serializable]
    public class ModelValidationResultsException : System.Exception, ISerializable
    {
        private ModelValidationResults _results;

        public ModelValidationResultsException(ModelValidationResults results)
            : base()
        {
            _results = results;
        }

        public ModelValidationResultsException(string errorMessage, IEnumerable<string> memberNames = null, ModelValidationResultType type = ModelValidationResultType.Error)
            : base()
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException("errorMessage");
            }

            _results = new ModelValidationResults();
            _results.Add(errorMessage, memberNames, type);
        }

        public ModelValidationResultsException(IEnumerable<ValidationResult> results)
            : base()
        {
            _results = new ModelValidationResults();
            foreach (var result in results)
            {
                _results.Add(result);
            }
        }


        public ModelValidationResults Result
        {
            get
            {
                return _results;
            }
        }

        public override string Message
        {
            get
            {
                string message = "Model Validation Exception (See below):";

                foreach (var error in _results.Details)
                {
                    message += Environment.NewLine + error;
                }

                return message;
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Result", _results);
        }
    }
}