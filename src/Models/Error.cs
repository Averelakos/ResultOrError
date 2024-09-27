using ResultOrError.Enums;

namespace ResultOrError.Models
{
    public readonly record struct Error
    {
        /// <summary>
        /// Gets the unique error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the error type.
        /// </summary>
        public ErrorType Type { get; }

        /// <summary>
        /// Gets the numeric value of the type.
        /// </summary>
        public int NumericType { get; }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; }

        private Error(string code, string description, ErrorType type, Dictionary<string, object>? metadata)
        {
            Code = code;
            Description = description;
            Type = type;
            NumericType = (int)type;
            Metadata = metadata;
        }

        /// <summary>
        /// Creates an Error of type Failure from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Failure(
            string code = "General.Failure", 
            string description = "A failure has occured.",
            Dictionary<string, object>? metadata = null) 
            => new Error(code, description, ErrorType.Failure, metadata);

        /// <summary>
        /// Creates an Error of type Unexpected from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Unexpected(
            string code = "General.Unexpected",
            string description = "An unexpected error has occured.",
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, ErrorType.Unexpected, metadata);

        /// <summary>
        /// Creates an Error of type Validation from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Validation(
            string code = "General.Validation",
            string description = "A validation error has occured.",
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, ErrorType.Validation, metadata);

        /// <summary>
        /// Creates an Error of type Conflict from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Conflict(
            string code = "General.Conflict",
            string description = "A conflict error has occured.",
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, ErrorType.Conflict, metadata);

        /// <summary>
        /// Creates an Error of type NotFound from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error NotFound(
            string code = "General.NotFound",
            string description = "A 'Not Found' error has occured.",
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, ErrorType.NotFound, metadata);

        /// <summary>
        /// Creates an Error of type Unauthorized from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Unauthorized(
            string code = "General.Unauthorized",
            string description = "An 'Unauthorized' error has occured.",
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, ErrorType.Unauthorized, metadata);

        /// <summary>
        /// Creates an Error of type Forbidden from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Forbidden(
            string code = "General.Forbidden",
            string description = "An 'Forbidden' error has occured.",
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, ErrorType.Forbidden, metadata);

        /// <summary>
        /// Creates an Error with the given numeric, code and description.
        /// </summary>
        /// <param name="type">An integer value which represents the type of error that occurred.</param>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        /// <returns></returns>
        public static Error Custom(
            int type,
            string code,
            string description,
            Dictionary<string, object>? metadata = null)
            => new Error(code, description, (ErrorType)type, metadata);

        public bool Equals(Error other)
        {
            if (Type != other.Type ||
                NumericType != other.NumericType ||
                Code != other.Code ||
                Description != other.Description)
                return false;

            if (Metadata is null)
                return other.Metadata is null;

            return other.Metadata is not null && CompareMetadata(Metadata, other.Metadata);
        }

        public override int GetHashCode() => Metadata is null ? HashCode.Combine(Code, Description, Type, NumericType) : ComposeHashCode();

        private int ComposeHashCode() 
        {
            var hashCode = new HashCode();
            hashCode.Add(Code);
            hashCode.Add(Description);
            hashCode.Add(Type);
            hashCode.Add(NumericType);
            foreach (var keyValuePair in Metadata!)
            {
                hashCode.Add(keyValuePair.Key);
                hashCode.Add(keyValuePair.Value);
            }

            return hashCode.ToHashCode();
        }

        private static bool CompareMetadata(Dictionary<string, object> metadata, Dictionary<string, object> otherMetadata)
        {
            if (ReferenceEquals(metadata, otherMetadata))
                return true;


            if (metadata.Count != otherMetadata.Count)
                return false;


            foreach (var keyValuePair in metadata)
            {
                if (!otherMetadata.TryGetValue(keyValuePair.Key, out var otherValue) ||
                    !keyValuePair.Value.Equals(otherValue))
                    return false;

            }

            return true;
        }

    }
}
