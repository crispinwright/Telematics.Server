using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Web;
using System.Reactive;

namespace Telematics.Server.Extensions
{

    //TODO EXTENSION METHODS FOR ENUMERABLE EXTENSIONS
    public partial class GEOMain
    {

        public IObservable<GEOMainPointTablePoints> PointsToObservable(GEOMainPointTable pointsTable)
        {
            //return Observable.Create<GEOMainPointTablePoints>();
            return null;
        }
    }
}