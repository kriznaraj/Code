using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Types
{
    public class BaseStreamNonDisposingCryptoStream : CryptoStream
    {
        public BaseStreamNonDisposingCryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
            : base(stream, transform, mode)
        { }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.HasFlushedFinalBlock)
                    {
                        this.FlushFinalBlock();
                    }
                }
            }
            finally
            {
                base.Dispose(false);
            }
        }
    }
}