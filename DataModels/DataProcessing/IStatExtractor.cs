using System;
using System.Collections.Generic;

namespace FantasySports.DataModels.DataProcessing
{
    public interface IStatExtractor
    {
        Constants.StatID StatID { get; }

        bool MoreIsBetter { get; }

        IStatValue Extract(Root root, int playerId, Constants.StatSource statSource);

        IStatValue Aggregate(IEnumerable<IStatValue> values);
    }
}
