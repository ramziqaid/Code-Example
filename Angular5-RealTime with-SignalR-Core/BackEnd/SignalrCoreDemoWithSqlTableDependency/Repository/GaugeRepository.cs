using SignalrCoreDemoWithSqlTableDependency.EF;
using SignalrCoreDemoWithSqlTableDependency.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalrCoreDemoWithSqlTableDependency.Repository
{
    public class GaugeRepository : IGaugeRepository
    {
        private readonly Func<GaugeContext> _contextFactory;

        public Gauge Gauge => GetGauge();
        public List<Gauge> GaugeList => GetGaugeList();

        public GaugeRepository(Func<GaugeContext> context)
        {
            _contextFactory = context;
        }

        private Gauge GetGauge()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Gauges.FirstOrDefault();
            }
        } 
        
        private List<Gauge> GetGaugeList()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Gauges.ToList();
            }
        }

    }
}
