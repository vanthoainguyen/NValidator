﻿using System;
using System.Linq.Expressions;

namespace NValidator.Validators
{
    public class EqualValidator<T, TProperty> : ComparisonValidator<T, TProperty> where TProperty : IComparable
    {
        internal const string Message = "@PropertyName must equal to @ComparisonValue.";

        public EqualValidator(TProperty value)
            : base(value, Message, NotEqualValidator<T, TProperty>.Message)
        {
        }

        public EqualValidator(Expression<Func<T, TProperty>> expression)
            : base(expression, Message, NotEqualValidator<T, TProperty>.Message)
        {
        }

        public override bool IsValid(TProperty value, ValidationContext validationContext)
        {
            if (_value == null)
            {
                return value == null;
            }
            return ((IComparable)_value).CompareTo(value) == 0;
        }
    }
}
