﻿using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public interface IStatValue
    {
        float Value { get; }

        int CountableValue { get; }
    }

    public interface IStatExtractor
    {
        string StatName { get; }

        bool MoreIsBetter { get; }

        Func<IPlayer, IStatValue> Extract { get; }

        IStatValue Aggregate(IEnumerable<IStatValue> values);
    }
}
