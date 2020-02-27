using SignalrCoreDemoWithSqlTableDependency.Models;
using System.Collections.Generic;

namespace SignalrCoreDemoWithSqlTableDependency.Repository
{
    public interface IGaugeRepository
    {
        Gauge Gauge { get; }
        List<Gauge> GaugeList { get; }
    }
}
