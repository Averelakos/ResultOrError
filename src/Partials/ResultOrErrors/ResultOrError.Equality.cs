﻿using ResultOrError.Models;

namespace ResultOrError.Partials
{
    public readonly partial record struct ResultOrError<TValue>
    {
        public bool Equals(ResultOrError<TValue> other)
        {
            if (!IsError)
            {
                return !other.IsError && EqualityComparer<TValue>.Default.Equals(_value, other._value);
            }

            return other.IsError && CheckIfErrorsAreEqual(_errors, other._errors);
        }

        public override int GetHashCode()
        {
            if (!IsError)
            {
                return _value.GetHashCode();
            }


            var hashCode = new HashCode();

            for (var i = 0; i < _errors.Count; i++)
            {
                hashCode.Add(_errors[i]);
            }

            return hashCode.ToHashCode();
        }

        private static bool CheckIfErrorsAreEqual(List<Error> errors1, List<Error> errors2)
        {
            // This method is currently implemented with strict ordering in mind, so the errors
            // of the two lists need to be in the exact same order.
            // This avoids allocating a hash set. We could provide a dedicated EqualityComparer for
            // ErrorOr<TValue> when arbitrary orders should be supported.
            if (ReferenceEquals(errors1, errors2))
            {
                return true;
            }

            if (errors1.Count != errors2.Count)
            {
                return false;
            }

            for (var i = 0; i < errors1.Count; i++)
            {
                if (!errors1[i].Equals(errors2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
