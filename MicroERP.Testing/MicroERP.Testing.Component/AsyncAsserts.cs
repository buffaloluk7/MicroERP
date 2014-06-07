using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component
{
    public static class AsyncAsserts
    {
        /// <summary>
        ///     Assert that an async method fails due to a specific exception.
        ///     This exception can be thrown directly or be the root cause of an aggregate exception.
        /// </summary>
        /// <typeparam name="T">Exception type expected</typeparam>
        /// <param name="testCode">Test async delegate</param>
        public static void Throws<T>(Func<Task> testCode) where T : Exception
        {
            try
            {
                Task.WaitAll(testCode());
                Assert.Fail("Expected exception of type: {0}", typeof (T));
            }
            catch (AggregateException aex)
            {
                if (!aex.BaseIsOfType<T>())
                {
                    Assert.Fail(
                        "Expected aggregate exception with base type: {0}"
                        + "\r\nBut got an aggregate exception with base type: {1}",
                        typeof (T),
                        aex.GetBaseException().GetType());
                }

                // Continue excecution if base exception was expected.
            }
            catch (T)
            {
                // Swallow exception as this is correct.
            }
        }
    }

    public static class AggregateExceptionExtensions
    {
        public static bool BaseIsOfType<T>(this AggregateException aex)
        {
            return aex.GetBaseException() is T;
        }
    }
}