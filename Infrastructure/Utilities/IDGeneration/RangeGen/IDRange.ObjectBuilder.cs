using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BallyTech.Infrastructure.IDGeneration;
using BallyTech.Infrastructure.Types;

namespace BallyTech.Infrastructure.IDGeneration
{
    public partial class IDRange : IObjectBuilder<IDataReader, IDRange>
    {
        IList<IDRange> IObjectBuilder<IDataReader, IDRange>.Fill(IDataReader reader)
        {
            List<IDRange> idRanges = new List<IDRange>();

            while (reader.Read())
            {
                IDRange idRange = new IDRange(new IDRangeId(reader.GetString(0)));

                idRange.StartRange = reader.GetInt64(1);
                idRange.EndRange = reader.GetInt64(2);

                idRanges.Add(idRange);
            }

            return idRanges;
        }
    }
}