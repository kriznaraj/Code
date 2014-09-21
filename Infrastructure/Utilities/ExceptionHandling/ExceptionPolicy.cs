namespace Controls.ExceptionHandling
{
    internal class ExceptionPolicy
    {
        private readonly ExceptionConvertorMap convertorMap;

        private readonly ExceptionHandlerMap handlerMap;

        public ExceptionPolicy(ExceptionConvertorMap convertorMap, ExceptionHandlerMap handlerMap)
        {
            this.convertorMap = convertorMap;
            this.handlerMap = handlerMap;
        }

        public ExceptionConvertorMap ConvertorMap
        {
            get
            {
                return this.convertorMap;
            }
        }

        public ExceptionHandlerMap HandlerMap
        {
            get
            {
                return this.handlerMap;
            }
        }
    }
}