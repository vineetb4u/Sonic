using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Common.Validation
{
    public enum ModelValidationResultType
    {
        Error,
        Warning
    }

    public class ModelValidationResult : ValidationResult
    {
        private ModelValidationResultType _type;

        public ModelValidationResult(string message, ModelValidationResultType type = ModelValidationResultType.Error)
            : base(message)
        {
            _type = type;
        }
        public ModelValidationResult(ValidationResult validationResult, ModelValidationResultType type = ModelValidationResultType.Error)
            : base(validationResult)
        {
            _type = type;
        }
        public ModelValidationResult(string message, IEnumerable<string> memberNames, ModelValidationResultType type = ModelValidationResultType.Error)
            : base(message, memberNames)
        {
            _type = type;
        }

        public ModelValidationResultType Type
        {
            get
            {
                return _type;
            }
        }
    }

    public class ModelValidationResults
    {
        List<ModelValidationResult> _results = new List<ModelValidationResult>();

        public bool Success
        {
            get
            {
                return (!_results.Any(r => r.Type == ModelValidationResultType.Error));
            }
        }

        public List<ModelValidationResult> Details
        {
            get
            {
                return _results;
            }
        }

        public void Clear()
        {
            _results.Clear();
        }

        public void Add(string errorMessage, IEnumerable<string> memberNames, ModelValidationResultType type)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException("errorMessage");
            }

            ModelValidationResult result = new ModelValidationResult(errorMessage, memberNames, type);
            _results.Add(result);
        }

        public void AddError(string errorMessage, IEnumerable<string> memberNames = null)
        {
            Add(errorMessage, memberNames, ModelValidationResultType.Error);
        }

        public void AddWarning(string errorMessage, IEnumerable<string> memberNames = null)
        {
            Add(errorMessage, memberNames, ModelValidationResultType.Warning);
        }

        public void Add(ModelValidationResults sourceResults)
        {
            if (sourceResults != null)
            {
                foreach (var result in sourceResults.Details)
                {
                    _results.Add(result);
                }
            }
        }

        public void Add(List<ValidationResult> sourceList)
        {
            if (sourceList != null)
            {
                foreach (var src in sourceList)
                {
                    addValidationResult(src);
                }
            }
        }

        private void addValidationResult(ValidationResult validationResult)
        {
            if (validationResult != null)
            {
                var srcFirstMemberName = validationResult.MemberNames.FirstOrDefault();

                var existing = this.Details.FirstOrDefault(r => r.MemberNames.Any(x => x == srcFirstMemberName));

                if ((existing == null) || (string.IsNullOrWhiteSpace(srcFirstMemberName)))
                {
                    this.AddError(validationResult.ErrorMessage, validationResult.MemberNames);
                }
            }
        }

        public void Add(ValidationResult validationResult)
        {
            addValidationResult(validationResult);
        }
    }
}