using System;

namespace Controls.Types
{
    public static class ExecutionContextProvider
    {
        [ThreadStatic]
        private static IExecutionContext ExecutionContext;

        public static IExecutionContext Current
        {
            get
            {
                return ExecutionContextProvider.ExecutionContext;
            }
        }

        public static void SetExecutionContext(IExecutionContext executionContext)
        {
            ExecutionContextProvider.ExecutionContext = executionContext;
        }

        public static void Clear()
        {
            ExecutionContextProvider.ExecutionContext = null;
        }
    }
}