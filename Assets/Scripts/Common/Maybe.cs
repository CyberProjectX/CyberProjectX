using System;

namespace Scripts.Common
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>
            (this TInput input, Func<TInput, TResult> evaluator)
            where TInput : class
            where TResult : class
        {
            if (input == null)
            {
                return null;
            }

            return evaluator(input);
        }

        public static TResult Return<TInput, TResult>
            (this TInput input, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (input == null)
            {
                return failureValue;
            }

            return evaluator(input);
        }

        public static TResult OfType<TResult>
            (this Object input)
            where TResult : class
        {
            return input as TResult;
        }

        public static bool IsNull<TInput>
            (this TInput input)
            where TInput : class
        {
            return input == null;
        }

        public static bool IsNotNull<TInput>
            (this TInput input)
            where TInput : class
        {
            return input != null;
        }

        public static TInput If<TInput>
            (this TInput input, Predicate<TInput> evaluator)
            where TInput : class
        {
            if (input == null)
            {
                return null;
            }

            return evaluator(input) ? input : null;
        }

        public static TInput Do<TInput>
            (this TInput input, Action<TInput> action)
            where TInput : class
        {
            if (input == null)
            {
                return null;
            }

            action(input);

            return input;
        }
    }
}
